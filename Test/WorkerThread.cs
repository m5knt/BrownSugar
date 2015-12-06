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
    public class WorkerThread_ {

        volatile int Worker1Count = 0;
        volatile int Worker2Count = 0;
        volatile int CallBackCount = 0;

        public void Worker1(object state) {
            Interlocked.Increment(ref Worker1Count);
            Console.WriteLine("worker1 " + Worker1Count);
        }

        public void Worker2(object state) {
            Interlocked.Increment(ref Worker2Count);
            Console.WriteLine("worker2 " + Worker2Count);
        }

        [TestMethod]
        public void Test() {
            var th = new WorkerThread(Worker1);
            //
            {
                var a = th.Start();
                while (!a.AsyncWaitHandle.WaitOne(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(1, Worker1Count);
            }
            //
            th.Job += Worker2;
            {
                var a = th.Start(_=> {
                    // ここはメインスレッドではありません
                    Interlocked.Increment(ref CallBackCount);
                    Console.WriteLine("callback " + CallBackCount);
                });
                while (!a.AsyncWaitHandle.WaitOne(0)) {
                    Thread.Sleep(1);
                }
                Assert.AreEqual(2, Worker1Count);
                Assert.AreEqual(1, Worker2Count);
                Assert.AreEqual(1, CallBackCount);
            }
        }
    }
}
