using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class Marshal_ : Values {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class MarshalEnum {
            EnumU8 u8 = EnumU8.Value;
            EnumU16 u16 = EnumU16.Value;
            EnumU32 u32 = EnumU32.Value;
            EnumU64 u64 = EnumU64.Value;
        }

        [TestMethod]
        public void Marshaling() {
            var uni = new UnicodeType();

            // マーシャル情報があればutf16で扱える
            Assert.AreEqual(38, Marshal.SizeOf(typeof(UnicodeType)));
            Assert.AreEqual(38, Marshal.SizeOf(uni));
            Assert.AreEqual(1, Marshal.SizeOf(s8));
            Assert.AreEqual(2, Marshal.SizeOf(s16));
            Assert.AreEqual(4, Marshal.SizeOf(s32));
            Assert.AreEqual(8, Marshal.SizeOf(s64));
            Assert.AreEqual(1, Marshal.SizeOf(u8));
            Assert.AreEqual(2, Marshal.SizeOf(u16));
            Assert.AreEqual(4, Marshal.SizeOf(u32));
            Assert.AreEqual(8, Marshal.SizeOf(u64));
            /**/
            Assert.AreEqual(4, Marshal.SizeOf(true));
            // マーシャルではStructLayout情報がない文字は1byte
            Assert.AreEqual(1, Marshal.SizeOf(uni.c16));
            // マーシャル情報(MarshalAs)はUnicodeTypeの型情報なので失敗する
            try {
                Assert.AreEqual(10, Marshal.SizeOf(uni.str));
                Assert.IsTrue(false);
            }
            catch (ArgumentException) {
            }
            Assert.AreEqual(4, Marshal.SizeOf(1.0f));
            Assert.AreEqual(8, Marshal.SizeOf(1.1));
            Assert.AreEqual(16, Marshal.SizeOf(1.1m));
            Assert.AreEqual(15, Marshal.SizeOf(new MarshalEnum()));
            try {
                Assert.AreEqual(1, Marshal.SizeOf(EnumU8.Value));
                Assert.IsTrue(false);
            }
            catch (ArgumentException) {
            }
            unsafe
            {
                Assert.AreEqual(2, sizeof(short));
                Assert.AreEqual(4, sizeof(int));
                Assert.AreEqual(8, sizeof(long));
                Assert.AreEqual(1, sizeof(byte));
                Assert.AreEqual(2, sizeof(ushort));
                Assert.AreEqual(4, sizeof(uint));
                Assert.AreEqual(8, sizeof(ulong));
                /**/
                Assert.AreEqual(1, sizeof(bool));
                Assert.AreEqual(2, sizeof(char));
                Assert.AreEqual(1, sizeof(sbyte));
                Assert.AreEqual(4, sizeof(float));
                Assert.AreEqual(8, sizeof(double));
                Assert.AreEqual(16, sizeof(decimal));
                Assert.AreEqual(1, sizeof(EnumU8));
                Assert.AreEqual(2, sizeof(EnumU16));
                Assert.AreEqual(4, sizeof(EnumU32));
                Assert.AreEqual(8, sizeof(EnumU64));
                Assert.AreEqual(1, sizeof(EnumS8));
                Assert.AreEqual(2, sizeof(EnumS16));
                Assert.AreEqual(4, sizeof(EnumS32));
                Assert.AreEqual(8, sizeof(EnumS64));
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

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        abstract class MarshalBase {
            public byte bvalue0;
            public abstract byte property0 { get; set; }
            public byte bvalue1;
            public abstract byte property1 { get; set; }
            public MarshalBase() {
                bvalue0 = 0x11;
                bvalue1 = 0x22;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class MarshalDerived : MarshalBase {
            public byte dvalue0;
            public override byte property0 { get; set; }
            public byte dvalue1;
            public override byte property1 { get; set; }
            public MarshalDerived() {
                dvalue0 = 0x33;
                property0 = 0x44;
                dvalue1 = 0x55;
                property1 = 0x66;
            }
        }

        [TestMethod]
        public void MarshalInherit() {
            var d = new MarshalDerived();
            var b = (MarshalBase)d;
            Assert.AreEqual(0, b.MarshalOffset("bvalue0"));
            Assert.AreEqual(1, b.MarshalOffset("bvalue1"));
            Assert.AreEqual(2, d.MarshalOffset("dvalue0"));
            Assert.AreEqual(3, d.MarshalOffset("property0".ToBackingField()));
            Assert.AreEqual(4, d.MarshalOffset("dvalue1"));
            Assert.AreEqual(5, d.MarshalOffset("property1".ToBackingField()));
            var bytes = ByteOrder.MarshalGetBytes(d);
            ByteOrder.Swap(bytes, 0, typeof(MarshalDerived));
            Assert.AreEqual(bytes[0], 0x11);
            Assert.AreEqual(bytes[1], 0x22);
            Assert.AreEqual(bytes[2], 0x33);
            Assert.AreEqual(bytes[3], 0x44);
            Assert.AreEqual(bytes[4], 0x55);
            Assert.AreEqual(bytes[5], 0x66);
        }
    }
}

