using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    public partial class ByteOrderTest : Values {

        [TestMethod]
        public void SwapPrimitive() {
            Assert.AreEqual(s16, ByteOrder.Swap(s16r));
            Assert.AreEqual(s32, ByteOrder.Swap(s32r));
            Assert.AreEqual(s64, ByteOrder.Swap(s64r));
            Assert.AreEqual(u16, ByteOrder.Swap(u16r));
            Assert.AreEqual(u32, ByteOrder.Swap(u32r));
            Assert.AreEqual(u64, ByteOrder.Swap(u64r));
            Assert.AreEqual(c16, ByteOrder.Swap(c16r));
        }
    }
}
