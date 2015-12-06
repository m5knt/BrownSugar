using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;
using System.Collections.Generic;

namespace Test {

    public partial class ByteOrderTest : Values {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct NestStruct {
            public ushort u16;
            public uint u32;
            //public Sub() {
            //}
            public NestStruct(int m) {
                u16 = (ushort)0x8899;
                u32 = (uint)0x8899aabb;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class NestClass {
            public ushort u16;
            public uint u32;
            public NestClass() {
            }
            public NestClass(int m) {
                u16 = (ushort)0x8899;
                u32 = (uint)0x8899aabb;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class Nest {
            public ushort u16;
            public uint u32;
            public NestClass nclass;
            public NestStruct nstruct;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
            public NestStruct[] nstructarray;
            // クラスはダメ
//            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
//            public NestClass[] nstructclass;

            public Nest() {
                nclass = new NestClass();
                nstruct = new NestStruct();
                nstructarray = new NestStruct[1];
//                nstructclass = new NestClass[1];
            }
            public Nest(int m) {
                u16 = (ushort)0x8899;
                u32 = (uint)0x8899aabb;
                nclass = new NestClass(m);
                nstruct = new NestStruct(m);
                nstructarray = new NestStruct[1] { new NestStruct(m) };
//                nstructclass = new NestClass[1] { new NestClass(m) };
            }
        }

        [TestMethod]
        public unsafe void SwapNest() {
            var src = new Nest(0);
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var ext = ByteOrder.MarshalTo<Nest>(srcbin, 0);
            var extbin = (byte[])srcbin.Clone();
            ByteOrder.Swap(extbin, 0, typeof(Nest));
            ext = ByteOrder.MarshalTo<Nest>(extbin, 0);

            Assert.AreEqual(src.u16, ByteOrder.Swap(ext.u16));
            Assert.AreEqual(src.u32, ByteOrder.Swap(ext.u32));
            Assert.AreEqual(src.nclass.u16, ByteOrder.Swap(ext.nclass.u16));
            Assert.AreEqual(src.nclass.u32, ByteOrder.Swap(ext.nclass.u32));
            Assert.AreEqual(src.nstruct.u16, ByteOrder.Swap(ext.nstruct.u16));
            Assert.AreEqual(src.nstruct.u32, ByteOrder.Swap(ext.nstruct.u32));
            Assert.AreEqual(src.nstructarray[0].u16, ByteOrder.Swap(ext.nstructarray[0].u16));
            Assert.AreEqual(src.nstructarray[0].u32, ByteOrder.Swap(ext.nstructarray[0].u32));
        }
    }
}
