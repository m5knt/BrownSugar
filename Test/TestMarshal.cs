using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class TestMarshal {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class MarshalClass {
            public byte u8 = 0x88;
            public ushort u16 = 0x8899;
            public uint u32 = 0x8899aabb;
            public ulong u64 = 0x8899aabbccddeeff;
            public sbyte s8 = unchecked((sbyte)0x88);
            public short s16 = unchecked((short)0x8899);
            public int s32 = unchecked((int)0x8899aabb);
            public long s64 = unchecked((long)0x8899aabbccddeeff);
            /**/
            public bool bt = true;
            public bool bf = false;
            public float f32 = 1.1f;
            public double f64 = 1.1;
            public decimal f128 = 1.1m;
            public char c = '@';
            /**/
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "01234567890";
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public double[] array = new double[10];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct MarshalFixed {
            public byte u8;
            public ushort u16;
            public uint u32;
            public ulong u64;
            public sbyte s8;
            public short s16;
            public int s32;
            public long s64;
            /**/
            public bool bt;
            public bool bf;
            public float f32;
            public double f64;
            public decimal f128;
            public char c;
            /**/
            public fixed char str[10];
            public fixed double array[10];

            public static MarshalFixed Create() {
                return new MarshalFixed() {
                    u8 = 0x88,
                    u16 = 0x8899,
                    u32 = 0x8899aabb,
                    u64 = 0x8899aabbccddeeff,
                    s8 = unchecked((sbyte)0x88),
                    s16 = unchecked((short)0x8899),
                    s32 = unchecked((int)0x8899aabb),
                    s64 = unchecked((long)0x8899aabbccddeeff),
                    /**/
                    bt = true,
                    bf = false,
                    f32 = 1.1f,
                    f64 = 1.1,
                    f128 = 1.1m,
                    c = '@',
                };
            }
        }
#if false
        [TestMethod]
        public unsafe void TestMarshalClass() {
            var src = new MarshalClass();
            var srcbin = new byte[src.MarshalSize()];
            HostOrder.Assign(srcbin, 0, src);
            /**/
            var ext = HostOrder.To<MarshalClass>(srcbin, 0);
            var extbin = new byte[ext.MarshalSize()];
            HostOrder.Assign(extbin, 0, ext);
            /**/
            Assert.IsTrue(srcbin.SequenceEqual(extbin));
        }

        [TestMethod]
        public unsafe void TestMarshalFixed() {
            var src = MarshalFixed.Create();
            var srcbin = new byte[src.MarshalSize()];
            HostOrder.Assign(srcbin, 0, src);
            /**/
            var ext = HostOrder.To<MarshalFixed>(srcbin, 0);
            var extbin = new byte[ext.MarshalSize()];
            HostOrder.Assign(extbin, 0, ext);
            /**/
            Assert.IsTrue(srcbin.SequenceEqual(extbin));
        }
#endif
    }
}
