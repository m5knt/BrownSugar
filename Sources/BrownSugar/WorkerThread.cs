/**
 * @file
 * @brief ワーカースレッド実装用
 */

using System;
using System.Threading;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>ワーカースレッドの同期用</summary>
    public class WorkerThreadAsyncResult : IAsyncResult {

        /// <summary>任意のオブジェクト</summary>
        public object AsyncState { get; set; }

        /// <summary>同期用イベントハンドル</summary>
        public WaitHandle AsyncWaitHandle { get; set; }

        public bool CompletedSynchronously {
            get { return false; }
        }

        /// <summary>処理が完了したかを返す</summary>
        public bool IsCompleted {
            get { return WorkerThread.State == WorkerThreadState.Idle; }
        }

        /// <summary>ワーカースレッド</summary>
        public WorkerThread WorkerThread;

        /// <summary>ワーカーの処理結果を返します、処理中の場合はブロックします</summary>
        public object Result {
            get { Wait(); return Result_; }
            set { Result_ = value; }
        }

        /// <summary>ワーカーの処理結果</summary>
        object Result_;

        /// <summary>終了を指定時間待つ</summary>
        /// <param name="msec">待ち時間</param>
        public bool Wait(int msec) {
            //if (IsCompleted) return true;
            return AsyncWaitHandle.WaitOne(msec);
        }

        /// <summary>終了を待つ、実行中の場合はブロックします</summary>
        public bool Wait() {
            //if (IsCompleted) return true;
            return AsyncWaitHandle.WaitOne();
        }
    }

    /// <summary>ワーカースレッドのステート</summary>
    public enum WorkerThreadState {
        /// <summary>初期状態</summary>
        Init,
        /// <summary>サービス中</summary>
        Idle,
        /// <summary>処理中</summary>
        Run,
        /// <summary>サービス停止</summary>
        ShutDown,
        /// <summary>不正サービス停止</summary>
        UnhandleException,
        /// <summary>ディスポーズ済み</summary>
        Disposed,
    }

    /// <summary>ワーカースレッドの処理</summary>
    public delegate object WorkerThreadJob(object obj);

    //
    //
    //

    /// <summary>ワーカースレッド実装用の基底</summary>
    public class WorkerThread : IDisposable {

        /// <summary>サービス状態</summary>
        public WorkerThreadState State {
            get { return State_; }
            private set { State_ = value; }
        }

        /// <summary>サービス状態</summary>
        volatile WorkerThreadState State_ = WorkerThreadState.Init;

        /// <summary>ディスポーズされているか</summary>
        public bool IsDisposed {
            get { return State == WorkerThreadState.Disposed; }
        }

        /// <summary>ワーカースレッドの名前</summary>
        public string Name {
            get { return Thread.Name; }
            set { Thread.Name = value; }
        }

        /// <summary>ワーカーのスレッドの処理</summary>
        protected Func<object, object> WorkerFunc;

        /// <summary>同期用</summary>
        protected WorkerThreadAsyncResult AsyncResult;

        /// <summary>ワーカースレッド</summary>
        protected Thread Thread;

        /// <summary>ワーカー処理開始通知用</summary>
        protected AutoResetEvent StartEvent = new AutoResetEvent(false);

        /// <summary>ワーカー処理終了通知用</summary>
        protected AutoResetEvent ComplateEvent;

        //
        //
        //

        /// <summary>コンストラクタ</summary>
        public WorkerThread() {
            ComplateEvent = new AutoResetEvent(false);
            AsyncResult = new WorkerThreadAsyncResult();
            AsyncResult.AsyncWaitHandle = ComplateEvent;
            AsyncResult.WorkerThread = this;
        }

        /// <summary>ファイナライザ</summary>
        ~WorkerThread() {
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
            if (Thread != null) {
                // ワーカーを強制停止 ワーカーでは ThreadAbortException が起きる
                Thread.Abort();
                Thread.Join();
                Thread = null;
            }
            AsyncResult = null;
            using (var t = StartEvent as IDisposable) { }
            StartEvent = null;
            using (var t = ComplateEvent as IDisposable) { }
            ComplateEvent = null;
            WorkerFunc = null;
            State = WorkerThreadState.Disposed;
            GC.SuppressFinalize(this);
        }

        /// <summary>処理を開始させます</summary>
        public WorkerThreadAsyncResult StartNew(Action job) {
            return Starter(_ => { job(); return null; }, null);
        }

        /// <summary>処理を開始させます</summary>
        public WorkerThreadAsyncResult StartNew<TParam>(Action<TParam> job, //
            TParam state) //
        {
            return Starter(_ => { job((TParam)_); return null; }, state);
        }

        /// <summary>処理を開始させます</summary>
        public WorkerThreadAsyncResult StartNew<TResult>(Func<TResult> job) {
            return Starter(_ => job(), null);
        }

        /// <summary>処理を開始させます</summary>
        public WorkerThreadAsyncResult StartNew<TParam, TResult>( //
            Func<TParam, TResult> job, TParam state) //
        {
            return Starter(_ => job((TParam)_), state);
        }

        /// <summary>処理を開始させます</summary>
        WorkerThreadAsyncResult Starter(Func<object, object> func, object state) {
            if (IsDisposed) {
                throw new ObjectDisposedException("disposed");
            }
            var st = State;
            if (st == WorkerThreadState.Init) {
                AsyncResult.AsyncState = state;
                WorkerFunc = func;
                Thread = new Thread(Worker);
                Thread.Start();
            }
            else if (st == WorkerThreadState.Idle) {
                AsyncResult.AsyncState = state;
                WorkerFunc = func;
                StartEvent.Set();
            }
            else {
                throw new InvalidOperationException("not idle");
            }
            return AsyncResult;
        }

        //
        //
        //

        /// <summary>スレッド処理の制御</summary>
        void Worker() {
            try {
            loop:
                // 実行
                State = WorkerThreadState.Run;
                AsyncResult.Result = WorkerFunc(AsyncResult.AsyncState);
                // 終了通知
                State = WorkerThreadState.Idle;
                ComplateEvent.Set();
                // 次の処理を待ちます
                StartEvent.WaitOne();
                goto loop;
            }
            catch (ThreadAbortException) {
                // 停止要求によるアボート例外
                State = WorkerThreadState.ShutDown;
            }
            catch (Exception e) {
                // 想定外の例外
                State = WorkerThreadState.UnhandleException;
            }
        }
    }
}


