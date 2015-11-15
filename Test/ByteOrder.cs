using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class ByteOrder_ : Values {

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
            AnsiType[] sub = new AnsiType[array_size];

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
                var n = src.MarshalCount("str") - 1;
                for (var i = 0; i < n; ++i) {
                    Assert.AreEqual(src.str[i], ByteOrder.Swap(ext.str[i]));
                }
            }
            {
                var n = src.MarshalCount("bools");
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
