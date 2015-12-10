/**
 * @file
 * @brief ワーカースレッド実装用
 */

using System;
using System.Diagnostics;
using System.Threading;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>ワーカー処理の非同期監視用</summary>
    public class WorkerThreadAsyncResult : IAsyncResult {

        /// <summary>引数</summary>
        public object AsyncState { get; set; }

        /// <summary>同期用イベントハンドル</summary>
        public WaitHandle AsyncWaitHandle { get; set; }

        public bool CompletedSynchronously {
            get { return false; }
        }

        /// <summary>ワーカー処理が完了したかを返す</summary>
        public bool IsCompleted {
            get { return WorkerThread.StateId == WorkerThreadStateId.Idle; }
        }

        //
        //
        //

        /// <summary>ワーカースレッド</summary>
        public WorkerThread WorkerThread;

        /// <summary>終了を指定時間待つ</summary>
        /// <param name="msec">待ち時間</param>
        public bool Wait(int msec) {
            return AsyncWaitHandle.WaitOne(msec);
        }

        /// <summary>終了を指定時間待つ</summary>
        /// <param name="span">待ち時間</param>
        public bool Wait(TimeSpan span) {
            return AsyncWaitHandle.WaitOne(span);
        }
    }

    /// <summary>ワーカー処理の非同期監視用</summary>
    public class WorkerThreadAsyncResult<TResult> : WorkerThreadAsyncResult {

        /// <summary>処理結果を返します、処理中の場合はブロックします</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TResult Result {
            get { return (TResult)WorkerThread.WorkerResult; }
        }

        /// <summary>終了を待つ、実行中の場合はブロックします</summary>
        protected bool Wait() {
            WorkerThread.LogError("blocking are you sure? " + Environment.StackTrace);
            return AsyncWaitHandle.WaitOne();
        }
    }

    //
    //
    //

    /// <summary>ワーカースレッドのステート</summary>
    public enum WorkerThreadStateId {
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

        /// <summary>エラーログ出力用</summary>
        public static Action<string> LogError = _ => { };

        /// <summary>サービス状態</summary>
        public volatile WorkerThreadStateId StateId;

        /// <summary>ディスポーズされているか</summary>
        public bool IsDisposed {
            get { return StateId == WorkerThreadStateId.Disposed; }
        }

        /// <summary>ワーカースレッドの名前</summary>
        public string Name {
            get { return Thread.Name; }
            set { Thread.Name = value; }
        }

        /// <summary>ワーカースレッド</summary>
        protected Thread Thread;

        /// <summary>ワーカー処理開始通知用</summary>
        protected AutoResetEvent StartEvent;

        /// <summary>ワーカー処理終了通知用</summary>
        protected AutoResetEvent ComplateEvent;

        /// <summary>非同期のリザルト</summary>
        protected WorkerThreadAsyncResult AsyncResult;

        /// <summary>リザルトの型情報</summary>
        protected Type AsyncResultType;

        /// <summary>ワーカーの引数/summary>
        protected object WorkerParam { get; set; }

        /// <summary>ワーカーのスレッドの処理</summary>
        protected Func<object, object> WorkerFunc;

        /// <summary>ワーカーの処理結果を返します、処理中の場合はブロックします</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public object WorkerResult {
            get {
                // 非ブロックを試みる
                if (!ComplateEvent.WaitOne(0)) {
                    // ブロックする
                    ComplateEvent.WaitOne();
                }
                var ret = WorkerResult;
                WorkerResult_ = null;
                return ret;
            }
            protected set {
                WorkerResult_ = value;
            }
        }

        /// <summary>ワーカーのリザルト</summary>
        object WorkerResult_;

        //
        //
        //

        /// <summary>コンストラクタ</summary>
        public WorkerThread() {
            StateId = WorkerThreadStateId.Init;
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
            WorkerParam = null;
            WorkerFunc = null;
            WorkerResult = null;
            StateId = WorkerThreadStateId.Disposed;
            GC.SuppressFinalize(this);
        }

        /// <summary>処理を開始させます</summary>
        /// <exception cref="ObjectDisposedException">ディスポーズ時</exception>
        /// <exception cref="InvalidOperationException">処理出来ない状態時</exception>
        public WorkerThreadAsyncResult<TResult> StartNew<TResult>( //
            Func<TResult> job) //
        {
            return Starter<TResult>(_ => job(), null);
        }

        /// <summary>処理を開始させます</summary>
        /// <exception cref="ObjectDisposedException">ディスポーズ時</exception>
        /// <exception cref="InvalidOperationException">処理出来ない状態時</exception>
        public WorkerThreadAsyncResult<TResult> StartNew<TParam, TResult>( //
            Func<TParam, TResult> job, TParam state) //
        {
            return Starter<TResult>(_ => job((TParam)_), state);
        }

        /// <summary>処理を開始させます</summary>
        WorkerThreadAsyncResult<TResult> Starter<TResult>( //
            Func<object, object> func, object state) //
        {
            // 状態の確認
            var st = StateId;
            switch (st) {
                case WorkerThreadStateId.Init:
                case WorkerThreadStateId.Idle:
                    break;
                case WorkerThreadStateId.Disposed:
                    throw new ObjectDisposedException("disposed");
                default:
                    throw new InvalidOperationException("out of service state");
            }

            // 非同期リザルト関係の用意
            var ty = typeof(WorkerThreadAsyncResult<TResult>);
            WorkerThreadAsyncResult<TResult> result;
            if (ty == AsyncResultType) {
                result = (WorkerThreadAsyncResult<TResult>)AsyncResult;
            }
            else {
                result = new WorkerThreadAsyncResult<TResult>();
                AsyncResult = result;
                AsyncResultType = ty;
            }

            // スレッドの用意
            WorkerParam = state;
            WorkerFunc = func;
            WorkerResult = null;
            if (st == WorkerThreadStateId.Init) {
                StartEvent = new AutoResetEvent(false);
                ComplateEvent = new AutoResetEvent(false);
                Thread = new Thread(Worker);
                result.AsyncWaitHandle = ComplateEvent;
                result.WorkerThread = this;
                Thread.Start();
            }
            else if (st == WorkerThreadStateId.Idle) {
                result.AsyncWaitHandle = ComplateEvent;
                result.WorkerThread = this;
                StartEvent.Set();
            }
            return result;
        }

        //
        //
        //

        /// <summary>スレッド処理の制御</summary>
        void Worker() {
            try {
            loop:
                // 実行
                StateId = WorkerThreadStateId.Run;
                WorkerResult = WorkerFunc(WorkerParam);
                WorkerFunc = null;
                WorkerParam = null;
                // 終了通知
                StateId = WorkerThreadStateId.Idle;
                ComplateEvent.Set();
                // 次の処理を待ちます
                StartEvent.WaitOne();
                goto loop;
            }
            catch (ThreadAbortException) {
                // 停止要求によるアボート例外
                StateId = WorkerThreadStateId.ShutDown;
            }
            catch (Exception e) {
                // 想定外の例外
                StateId = WorkerThreadStateId.UnhandleException;
                LogError(e.StackTrace);
            }
        }
    }
}


