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

            public void Init() {
                fixed (Struct* p = &this)
                {
                    p->fix[0] = u32;
                    p->fix[1] = u32r;
                }
            }
        }

        [TestMethod]
        public unsafe void SwapStruct() {
            // マーシャル通して同じ結果になるか
            Struct src;
            src.Init();
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var ext = ByteOrder.MarshalTo<Struct>(srcbin, 0);
            var extbin = new byte[Marshal.SizeOf(ext)];
            ByteOrder.MarshalAssign(extbin, 0, ext);
            Assert.IsTrue(srcbin.SequenceEqual(extbin));

            ByteOrder.Swap(srcbin, 0, typeof(Struct));
            ext = ByteOrder.MarshalTo<Struct>(srcbin, 0);
            ByteOrder.MarshalAssign(extbin, 0, ext);

            Assert.AreEqual(src.fix[0], ByteOrder.Swap(ext.fix[0]));
            Assert.AreEqual(ext.fix[0], u32r);
        }

    }
}
