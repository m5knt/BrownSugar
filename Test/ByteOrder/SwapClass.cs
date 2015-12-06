using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    public partial class ByteOrderTest : Values {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        unsafe class Class {
            public bool bt = true;
            public bool bf = false;
            public float f32 = 1.1f;
            public double f64 = 1.1;
            /**/
            public byte u8 = (byte)0x88;
            public ushort u16 = (ushort)0x8899;
            public uint u32 = (uint)0x8899aabb;
            public ulong u64 = (ulong)0x8899aabbccddeeff;
            public sbyte s8 = unchecked((sbyte)0x88);
            public short s16 = unchecked((short)0x8899);
            public int s32 = unchecked((int)0x8899aabb);
            public long s64 = unchecked((long)0x8899aabbccddeeff);
            /**/
            public EnumU8 eu8 = EnumU8.Value;
            public EnumU16 eu16 = EnumU16.Value;
            public EnumU32 eu32 = EnumU32.Value;
            public EnumU64 eu64 = EnumU64.Value;
            public EnumS8 es8 = EnumS8.Value;
            public EnumS16 es16 = EnumS16.Value;
            public EnumS32 es32 = EnumS32.Value;
            public EnumS64 es64 = EnumS64.Value;
        }

        [TestMethod]
        public unsafe void SwapClass() {
            // マーシャル通して同じ結果になるか
            var src = new Class();
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var extbin = (byte[])srcbin.Clone();
            ByteOrder.Swap(extbin, 0, typeof(Class));
            var ext = ByteOrder.MarshalTo<Class>(extbin, 0);

            Assert.AreEqual(src.bt, true);
            Assert.AreEqual(src.bf, false);
            Assert.AreEqual(src.s8, unchecked((sbyte)0x88));
            Assert.AreEqual(src.u8, unchecked((byte)0x88));
            Assert.AreEqual(src.eu8, EnumU8.Value);
            Assert.AreEqual(src.es8, EnumS8.Value);

            Assert.AreEqual(src.s8, ext.s8);
            Assert.AreEqual(src.s16, ByteOrder.Swap(ext.s16));
            Assert.AreEqual(src.s32, ByteOrder.Swap(ext.s32));
            Assert.AreEqual(src.s64, ByteOrder.Swap(ext.s64));
            Assert.AreEqual(src.u8, ext.u8);
            Assert.AreEqual(src.u16, ByteOrder.Swap(ext.u16));
            Assert.AreEqual(src.u32, ByteOrder.Swap(ext.u32));
            Assert.AreEqual(src.u64, ByteOrder.Swap(ext.u64));

            Assert.AreEqual((byte)src.eu8, (byte)ext.eu8);
            Assert.AreEqual((UInt16)src.eu16, ByteOrder.Swap((UInt16)ext.eu16));
            Assert.AreEqual((UInt32)src.eu32, ByteOrder.Swap((UInt32)ext.eu32));
            Assert.AreEqual((UInt64)src.eu64, ByteOrder.Swap((UInt64)ext.eu64));
            Assert.AreEqual((sbyte)src.es8, (sbyte)ext.es8);
            Assert.AreEqual((Int16)src.es16, ByteOrder.Swap((Int16)ext.es16));
            Assert.AreEqual((Int32)src.es32, ByteOrder.Swap((Int32)ext.es32));
            Assert.AreEqual((Int64)src.es64, ByteOrder.Swap((Int64)ext.es64));
        }
    }
}
