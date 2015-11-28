/**
 * @file
 * @brief ワーカースレッド実装用
 */

using System;
using System.Threading;
using System.Collections.Generic;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>ワーカースレッドのステート</summary>
    public enum WorkerThreadState {
        /// <summary>サービス中</summary>
        Idle,
        /// <summary>処理中</summary>
        Run,
        /// <summary>サービス停止</summary>
        ShutDown,
        /// <summary>不正サービス停止</summary>
        Fatal,
    }

    /// <summary>ワーカースレッド実装用の基底</summary>
    public abstract class WorkerThreadBase : IDisposable {

        #region WorkerThread // ワーカースレッドが参照変更するもの

        /// <summary>サービス状態</summary>
        public WorkerThreadState State {
            get { return State_; }
            private set { State_ = value; }
        }

        /// <summary>サービス状態</summary>
        volatile WorkerThreadState State_ = WorkerThreadState.Idle;

        /// <summary>ディスポーズされているか</summary>
        public volatile bool IsDisposed = false;

        /// <summary>ワーカー処理開始通知用</summary>
        AutoResetEvent RunEvent = new AutoResetEvent(false);

        #endregion

        /// <summary>ワーカー処理終了通知用</summary>
        protected AutoResetEvent IdleEvent = new AutoResetEvent(false);

        /// <summary>ワーカーのスレッド</summary>
        Thread Thread;

        //
        //
        //

        /// <summary>コンストラクタ</summary>
        public WorkerThreadBase() {
        }

        /// <summary>ファイナライザ</summary>
        ~WorkerThreadBase() {
            Dispose(false);
        }

        /// <summary>ディスポーズ</summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>ディスポーズ</summary>
        void Dispose(bool disposing) {
            if (IsDisposed) {
                return;
            }
            IsDisposed = true;
            // ワーカーを強制停止 ワーカーでは ThreadAbortException が起きる
            if (Thread != null) {
                Thread.Abort();
                Thread = null;
            }
            using (var t = RunEvent as IDisposable) { }
            RunEvent = null;
            using (var t = IdleEvent as IDisposable) { }
            IdleEvent = null;
            // 派生のディスポーズ
            OnDispose();
            GC.SuppressFinalize(this);
        }

        //
        //
        //

        /// <summary>メソッド等の事前処理</summary>
        protected void Check() {
            if (IsDisposed) {
                throw new ObjectDisposedException("disposed");
            }
            if (State != WorkerThreadState.Fatal) {
                throw new InvalidOperationException("state fatal");
            }
        }

        /// <summary>サービススタート</summary>
        public void Start() {
            Check();
            if (Thread == null) {
                Thread = new Thread(Worker);
                Thread.Start();
            }
        }

        /// <summary>処理を開始させます (idle から run にする)</summary>
        /// <param name="passed">開始時実行アクション</param>
        /// <exception cref="InvalidOperationException">not idle</exception>
        protected void TryRun(Action passed = null) {
            Start();
            if (State != WorkerThreadState.Idle) {
                throw new InvalidOperationException("not idle");
            }
            if (passed != null) passed();
            RunEvent.Set();
        }

        /// <summary>スレッドの処理を待ちます (終了時は run から idle にする)</summary>
        public bool Wait(int n) {
            Check();
            return IdleEvent.WaitOne(n);
        }

        //
        //
        //

        /// <summary>スレッド処理の制御</summary>
        void Worker() {
            try {
                while (true) {
                    RunEvent.WaitOne();
                    State = WorkerThreadState.Run;
                    OnJob();
                    State = WorkerThreadState.Idle;
                    IdleEvent.Set();
                }
            }
            catch (ThreadAbortException) {
                State = WorkerThreadState.ShutDown;
            }
            catch (Exception e) {
                State = WorkerThreadState.Fatal;
                OnUnHandledException(e);
            }
        }

        /// <summary>ディスポーズ処理</summary>
        protected abstract void OnDispose();

        /// <summary>スレッド処理</summary>
        protected abstract void OnJob();

        /// <summary>スレッド処理中の誰もキャッチしない例外</summary>
        protected virtual void OnUnHandledException(Exception e) {
        }
    }
}


