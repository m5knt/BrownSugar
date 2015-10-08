using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThunderEgg.BrownSugar.Byte;
using System;
using System.Net;

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
        public void PerfSwapByteOrderPerf() {
            if (!BitConverter.IsLittleEndian) {
                return;
            }
            var api_start = DateTime.Now;
            var api = 0L;
            while ((DateTime.Now - api_start).TotalSeconds < 10) {
                IPAddress.HostToNetworkOrder(unchecked((long)0x8899aabbccddeeff));
                ++api;
            }
            //
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((long)0x8899aabbccddeeff).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((ulong)0x8899aabbccddeeff).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }

        }

        [TestMethod]
        public void PerfSwapByteOrderInt() {
            if (!BitConverter.IsLittleEndian) {
                return;
            }
            var api_start = DateTime.Now;
            var api = 0L;
            while ((DateTime.Now - api_start).TotalSeconds < 10) {
                IPAddress.HostToNetworkOrder(unchecked((int)0x8899aabb));
                ++api;
            }
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((int)0x8899aabb).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((uint)0x8899aabb).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }
        }

        [TestMethod]
        public void PerfSwapByteOrderShort() {
            if (!BitConverter.IsLittleEndian) {
                return;
            }
            var api_start = DateTime.Now;
            var api = 0L;
            while ((DateTime.Now - api_start).TotalSeconds < 10) {
                IPAddress.HostToNetworkOrder(unchecked((short)0x8899));
                ++api;
            }
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((short)0x8899).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }
            {
                var my_start = DateTime.Now;
                var my = 0L;
                while ((DateTime.Now - my_start).TotalSeconds < 10) {
                    unchecked((ushort)0x8899).SwapByteOrder();
                    ++my;
                }
                Assert.IsTrue(my >= api);
            }
        }

        //
        //
        //

        [TestMethod]
        public void TestBigEndian() {
            var src = new byte[] { 0x80, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff};
            Assert.AreEqual(BigEndian.ToUInt8(src, 1), (byte)0x88);
            Assert.AreEqual(BigEndian.ToUInt16(src, 1), (ushort)0x8899);
            Assert.AreEqual(BigEndian.ToUInt32(src, 1), (uint)0x8899aabb);
            Assert.AreEqual(BigEndian.ToUInt64(src, 1), (ulong)0x8899aabbccddeeff);
            Assert.AreEqual(BigEndian.ToInt8(src, 1), unchecked((sbyte)0x88));
            Assert.AreEqual(BigEndian.ToInt16(src, 1), unchecked((short)0x8899));
            Assert.AreEqual(BigEndian.ToInt32(src, 1), unchecked((int)0x8899aabb));
            Assert.AreEqual(BigEndian.ToInt64(src, 1), unchecked((long)0x8899aabbccddeeff));

            byte[] tmp;
            tmp = new byte[1+1];
            BigEndian.Assign(tmp, 1, (byte)0x88);
            Assert.IsTrue(tmp.Skip(1).Take(1).SequenceEqual(src.Skip(1).Take(1)));
            tmp = new byte[2 + 1];
            BigEndian.Assign(tmp, 1, (ushort)0x8899);
            Assert.IsTrue(tmp.Skip(1).Take(2).SequenceEqual(src.Skip(1).Take(2)));
            tmp = new byte[4 + 1];
            BigEndian.Assign(tmp, 1, (uint)0x8899aabb);
            Assert.IsTrue(tmp.Skip(1).Take(4).SequenceEqual(src.Skip(1).Take(4)));
            tmp = new byte[8 + 1];
            BigEndian.Assign(tmp, 1, (ulong)0x8899aabbccddeeff);
            Assert.IsTrue(tmp.Skip(1).Take(8).SequenceEqual(src.Skip(1).Take(8)));

            tmp = new byte[1 + 1];
            BigEndian.Assign(tmp, 1, unchecked((sbyte)0x88));
            Assert.IsTrue(tmp.Skip(1).Take(1).SequenceEqual(src.Skip(1).Take(1)));
            tmp = new byte[2 + 1];
            BigEndian.Assign(tmp, 1, unchecked((short)0x8899));
            Assert.IsTrue(tmp.Skip(1).Take(2).SequenceEqual(src.Skip(1).Take(2)));
            tmp = new byte[4 + 1];
            BigEndian.Assign(tmp, 1, unchecked((int)0x8899aabb));
            Assert.IsTrue(tmp.Skip(1).Take(4).SequenceEqual(src.Skip(1).Take(4)));
            tmp = new byte[8 + 1];
            BigEndian.Assign(tmp, 1, unchecked((long)0x8899aabbccddeeff));
            Assert.IsTrue(tmp.Skip(1).Take(8).SequenceEqual(src.Skip(1).Take(8)));
        }

        [TestMethod]
        public void TestLitleEndian() {
            var src = new byte[] { 0x80, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            Assert.AreEqual(LittleEndian.ToUInt8(src, 1), (byte)0x88);
            Assert.AreEqual(LittleEndian.ToUInt16(src, 1), (ushort)0x9988);
            Assert.AreEqual(LittleEndian.ToUInt32(src, 1), (uint)0xbbaa9988);
            Assert.AreEqual(LittleEndian.ToUInt64(src, 1), (ulong)0xffeeddccbbaa9988);
            Assert.AreEqual(LittleEndian.ToInt8(src, 1), unchecked((sbyte)0x88));
            Assert.AreEqual(LittleEndian.ToInt16(src, 1), unchecked((short)0x9988));
            Assert.AreEqual(LittleEndian.ToInt32(src, 1), unchecked((int)0xbbaa9988));
            Assert.AreEqual(LittleEndian.ToInt64(src, 1), unchecked((long)0xffeeddccbbaa9988));

            byte[] tmp;
            tmp = new byte[1 + 1];
            LittleEndian.Assign(tmp, 1, (byte)0x88);
            Assert.IsTrue(tmp.Skip(1).Take(1).SequenceEqual(src.Skip(1).Take(1)));
            tmp = new byte[2 + 1];
            LittleEndian.Assign(tmp, 1, (ushort)0x9988);
            Assert.IsTrue(tmp.Skip(1).Take(2).SequenceEqual(src.Skip(1).Take(2)));
            tmp = new byte[4 + 1];
            LittleEndian.Assign(tmp, 1, (uint)0xbbaa9988);
            Assert.IsTrue(tmp.Skip(1).Take(4).SequenceEqual(src.Skip(1).Take(4)));
            tmp = new byte[8 + 1];
            LittleEndian.Assign(tmp, 1, (ulong)0xffeeddccbbaa9988);
            Assert.IsTrue(tmp.Skip(1).Take(8).SequenceEqual(src.Skip(1).Take(8)));

            tmp = new byte[1 + 1];
            LittleEndian.Assign(tmp, 1, unchecked((sbyte)0x88));
            Assert.IsTrue(tmp.Skip(1).Take(1).SequenceEqual(src.Skip(1).Take(1)));
            tmp = new byte[2 + 1];
            LittleEndian.Assign(tmp, 1, unchecked((short)0x9988));
            Assert.IsTrue(tmp.Skip(1).Take(2).SequenceEqual(src.Skip(1).Take(2)));
            tmp = new byte[4 + 1];
            LittleEndian.Assign(tmp, 1, unchecked((int)0xbbaa9988));
            Assert.IsTrue(tmp.Skip(1).Take(4).SequenceEqual(src.Skip(1).Take(4)));
            tmp = new byte[8+1];
            LittleEndian.Assign(tmp, 1, unchecked((long)0xffeeddccbbaa9988));
            Assert.IsTrue(tmp.Skip(1).Take(8).SequenceEqual(src.Skip(1).Take(8)));
        }
    }
}
