using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class Swap {

        Int16 s16r = unchecked((Int16)0x9988);
        Int32 s32r = unchecked((Int32)0xbbaa9988);
        Int64 s64r = unchecked((Int64)0xffeeddccbbaa9988);
        UInt16 u16r = (UInt16)0x9988;
        UInt32 u32r = (UInt32)0xbbaa9988;
        UInt64 u64r = (UInt64)0xffeeddccbbaa9988;
        sbyte s8 = unchecked((sbyte)0x88);
        Int16 s16 = unchecked((Int16)0x8899);
        Int32 s32 = unchecked((Int32)0x8899aabb);
        Int64 s64 = unchecked((Int64)0x8899aabbccddeeff);
        char c16r = '\x4000';
        char c16 = '\x40';
        byte u8 = unchecked((byte)0x88);
        UInt16 u16 = (UInt16)0x8899;
        UInt32 u32 = (UInt32)0x8899aabb;
        UInt64 u64 = (UInt64)0x8899aabbccddeeff;

        [TestMethod]
        public void Premise() {
            Assert.AreEqual(1, Marshal.SizeOf('0')); // C言語 char 扱い
            Assert.AreEqual(4, Marshal.SizeOf(true)); // Win32 BOOL 扱い
            Assert.AreEqual(1, Marshal.SizeOf(s8));
            Assert.AreEqual(2, Marshal.SizeOf(s16));
            Assert.AreEqual(4, Marshal.SizeOf(s32));
            Assert.AreEqual(8, Marshal.SizeOf(s64));
            Assert.AreEqual(1, Marshal.SizeOf(u8));
            Assert.AreEqual(2, Marshal.SizeOf(u16));
            Assert.AreEqual(4, Marshal.SizeOf(u32));
            Assert.AreEqual(8, Marshal.SizeOf(u64));
            Assert.AreEqual(4, Marshal.SizeOf(1.0f));
            Assert.AreEqual(8, Marshal.SizeOf(1.1));
            Assert.AreEqual(16, Marshal.SizeOf(1.1m));
        }

        [TestMethod]
        public void SwapValues() {
            Assert.AreEqual(s16, ByteOrder.Swap(s16r));
            Assert.AreEqual(s32, ByteOrder.Swap(s32r));
            Assert.AreEqual(s64, ByteOrder.Swap(s64r));
            Assert.AreEqual(c16, ByteOrder.Swap(c16r));
            Assert.AreEqual(u16, ByteOrder.Swap(u16r));
            Assert.AreEqual(u32, ByteOrder.Swap(u32r));
            Assert.AreEqual(u64, ByteOrder.Swap(u64r));
        }

        [TestMethod]
        public void SwapArray() {
            var buf = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee };
            var bufr = (byte[])buf.Clone();
            Array.Reverse(bufr);
            ByteOrder.Swap(buf, 0, buf.Length);
        }

        //
        //
        //

        // メンバーの並びは定義順,詰め系の並び,文字列はUnicode(utf16)
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        class MarshalTypeClass {

            // 文字列は Win32 の TCHAR に相当するが文字型を決定する指定は
            // StructLayout の CharSet になる
            // 文字列はヌル終端サイズはヌル文字を含む
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "あ12345678\0";
            // 配列サイズ変更による事故防止
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
            // ネスト処理の確認
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            MarshalTypeClassSub[] sub = new MarshalTypeClassSub[2];
            /**/
            public char c16 = 'あ';
            public bool bt = true;
            public bool bf = false;
            public float f32 = 1.1f;
            public double f64 = 1.1;
            public decimal f128 = 1.1m;
            /**/
            public byte u8 = (byte)0x88;
            public ushort u16 = (ushort)0x8899;
            public uint u32 = (uint)0x8899aabb;
            public ulong u64 = (ulong)0x8899aabbccddeeff;
            public sbyte s8 = unchecked((sbyte)0x88);
            public short s16 = unchecked((short)0x8899);
            public int s32 = unchecked((int)0x8899aabb);
            public long s64 = unchecked((long)0x8899aabbccddeeff);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        class MarshalTypeClassSub {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "ABCDEFGHI\0";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
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

        [TestMethod]
        public unsafe void SwapMarshal() {
            var src = new MarshalTypeClass();
            var srcbin = new byte[ByteOrder.SizeOf(src)];
            ByteOrder.Assign(srcbin, 0, src);
            /**/
            var ext = ByteOrder.To<MarshalTypeClass>(srcbin, 0);
            var extbin = new byte[ByteOrder.SizeOf(ext)];
            ByteOrder.Assign(extbin, 0, ext);
            /**/
            Assert.IsTrue(srcbin.SequenceEqual(extbin));
            //
            ByteOrder.Swap(srcbin, 0, typeof(MarshalTypeClass));
            ext = ByteOrder.To<MarshalTypeClass>(srcbin, 0);
            ByteOrder.Assign(extbin, 0, ext);
            Assert.AreEqual(src.s16, ByteOrder.Swap(ext.s16));
            Assert.AreEqual(src.s32, ByteOrder.Swap(ext.s32));
            Assert.AreEqual(src.s64, ByteOrder.Swap(ext.s64));
            Assert.AreEqual(src.u16, ByteOrder.Swap(ext.u16));
            Assert.AreEqual(src.u32, ByteOrder.Swap(ext.u32));
            Assert.AreEqual(src.u64, ByteOrder.Swap(ext.u64));
            //
            Assert.AreEqual(src.str[0], ByteOrder.Swap(ext.str[0]));
            Assert.AreEqual(src.c16, ByteOrder.Swap(ext.c16));
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

    }
}
