using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class ObjectPool_ {

        static int collect_count = 0;

        ObjectPool<Holder> pool = new ObjectPool<Holder>(
            () => new Holder(), 2);

        public class Holder : ObjectHolder<byte[]> {
            public Holder() : base(new byte[1500])
            {
            }

            ~Holder() {
                Interlocked.Increment(ref collect_count);
            }
        }

        void ObjectPoolSub() {
            Holder one, two, three, four;

            // pool 0, lease 1
            one = pool.Allocate();
            Assert.AreEqual(1, pool.LeasedCount);
            Assert.AreEqual(0, pool.Count);
            // pool 1, lease 0
            one.Dispose();
            Assert.AreEqual(0, pool.LeasedCount);
            Assert.AreEqual(1, pool.Count);
            // pool 0, lease 1 再利用
            two = pool.Allocate();
            Assert.AreSame(one, two);
            Assert.AreEqual(1, pool.LeasedCount);
            Assert.AreEqual(0, pool.Count);
            // pool 0, lease 2 新規
            three = pool.Allocate();
            Assert.AreEqual(2, pool.LeasedCount);
            Assert.AreEqual(0, pool.Count);
            Assert.AreNotSame(three, two);
            // pool 0, lease 3 新規
            four = pool.Allocate();
            Assert.AreEqual(3, pool.LeasedCount);
            Assert.AreEqual(0, pool.Count);

            // pool 1, lease 2
            two.Dispose();
            Assert.AreEqual(2, pool.LeasedCount);
            Assert.AreEqual(1, pool.Count);
            // pool 2, lease 2
            three.Dispose();
            Assert.AreEqual(1, pool.LeasedCount);
            Assert.AreEqual(2, pool.Count);
            // pool 2, lease 1
            four.Dispose();
            Assert.AreEqual(0, pool.LeasedCount);
            Assert.AreEqual(2, pool.Count);
            // pool 1, lease 1, gc 0, for gc collect 
            var t = pool.Allocate();
            Assert.AreEqual(1, pool.Count);
            Assert.AreEqual(1, pool.LeasedCount);
        }

        [TestMethod]
        public void ObjectPool() {
            ObjectPoolSub();
            Assert.AreEqual(0, collect_count);
            // pool 1, lease 0, gc 1
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Assert.AreEqual(1, collect_count);
            Assert.AreEqual(0, pool.LeasedCount);
            Assert.AreEqual(1, pool.Count);
            // gc 2
            pool.Dispose();
            pool = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Assert.AreEqual(2, collect_count);
        }
    }

}

