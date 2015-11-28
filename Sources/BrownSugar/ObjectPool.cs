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

    /// <summary>オブジェクトプールのインターフェース</summary>
    public interface IObjectPool : IDisposable {
        /// <summary>オブジェクトを確保します</summary>
        IObjectHolder Allocate();
        /// <summary>プールへ戻す</summary>
        /// <returns>成否を返す</returns>
        bool Free(IObjectHolder holder);
    }

    /// <summary>オブジェクトプールのオブジェクトを保持するインターフェース</summary>
    public interface IObjectHolder : IDisposable {
        /// <summary>ディスポーズ済みかを返す</summary>
        bool IsDisposed { get; }
        /// <summary>本当のディスポーズ</summary>
        void RealDispose();
        /// <summary>オブジェクト管理情報</summary>
        ObjectPoolStat Stat {get;set;}
    }

    /// <summary>オブジェクトプール用のオブジェクトを保持</summary>
    public class ObjectHolder<TValue> : IObjectHolder //
        where TValue : class //
    {
        /// <summary>保持するオブジェクト</summary>
        public TValue Value { get; protected set; }

        /// <summary>ディスポーズされているか</summary>
        public bool IsDisposed {
            get { return Value != null; }
            set { Value = null; }
        }

        /// <summary>管理元プール</summary>
        IObjectPool Pool;

        /// <summary>管理キー</summary>
        public ObjectPoolStat Stat { get; set; }

        //
        //
        //

        /// <summary>コンストラクタ</summary>
        public ObjectHolder(IObjectPool pool, ObjectPoolStat stat, TValue val) {
            Pool = pool;
            Stat = stat;
            Value = null;
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
            // プールがディスポーズされているならディスポーズ
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
        }

        /// <summary>ディスポーズ済みかを返す</summary>
        volatile bool IsDisposed_;

        /// <summary>プール中</summary>
        LinkedList<ObjectPoolStat> Pooled;
        object PooledLock = new object();
        /// <summary>貸出中</summary>
        LinkedList<ObjectPoolStat> Leased;

        /// <summary>オブジェクト生成器</summary>
        Func<IObjectPool, IObjectHolder> Creater;
        /// <summary>保持量</summary>
        int Limit;

        /// <summary>コンストラクタ</summary>
        public ObjectPool(Func<IObjectPool, IObjectHolder> creater,
            int limit) //
        {
            Pooled = new LinkedList<ObjectPoolStat>();
            Leased = new LinkedList<ObjectPoolStat>();
            Limit = limit;
            Creater = creater;
        }

        /// <summary>プールからオブジェクトを確保する</summary>
        public IObjectHolder Allocate() {
            LinkedListNode<ObjectPoolStat> node = null;
            // プールから取得を試みる
            lock(this) {
                // ディスポーズ済みなら例外
                if (IsDisposed) {
                    throw new ObjectDisposedException("disposed");
                }
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
            var holder = Creater(this);
            var weak = new WeakReference(holder);
            var stat = new ObjectPoolStat() { Holder = holder, Weak = weak};
            node = new LinkedListNode<ObjectPoolStat>(stat);
            return DoLeased(node);
        }

        /// <summary>貸出状態にする</summary>
        IObjectHolder DoLeased(LinkedListNode<ObjectPoolStat> node) {
            // 未返却対応で弱監視にする
            var holder = node.Value.Holder;
            node.Value.Holder = null;
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
                if (Pooled.Count > Limit) return false;
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
}


