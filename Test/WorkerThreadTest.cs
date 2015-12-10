using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;

using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;
using System.Threading;

namespace Test {

    [TestClass]
    public class WorkerThreadTest {

        int Worker0Count = 0;
        int Worker1Count = 0;
        int Worker2Count = 0;
        int Worker3Count = 0;

#if false
        void Worker0() {
            Interlocked.Increment(ref Worker0Count);
            Console.WriteLine("worker0 " + Worker0Count);
            Thread.Sleep(1000);
        }

        void Worker1(int state) {
            Interlocked.Increment(ref Worker1Count);
            Console.WriteLine("worker1 " + Worker1Count);
            Thread.Sleep(1000);
        }
#endif
        int Worker2() {
            Interlocked.Increment(ref Worker2Count);
            Console.WriteLine("worker2 " + Worker2Count);
            Thread.Sleep(1000);
            return 0;
        }

        int Worker3(int state) {
            Interlocked.Increment(ref Worker3Count);
            Console.WriteLine("worker3 " + Worker3Count);
            Thread.Sleep(1000);
            return state;
        }

        [TestMethod]
        public void Test() {
            var th = new WorkerThread();
            //
#if false
            {
                var a = th.StartNew(Worker0);
                while (!a.Wait(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(1, Worker0Count);
            }
            {
                var a = th.StartNew(Worker1, 0);
                while (!a.Wait(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(1, Worker1Count);
            }
#endif
            {
                var a = th.StartNew(Worker2);
                while (!a.Wait(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(1, Worker2Count);
            }
            {
                var a = th.StartNew(Worker3, 0);
                while (!a.Wait(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(1, Worker3Count);
            }
        }
    }
}
