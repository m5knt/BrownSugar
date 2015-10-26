using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using System.Collections.Generic;

namespace Test {

    [TestClass]
    public class TestByteSwap {
#if false
        [TestMethod]
        public void TestSwapByteOrder() {
            Assert.AreEqual(((ushort)0x8899).SwapByteOrder(), (ushort)0x9988);
            Assert.AreEqual(((uint)0x8899aabb).SwapByteOrder(), (uint)0xbbaa9988);
            Assert.AreEqual(((ulong)0x8899aabbccddeeff).SwapByteOrder(), (ulong)0xffeeddccbbaa9988);
            Assert.AreEqual(unchecked((short)0x8899).SwapByteOrder(), unchecked((short)0x9988));
            Assert.AreEqual(unchecked((int)0x8899aabb).SwapByteOrder(), unchecked((int)0xbbaa9988));
            Assert.AreEqual(unchecked((long)0x8899aabbccddeeff).SwapByteOrder(), unchecked((long)0xffeeddccbbaa9988));
        }

        [TestMethod]
        public void PerfSwapByteOrderShort() {
                var std = Bench.Run(5, (n) => {
                    IPAddress.HostToNetworkOrder(unchecked((short)n));
                });
                var alt = Bench.Run(5, (n) => {
                    unchecked((short)n).SwapByteOrder();
                });
                Assert.IsTrue(alt >= std);
            }

        [TestMethod]
        public void PerfSwapByteOrderInt() {
            var std = Bench.Run(5, (n) => {
                IPAddress.HostToNetworkOrder(unchecked((int)n));
            });
            var alt = Bench.Run(5, (n) => {
                unchecked((int)n).SwapByteOrder();
            });
            Assert.IsTrue(alt >= std);
        }

        [TestMethod]
        public void PerfSwapByteOrderLong() {
            var std = Bench.Run(5, (n) => {
                IPAddress.HostToNetworkOrder(unchecked((long)n));
            });
            var alt = Bench.Run(5, (n) => {
                unchecked((long)n).SwapByteOrder();
            });
            Assert.IsTrue(alt >= std);
        }
#endif
    }
}
