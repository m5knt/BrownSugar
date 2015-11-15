using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class ByteOrder_ {

        static Int16 s16r = unchecked((Int16)0x9988);
        static Int32 s32r = unchecked((Int32)0xbbaa9988);
        static Int64 s64r = unchecked((Int64)0xffeeddccbbaa9988);
        static UInt16 u16r = (UInt16)0x9988;
        static UInt32 u32r = (UInt32)0xbbaa9988;
        static UInt64 u64r = (UInt64)0xffeeddccbbaa9988;
        static sbyte s8 = unchecked((sbyte)0x88);
        static Int16 s16 = unchecked((Int16)0x8899);
        static Int32 s32 = unchecked((Int32)0x8899aabb);
        static Int64 s64 = unchecked((Int64)0x8899aabbccddeeff);
        static char c16r = '\x4000';
        static char c16 = '\x40';
        static byte u8 = unchecked((byte)0x88);
        static UInt16 u16 = (UInt16)0x8899;
        static UInt32 u32 = (UInt32)0x8899aabb;
        static UInt64 u64 = (UInt64)0x8899aabbccddeeff;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        class UnicodeType {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "あいうえおかきくけこ";
            public char c16 = '@';
        }

        [TestMethod]
        public void Marshal_() {
            var uni = new UnicodeType();
            // マーシャルではStructLayout情報がない文字は1byte
            Assert.AreEqual(1, Marshal.SizeOf(uni.c16));
            // マーシャル情報(MarshalAs)はUnicodeTypeの型情報なので失敗する
            try {
                Assert.AreEqual(10, Marshal.SizeOf(uni.str));
                Assert.IsTrue(false);
            }
            catch (ArgumentException) {
            }
            // マーシャル情報があればutf16で扱える
            Assert.AreEqual(22, Marshal.SizeOf(typeof(UnicodeType))); 
            Assert.AreEqual(22, Marshal.SizeOf(uni));
            // マーシャルでは bool は Win32 BOOL 扱い
            Assert.AreEqual(4, Marshal.SizeOf(true));

            // 普通
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
            unsafe
            {
                Assert.AreEqual(1, sizeof(bool));
                Assert.AreEqual(2, sizeof(char));
                Assert.AreEqual(1, sizeof(sbyte));
                Assert.AreEqual(2, sizeof(short));
                Assert.AreEqual(4, sizeof(int));
                Assert.AreEqual(8, sizeof(long));
                Assert.AreEqual(1, sizeof(byte));
                Assert.AreEqual(2, sizeof(ushort));
                Assert.AreEqual(4, sizeof(uint));
                Assert.AreEqual(8, sizeof(ulong));
                Assert.AreEqual(4, sizeof(float));
                Assert.AreEqual(8, sizeof(double));
                Assert.AreEqual(16, sizeof(decimal));
            }

        }

        [TestMethod]
        public void SwapValues() {
            Assert.AreEqual(s16, ByteOrder.Swap(s16r));
            Assert.AreEqual(s32, ByteOrder.Swap(s32r));
            Assert.AreEqual(s64, ByteOrder.Swap(s64r));
            Assert.AreEqual(u16, ByteOrder.Swap(u16r));
            Assert.AreEqual(u32, ByteOrder.Swap(u32r));
            Assert.AreEqual(u64, ByteOrder.Swap(u64r));
            Assert.AreEqual(c16, ByteOrder.Swap(c16r));
        }

        [TestMethod]
        public void SwapArray() {
            var buf = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee };
            var bufr = (byte[])buf.Clone();
            Array.Reverse(bufr);
            Assert.IsFalse(buf.SequenceEqual(bufr));
            ByteOrder.Swap(buf, 0, buf.Length);
            Assert.IsTrue(buf.SequenceEqual(bufr));
        }

        [TestMethod]
        public void MarshalOffset() {
            Assert.AreEqual(10, ByteOrder.MarshalOffset(new ClassSub(), "array"));
            Assert.AreEqual(10, ByteOrder.MarshalOffset(typeof(ClassSub), "array"));
        }

        [TestMethod]
        public void MarshalCount() {
            Assert.AreEqual(10, ByteOrder.MarshalCount(new ClassSub(), "str"));
            Assert.AreEqual(10, ByteOrder.MarshalCount(typeof(ClassSub), "str"));
        }

        [TestMethod]
        public void MarshalExtension() {
            Assert.AreEqual(Marshal.SizeOf(new ClassSub()), new ClassSub().MarshalSize());
            Assert.AreEqual(Marshal.SizeOf(typeof(ClassSub)), typeof(ClassSub).MarshalSize());
            Assert.AreEqual(10, new ClassSub().MarshalOffset("array"));
            Assert.AreEqual(10, typeof(ClassSub).MarshalOffset("array"));
            Assert.AreEqual(10, new Class().MarshalCount("str"));
            Assert.AreEqual(10, typeof(Class).MarshalCount("str"));
        }

        //
        //
        //

        // メンバーの並びは定義順,詰め系の並び,文字列はUnicode(utf16)
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        unsafe class Class {

            // 文字列は Win32 の TCHAR に相当するが文字型を決定する指定は
            // StructLayout の CharSet になる
            // 文字列はヌル終端サイズはヌル文字を含む
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "あいうえおかきくけ\0";
            // 配列サイズ変更による事故防止
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public bool[] bools = new bool[array_size] { true, false };
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] doubles = new double[array_size] { 1.1, 1.2 };
            // ネスト処理の確認
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            ClassSub[] sub = new ClassSub[array_size];

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

        // 10 8 8
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        class ClassSub {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "012345678";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
        }

        [TestMethod]
        public unsafe void SwapClass() {
            // マーシャル通して同じ結果になるか
            var src = new Class();
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var ext = ByteOrder.MarshalTo<Class>(srcbin, 0);
            var extbin = new byte[Marshal.SizeOf(ext)];
            ByteOrder.MarshalAssign(extbin, 0, ext);
            Assert.IsTrue(srcbin.SequenceEqual(extbin));

            // マーシャルで反転したものが値を反転したものと同じになるか
            ByteOrder.Swap(srcbin, 0, typeof(Class));
            Assert.IsFalse(srcbin.SequenceEqual(extbin));

            ext = ByteOrder.MarshalTo<Class>(srcbin, 0);
            ByteOrder.MarshalAssign(extbin, 0, ext);
            Assert.AreEqual(src.s16, ByteOrder.Swap(ext.s16));
            Assert.AreEqual(src.s32, ByteOrder.Swap(ext.s32));
            Assert.AreEqual(src.s64, ByteOrder.Swap(ext.s64));
            Assert.AreEqual(src.u16, ByteOrder.Swap(ext.u16));
            Assert.AreEqual(src.u32, ByteOrder.Swap(ext.u32));
            Assert.AreEqual(src.u64, ByteOrder.Swap(ext.u64));
            {
                var n = ByteOrder.MarshalCount(src.GetType(), "str") - 1;
                for (var i = 0; i < n; ++i) {
                    Assert.AreEqual(src.str[i], ByteOrder.Swap(ext.str[i]));
                }
            }
            {
                var n = ByteOrder.MarshalCount(src.GetType(), "bools");
                for (var i = 0; i < n; ++i) {
                    Assert.AreEqual(src.bools[i], ext.bools[i]);
                }
            }
            Assert.AreEqual(src.c16, ByteOrder.Swap(ext.c16));
        }

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
