/**
 * @file
 * @brief オブジェクトプール関係
 */

using System;
using System.Collections.Generic;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>オブジェクト管理情報</summary>
    public class ObjectPoolStat {
        /// <summary>オブジェクト</summary>
        public IObjectHolder Holder;
        /// <summary>オブジェクト弱参照</summary>
        public WeakReference Weak;
    }

    public delegate IObjectHolder ObjectHolderCreater();

    /// <summary>オブジェクトプールのオブジェクトを保持するインターフェース</summary>
    public interface IObjectHolder : IDisposable {
        /// <summary>ディスポーズ済みかを返す</summary>
        bool IsFakeDisposed { get; }
        /// <summary>本当のディスポーズ</summary>
        void RealDispose();

        /// <summary>オブジェクト管理元</summary>
        IObjectPool Pool { get; set; }
        /// <summary>オブジェクト管理情報</summary>
        ObjectPoolStat Stat { get; set; }
        /// <summary>再利用</summary>
        void Reuse();
    }

    /// <summary>オブジェクトプールのオブジェクトを保持するインターフェース</summary>
    public interface IObjectHolder<TValue> : IObjectHolder {
        /// <summary>オブジェクトの値</summary>
        TValue Value { get; }
    }

    /// <summary>オブジェクトプールのインターフェース</summary>
    public interface IObjectPool : IDisposable {
        /// <summary>オブジェクトを確保します</summary>
        IObjectHolder Allocate();
        /// <summary>プールへ戻す</summary>
        /// <returns>成否を返す</returns>
        bool Free(IObjectHolder holder);
        /// <summary>プールしているオブジェクト数</summary>
        int Count { get; }
    }

    //
    //
    //    
    
    /// <summary>オブジェクトプール用のオブジェクトを保持</summary>
    public class ObjectHolder<TValue> : IObjectHolder<TValue> //
        where TValue : class //
    {
        /// <summary>保持するオブジェクト</summary>
        public TValue Value { get; protected set; }

        /// <summary>ディスポーズされているか</summary>
        public bool IsFakeDisposed { get; protected set; }

        /// <summary>管理元プール</summary>
        public IObjectPool Pool { get; set; }

        /// <summary>管理キー</summary>
        public ObjectPoolStat Stat { get; set; }

        //
        //
        //

        /// <summary>コンストラクタ</summary>
        public ObjectHolder(TValue val) {
            IsFakeDisposed = true;
            Value = val;
        }

        /// <summary>再利用</summary>
        public void Reuse() {
            IsFakeDisposed = false;
        }

        /// <summary>ディスポーズ</summary>
		public void Dispose() {
            if (IsFakeDisposed) {
                return;
            }
            IsFakeDisposed = true;
            // プールがあるなら返す
            if (Pool.Free(this)) {
                return;
            }
            // 実際にディスポーズ
            RealDispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>本当のディスポーズ</summary>
        public void RealDispose() {
            using (var dis = Value as IDisposable) { }
            Stat = null;
            Value = null;
            Pool = null;
        }
    }

    //
    //
    //

    /// <summary>オブジェクトプール</summary>
    public class ObjectPool : IObjectPool {

        /// <summary>ディスポーズ済みかを返す</summary>
        public bool IsDisposed {
            get { return IsDisposed_; }
            private set { IsDisposed_ = value; }
        }

        /// <summary>プールしているオブジェクト数</summary>
        public int Count { 
            get { 
                lock (this) {
                    CheckDisposed();
                    return Pooled.Count;
                }
            }
        }

        /// <summary>ディスポーズ済みかを返す</summary>
        volatile bool IsDisposed_;

        /// <summary>プール中</summary>
        LinkedList<ObjectPoolStat> Pooled;

        /// <summary>貸出中</summary>
        LinkedList<ObjectPoolStat> Leased;

        /// <summary>オブジェクト生成器</summary>
        ObjectHolderCreater Creater;
        /// <summary>保持量</summary>
        int Limit;

        /// <summary>コンストラクタ</summary>
        public ObjectPool(ObjectHolderCreater creater, int limit) {
            IsDisposed = false;
            Pooled = new LinkedList<ObjectPoolStat>();
            Leased = new LinkedList<ObjectPoolStat>();
            Limit = limit;
            Creater = creater;
        }

        void CheckDisposed() {
            // ディスポーズ済みなら例外
            if (IsDisposed) {
                throw new ObjectDisposedException("disposed");
            }
        }

        /// <summary>プールからオブジェクトを確保する</summary>
        public IObjectHolder Allocate() //
        {
            LinkedListNode<ObjectPoolStat> node = null;
            // プールから取得を試みる
            lock(this) {
                CheckDisposed();
                if (Pooled.Count != 0) {
                    node = Pooled.First;
                    Pooled.RemoveFirst();
                }
            }
            // 取得できたら貸出
            if (node != null) {
                return DoLeased(node);
            }
            // 新規に作成して貸出
            var holder = Creater();
            var stat = new ObjectPoolStat();
            holder.Pool = this;
            holder.Stat = stat;
            stat.Holder = holder;
            stat.Weak = new WeakReference(stat.Holder);
            node = new LinkedListNode<ObjectPoolStat>(stat);
            return DoLeased(node);
        }

        /// <summary>貸出状態にする</summary>
        IObjectHolder DoLeased(LinkedListNode<ObjectPoolStat> node) {
            // 未返却対応で弱監視にする
            var holder = (IObjectHolder)node.Value.Holder;
            node.Value.Holder = null;
            holder.Reuse();
            // 貸出表へ登録
            lock (this) {
                if (!IsDisposed) {
                    Leased.AddLast(node);
                }
            }
            return holder;
        }

        /// <summary>プールへ戻す</summary>
        public bool Free(IObjectHolder holder) {
            holder.Stat.Holder = holder;
            lock (this) {
                if (IsDisposed) return false;
                // 貸出表から消す
                Leased.Remove(holder.Stat);
                // 十分な数があればプールに入れない
                if (Pooled.Count >= Limit) return false;
                Pooled.AddLast(holder.Stat);
                return true;
            }
        }

        /// <summary>ディスポーズ</summary>
        public void Dispose() {
            LinkedList<ObjectPoolStat> pooled;
            LinkedList<ObjectPoolStat> leased;
            lock (this) {
                if (IsDisposed) return;
                IsDisposed_ = true;
                pooled = Pooled;
                Pooled = null;
                leased = Leased;
                Leased = null;
            }
            foreach (var item in pooled) {
                item.Holder.RealDispose();
            }
            pooled.Clear();
            leased.Clear();
            Creater = null;
            GC.SuppressFinalize(this);
        }
    }


    /// <summary>明示されたオブジェクトを管理するオブジェクトプール</summary>
    public class ObjectPool<THolder> : ObjectPool //
        where THolder : IObjectHolder //
    {
        /// <summary>コンストラクタ</summary>
        public ObjectPool(ObjectHolderCreater creater, int limit) //
            : base(creater, limit) //
        {
        }

        /// <summary>プールからオブジェクトを確保する</summary>
        public new THolder Allocate() {
            return (THolder)base.Allocate();
        }
    }
}


