using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class Marshal_ {

        // 10 8 8 1
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        class AnsiType {
            // null 含みの数
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "012345678";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
            public char c16 = '@';
        }

        // 20 8 8 2
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        class UnicodeType {
            // null 含みの数
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "012345678";
            const int array_size = 2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = array_size)]
            public double[] array = new double[array_size] { 1.1, 1.2 };
            public char c16 = '@';
        }

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

        [TestMethod]
        public void Marshaling() {
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
            Assert.AreEqual(38, Marshal.SizeOf(typeof(UnicodeType)));
            Assert.AreEqual(38, Marshal.SizeOf(uni));
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
        public void MarshalExtension() {
            // size
            Assert.AreEqual(27, new AnsiType().MarshalSize());
            Assert.AreEqual(27, typeof(AnsiType).MarshalSize());
            Assert.AreEqual(38, new UnicodeType().MarshalSize());
            Assert.AreEqual(38, typeof(UnicodeType).MarshalSize());
            Assert.AreEqual(Marshal.SizeOf(new AnsiType()), new AnsiType().MarshalSize());
            Assert.AreEqual(Marshal.SizeOf(typeof(AnsiType)), typeof(AnsiType).MarshalSize());
            Assert.AreEqual(Marshal.SizeOf(new UnicodeType()), new UnicodeType().MarshalSize());
            Assert.AreEqual(Marshal.SizeOf(typeof(UnicodeType)), typeof(UnicodeType).MarshalSize());
            // offset
            Assert.AreEqual(10, new AnsiType().MarshalOffset("array"));
            Assert.AreEqual(10, typeof(AnsiType).MarshalOffset("array"));
            Assert.AreEqual(20, new UnicodeType().MarshalOffset("array"));
            Assert.AreEqual(20, typeof(UnicodeType).MarshalOffset("array"));
            // count
            Assert.AreEqual(10, new AnsiType().MarshalCount("str"));
            Assert.AreEqual(10, typeof(AnsiType).MarshalCount("str"));
            Assert.AreEqual(2, new AnsiType().MarshalCount("array"));
            Assert.AreEqual(2, typeof(AnsiType).MarshalCount("array"));
        }


    }
}

