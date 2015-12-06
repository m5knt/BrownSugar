using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    public partial class ByteOrderTest {
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        unsafe struct Struct {
            // fixed は struct のみ
            public fixed UInt32 fix[2];

            public Struct(int n) {
                fixed (Struct* p = &this)
                {
                    p->fix[0] = u32;
                    p->fix[1] = u32r;
                }
            }
        }

        [TestMethod]
        public unsafe void SwapStruct() {
            var src = new Struct(0);
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var extbin = (byte[])srcbin.Clone();
            ByteOrder.Swap(extbin, 0, typeof(Struct));
            var ext = ByteOrder.MarshalTo<Struct>(extbin, 0);

            Assert.AreEqual(u32r, ext.fix[0]);
            Assert.AreEqual(u32, ext.fix[1]);
        }

    }
}
