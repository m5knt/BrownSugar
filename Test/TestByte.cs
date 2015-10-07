using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThunderEgg.BrownSugar.Byte;

namespace Test {
    [TestClass]
    public class TestByte {

        [TestMethod]
        public void TestSwapByteOrder() {
            Assert.AreEqual(((byte)0x88).SwapByteOrder(), (byte)0x88);
            Assert.AreEqual(((ushort)0x8899).SwapByteOrder(), (ushort)0x9988);
            Assert.AreEqual(((uint)0x8899aabb).SwapByteOrder(), (uint)0xbbaa9988);
            Assert.AreEqual(((ulong)0x8899aabbccddeeff).SwapByteOrder(), (ulong)0xffeeddccbbaa9988);
            Assert.AreEqual(unchecked((sbyte)0x88).SwapByteOrder(), unchecked((sbyte)0x88));
            Assert.AreEqual(unchecked((short)0x8899).SwapByteOrder(), unchecked((short)0x9988));
            Assert.AreEqual(unchecked((int)0x8899aabb).SwapByteOrder(), unchecked((int)0xbbaa9988));
            Assert.AreEqual(unchecked((long)0x8899aabbccddeeff).SwapByteOrder(), unchecked((long)0xffeeddccbbaa9988));
        }

        [TestMethod]
        public void TestBigEndian() {
            var src = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            Assert.AreEqual(BigEndian.ToUInt8(src, 0), (byte)0x88);
            Assert.AreEqual(BigEndian.ToUInt16(src, 0), (ushort)0x8899);
            Assert.AreEqual(BigEndian.ToUInt32(src, 0), (uint)0x8899aabb);
            Assert.AreEqual(BigEndian.ToUInt64(src, 0), (ulong)0x8899aabbccddeeff);
            Assert.AreEqual(BigEndian.ToInt8(src, 0), unchecked((sbyte)0x88));
            Assert.AreEqual(BigEndian.ToInt16(src, 0), unchecked((short)0x8899));
            Assert.AreEqual(BigEndian.ToInt32(src, 0), unchecked((int)0x8899aabb));
            Assert.AreEqual(BigEndian.ToInt64(src, 0), unchecked((long)0x8899aabbccddeeff));

            byte[] tmp;
            tmp = new byte[1];
            BigEndian.Assign(tmp, 0, (byte)0x88);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(1)));
            tmp = new byte[2];
            BigEndian.Assign(tmp, 0, (ushort)0x8899);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(2)));
            tmp = new byte[4];
            BigEndian.Assign(tmp, 0, (uint)0x8899aabb);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(4)));
            tmp = new byte[8];
            BigEndian.Assign(tmp, 0, (ulong)0x8899aabbccddeeff);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(8)));
            tmp = new byte[1];

            BigEndian.Assign(tmp, 0, unchecked((sbyte)0x88));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(1)));
            tmp = new byte[2];
            BigEndian.Assign(tmp, 0, unchecked((short)0x8899));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(2)));
            tmp = new byte[4];
            BigEndian.Assign(tmp, 0, unchecked((int)0x8899aabb));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(4)));
            tmp = new byte[8];
            BigEndian.Assign(tmp, 0, unchecked((long)0x8899aabbccddeeff));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(8)));
        }

        [TestMethod]
        public void TestLitleEndian() {
            var src = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            Assert.AreEqual(LittleEndian.ToUInt8(src, 0), (byte)0x88);
            Assert.AreEqual(LittleEndian.ToUInt16(src, 0), (ushort)0x9988);
            Assert.AreEqual(LittleEndian.ToUInt32(src, 0), (uint)0xbbaa9988);
            Assert.AreEqual(LittleEndian.ToUInt64(src, 0), (ulong)0xffeeddccbbaa9988);
            Assert.AreEqual(LittleEndian.ToInt8(src, 0), unchecked((sbyte)0x88));
            Assert.AreEqual(LittleEndian.ToInt16(src, 0), unchecked((short)0x9988));
            Assert.AreEqual(LittleEndian.ToInt32(src, 0), unchecked((int)0xbbaa9988));
            Assert.AreEqual(LittleEndian.ToInt64(src, 0), unchecked((long)0xffeeddccbbaa9988));

            byte[] tmp;
            tmp = new byte[1];
            LittleEndian.Assign(tmp, 0, (byte)0x88);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(1)));
            tmp = new byte[2];
            LittleEndian.Assign(tmp, 0, (ushort)0x9988);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(2)));
            tmp = new byte[4];
            LittleEndian.Assign(tmp, 0, (uint)0xbbaa9988);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(4)));
            tmp = new byte[8];
            LittleEndian.Assign(tmp, 0, (ulong)0xffeeddccbbaa9988);
            Assert.IsTrue(tmp.SequenceEqual(src.Take(8)));
            tmp = new byte[1];

            LittleEndian.Assign(tmp, 0, unchecked((sbyte)0x88));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(1)));
            tmp = new byte[2];
            LittleEndian.Assign(tmp, 0, unchecked((short)0x9988));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(2)));
            tmp = new byte[4];
            LittleEndian.Assign(tmp, 0, unchecked((int)0xbbaa9988));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(4)));
            tmp = new byte[8];
            LittleEndian.Assign(tmp, 0, unchecked((long)0xffeeddccbbaa9988));
            Assert.IsTrue(tmp.SequenceEqual(src.Take(8)));
        }
    }
}
