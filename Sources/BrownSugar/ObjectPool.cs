

/**
 * @file
 * @brief メモリーキャッシュ関係
 */

using System;
using System.Collections.Generic;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

	public class ObjectHolder<TValue> : IDisposable //
		where TValue : class //
	{
		public TValue Value { get; protected set; }

		TValue Shadow;
		ObjectPool<TValue> Pool;

		public ObjectHolder(ObjectPool<TValue> pool, TValue val) {
			Pool = pool;
			Shadow = val;
		}

		public void Dispose() {
			if (Value == null) {
				return;
			}
			Value = null;
			if (Pool.IsDispose) {
				RealDispose();
			}
			else {
				Pool.Free(this);
			}
		}

		public void RealDispose() {
			using (var dis = Shadow as IDisposable) {
			}
			Shadow = null;
			Pool = null;
		}

		public void ReUse(ObjectPool<TValue> n) {
			Value = Shadow;
		}
	}

	//
	//
	//

	public class ObjectPool<TValue>
		where TValue : class //
	{
		Object Object = new object();
		Queue<ObjectHolder<TValue>> Frees;
		Queue<ObjectHolder<TValue>> Allocs;
		int Capacity;
		Func<TValue> Creater;

		public ObjectPool(Func<TValue> creater, int capacity) {
			Frees = new Queue<ObjectHolder<TValue>>();
			Allocs = new Queue<ObjectHolder<TValue>>();
			Capacity = capacity;
			Creater = creater;
		}

		public ObjectHolder<TValue> Allocate() {
			lock (Object) {
				ObjectHolder<TValue> holder;
				if (Frees.Count > 0) {
					holder = Frees.Dequeue();
				}
				else {
					holder = new ObjectHolder<TValue>(this, Creater());
				}
				Allocs.Enqueue(holder);
				holder.ReUse(this);
				return holder;
			}
		}

		public void Free(ObjectHolder<TValue> holder) {
			lock (Object) {
				Frees.Enqueue(holder);
			}
		}

		public void Dispose() {
			if (Object == null) {
				return;
			}
			lock (Object) {
				foreach (var n in Frees) {
					n.RealDispose();
				}
				Allocs = null;
				Frees = null;
				Creater = null;
			}
			Object = null;
			GC.SuppressFinalize(this);
		}

		public bool IsDispose {
			get {
				return Object == null;
			}
		}
	}
}
//
//
//

