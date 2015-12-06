using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public partial class ByteOrderTest {

        [TestMethod]
        public void SwapArray() {
            var buf = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee };
            var bufr = new byte[] { 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x99, 0x88 };
            ByteOrder.Swap(buf, 0, buf.Length);
            Assert.IsTrue(buf.SequenceEqual(bufr));
            //
            buf = new byte[] { 0x88, 0x99, 0xaa };
            bufr = new byte[] { 0xee, 0xaa, 0x99 };
            ByteOrder.Swap(buf, 1, 2);
            Assert.IsTrue(buf.Skip(1).Take(2).SequenceEqual(bufr.Skip(1).Take(2)));
        }
    }
}
