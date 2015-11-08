using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class Marshal {

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public class MarshalTypeClassSub {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "0123456789";
            const int array_size = 1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        public class MarshalTypeClass {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "あ12345678\0";
            const int array_size = 1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[] { 1.1 };
            /**/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            MarshalTypeClassSub[] sub = new MarshalTypeClassSub[2];
            /**/
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
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct MarshalTypeFixed {
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

            public static MarshalTypeFixed Create() {
                return new MarshalTypeFixed() {
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
#if true
        [TestMethod]
        public unsafe void TestMarshalClass() {
            var src = new MarshalTypeClass();
            var srcbin = new byte[ByteOrder.SizeOf(src)];
            ByteOrder.Assign(srcbin, 0, src);
            ByteOrder.Swap(srcbin, 0, typeof(MarshalTypeClass));
            ByteOrder.Swap(srcbin, 0, typeof(MarshalTypeClass));
            /**/
            var ext = ByteOrder.To<MarshalTypeClass>(srcbin, 0);
            var extbin = new byte[ByteOrder.SizeOf(ext)];
            ByteOrder.Assign(extbin, 0, ext);
            /**/
            Assert.IsTrue(srcbin.SequenceEqual(extbin));
        }

        [TestMethod]
        public unsafe void TestMarshalFixed() {
            var src = MarshalTypeFixed.Create();
            var srcbin = new byte[ByteOrder.SizeOf(src)];
            ByteOrder.Assign(srcbin, 0, src);
            ByteOrder.Swap(srcbin, 0, typeof(MarshalTypeFixed));
            ByteOrder.Swap(srcbin, 0, typeof(MarshalTypeFixed));
            /**/
            var ext = ByteOrder.To<MarshalTypeFixed>(srcbin, 0);
            var extbin = new byte[ByteOrder.SizeOf(ext)];
            ByteOrder.Assign(extbin, 0, ext);
            /**/
            Assert.IsTrue(srcbin.SequenceEqual(extbin));
        }
#endif
    }
}
