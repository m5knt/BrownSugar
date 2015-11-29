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
        /// <summary>オブジェクトホルダー</summary>
        public IObjectHolder Holder;
        /// <summary>オブジェクトホルダーへの弱参照</summary>
        public WeakReference Weak;
    }

    /// <summary>オブジェクトプールのオブジェクトを保持するインターフェース</summary>
    public interface IObjectHolder : IDisposable {
        /// <summary>ディスポーズ済みかを返す</summary>
        bool IsDisposed { get; }
        /// <summary>本当のディスポーズ</summary>
        void RealDispose();
        /// <summary>再利用</summary>
        void Reuse();

        /// <summary>オブジェクト管理元</summary>
        IObjectPool Pool { get; set; }
        /// <summary>オブジェクト管理情報</summary>
        ObjectPoolStat PoolStat { get; set; }
    }

    /// <summary>オブジェクトプールのインターフェース</summary>
    public interface IObjectPool : IDisposable {

        /// <summary>ディスポーズ済みかを返す</summary>
        bool IsDisposed { get; }

        /// <summary>プールしているオブジェクト数</summary>
        int Count { get; }

        /// <summary>オブジェクトを確保します</summary>
        IObjectHolder Allocate();

        /// <summary>プールへ戻す</summary>
        /// <returns>成否を返す</returns>
        bool Free(IObjectHolder holder);

        /// <summary>GC回収を知らせる</summary>
        void HolderCollect(IObjectHolder holder);
    }

    //
    //
    //

    /// <summary>オブジェクト生成器</summary>
    public delegate IObjectHolder ObjectHolderCreater();

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

        /// <summary>貸出しているオブジェクト数</summary>
        public int LeasedCount {
            get {
                lock (this) {
                    CheckDisposed();
                    return Leased.Count;
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
                return Lender(node);
            }
            // 新規に作成して貸出
            var stat = new ObjectPoolStat();
            var holder = Creater();
            holder.Pool = this;
            holder.PoolStat = stat;
            stat.Holder = holder;
            stat.Weak = new WeakReference(stat.Holder);
            node = new LinkedListNode<ObjectPoolStat>(stat);
            return Lender(node);
        }

        /// <summary>貸出状態にする</summary>
        IObjectHolder Lender(LinkedListNode<ObjectPoolStat> node) {
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
            lock (this) {
                if (IsDisposed) goto HolderDisposer;
                // 貸出表から消す
                Leased.Remove(holder.PoolStat);
                CheckCollectedHolder();
                // 制限内ならプールへ戻す
                if (Pooled.Count >= Limit) goto HolderDisposer;
                holder.PoolStat.Holder = holder;
                Pooled.AddLast(holder.PoolStat);
                return true;
            }
        HolderDisposer:
            HolderDisposer(holder);
            return false;
        }

        /// <summary>ホルダーをディスポーズをする</summary>
        void HolderDisposer(IObjectHolder holder) {
            holder.PoolStat = null;
            holder.Pool = null;
            holder.RealDispose();
        }

        /// <summary>GC済みを管理から外す</summary>
        void CheckCollectedHolder() {
            lock (this) {
                var n = Leased.GetEnumerator();
                while (n.MoveNext()) {
                    if (!n.Current.Weak.IsAlive) {
                        Leased.Remove(n.Current);
                    }
                }
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
                HolderDisposer(item.Holder);
                item.Holder = null;
                item.Weak = null;
            }
            pooled.Clear();
            leased.Clear();
            Creater = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>ホルダーのGC回収を知らてもらう</summary>
        public void HolderCollect(IObjectHolder holder) {
            lock (this) {
                if (IsDisposed) return;
                holder.PoolStat.Weak = null;
                Leased.Remove(holder.PoolStat);
            }
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

    //
    //
    //

    /// <summary>オブジェクトプール用のオブジェクトを保持</summary>
    public abstract class ObjectHolderBase : IObjectHolder //
    {
        /// <summary>ディスポーズされているか</summary>
        public bool IsDisposed { get; protected set; }

        /// <summary>管理元プール</summary>
        public IObjectPool Pool { get; set; }

        /// <summary>管理キー</summary>
        public ObjectPoolStat PoolStat { get; set; }

        /// <summary>コンストラクタ</summary>
        protected ObjectHolderBase() {
            IsDisposed = true;
        }

        /// <summary>ファイナライザ</summary>
        ~ObjectHolderBase() {
            if (Pool != null) {
                Pool.HolderCollect(this);
            }
        }

        /// <summary>再利用</summary>
        public void Reuse() {
            IsDisposed = false;
        }

        /// <summary>ディスポーズ</summary>
		public void Dispose() {
            if (IsDisposed) {
                return;
            }
            IsDisposed = true;
            // プールがあるなら返す
            if (Pool.Free(this)) {
                return;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>本当のディスポーズ</summary>
        public abstract void RealDispose();
    }

    /// <summary>オブジェクトプール用のオブジェクトを保持</summary>
    public class ObjectHolder<TValue> : ObjectHolderBase //
        where TValue : class //
    {
        /// <summary>保持するオブジェクト</summary>
        public TValue Value { get; protected set; }

        /// <summary>コンストラクタ</summary>
        public ObjectHolder(TValue val) {
            Value = val;
        }

        /// <summary>本当のディスポーズ</summary>
        public override void RealDispose() {
            using (var dis = Value as IDisposable) { }
            Value = null;
        }
    }
}


