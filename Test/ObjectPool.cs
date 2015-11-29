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
            () => {
                return new Holder(new byte[1500]);
            }, 2);

        public class Holder : ObjectHolder<byte[]> {
            public Holder(byte[] value) : base(value) { }
            ~Holder() {
                Interlocked.Increment(ref collect_count);
            }
        }

        void ObjectPoolSub() {
            Holder one, two, three, four;

            one = pool.Allocate(); // pool 0
            Assert.AreEqual(0, pool.Count);
            one.Dispose(); // pool 1
            Assert.AreEqual(1, pool.Count);
            //
            two = pool.Allocate(); // pool 0 再利用
            Assert.AreSame(one, two);
            Assert.AreEqual(0, pool.Count);
            //
            three = pool.Allocate();　// pool 0　新規
            Assert.AreNotSame(three, two);
            four = pool.Allocate();　// pool 0　新規
            //
            two.Dispose(); // pool 1
            Assert.AreEqual(1, pool.Count);
            three.Dispose(); // pool 2
            Assert.AreEqual(2, pool.Count);
            four.Dispose(); // pool 2
            Assert.AreEqual(2, pool.Count);
            // for gc collect
            var t = pool.Allocate();
        }

        [TestMethod]
        public void ObjectPool() {
            ObjectPoolSub();
            Assert.AreEqual(0, collect_count);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Assert.AreEqual(1, collect_count);
        }
    }

}

