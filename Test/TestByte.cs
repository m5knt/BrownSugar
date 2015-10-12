using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class TestByte {

        public long Bench(int limit, Func<long, long> action) {
            var now = DateTime.Now;
            var count = 0L;
            while ((DateTime.Now - now).TotalSeconds < limit) {
                action(count);
                ++count;
            }
            Console.WriteLine(action.Method.DeclaringType.ToString() + ":" + count.ToString());
            return count;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class Hoge {
            public int i;
            public short s;
            public sbyte b;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "01234567890";
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public double[] array = new double[10];
        }

        [TestMethod]
        public unsafe void TestMisc() {
            var src = new Hoge {
                i = unchecked((int)0x8899aabb),
                s = unchecked((short)0x8899),
                b = unchecked((sbyte)0x88),
            };
            var dst = new byte[src.ByteSize()];
            HostOrder.Assign(dst, 0, src);
            Assert.AreEqual(LittleEndian.ToUInt32(dst, 0), 0x8899aabb);
            Assert.AreEqual(LittleEndian.ToUInt16(dst, 4), 0x8899);
            Assert.AreEqual(LittleEndian.ToUInt8(dst, 5), 0x88);
            var restore = new Hoge();
            HostOrder.CopyTo(dst, 0, restore);

            //            var n  = new Hoge().SizeOf();
            //            Console.WriteLine(n);
            //            System.Diagnostics.Debug.Listeners.Add(
            //                new System.Diagnostics.TextWriterTraceListener(Console.Out));
            //            System.Diagnostics.Debug.WriteLine("size:" + n.ToString());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestCast() {
        }

        [TestMethod]
        public void TestSwapByteOrder() {
            Assert.AreEqual(((ushort)0x8899).SwapByteOrder(), (ushort)0x9988);
            Assert.AreEqual(((uint)0x8899aabb).SwapByteOrder(), (uint)0xbbaa9988);
            Assert.AreEqual(((ulong)0x8899aabbccddeeff).SwapByteOrder(), (ulong)0xffeeddccbbaa9988);
            Assert.AreEqual(unchecked((short)0x8899).SwapByteOrder(), unchecked((short)0x9988));
            Assert.AreEqual(unchecked((int)0x8899aabb).SwapByteOrder(), unchecked((int)0xbbaa9988));
            Assert.AreEqual(unchecked((long)0x8899aabbccddeeff).SwapByteOrder(), unchecked((long)0xffeeddccbbaa9988));
        }

        [TestMethod]
        public void TestNetOrder() {
            if (BitConverter.IsLittleEndian) {
                Assert.AreEqual(((ushort)0x8899).ToNetOrder(), (ushort)0x9988);
                Assert.AreEqual(((uint)0x8899aabb).ToNetOrder(), (uint)0xbbaa9988);
                Assert.AreEqual(((ulong)0x8899aabbccddeeff).ToNetOrder(), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(unchecked((short)0x8899).ToNetOrder(), unchecked((short)0x9988));
                Assert.AreEqual(unchecked((int)0x8899aabb).ToNetOrder(), unchecked((int)0xbbaa9988));
                Assert.AreEqual(unchecked((long)0x8899aabbccddeeff).ToNetOrder(), unchecked((long)0xffeeddccbbaa9988));
                //
                Assert.AreEqual(((ushort)0x8899).ToHostOrder(), (ushort)0x9988);
                Assert.AreEqual(((uint)0x8899aabb).ToHostOrder(), (uint)0xbbaa9988);
                Assert.AreEqual(((ulong)0x8899aabbccddeeff).ToHostOrder(), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(unchecked((short)0x8899).ToHostOrder(), unchecked((short)0x9988));
                Assert.AreEqual(unchecked((int)0x8899aabb).ToHostOrder(), unchecked((int)0xbbaa9988));
                Assert.AreEqual(unchecked((long)0x8899aabbccddeeff).ToHostOrder(), unchecked((long)0xffeeddccbbaa9988));
            }
        }

        [TestMethod]
        public void PerfSwapByteOrder() {
            {
                var std = Bench(10, (n) => {
                    return IPAddress.HostToNetworkOrder(unchecked((short)n));
                });
                var alt = Bench(10, (n) => {
                    return unchecked((short)n).SwapByteOrder();
                });
                Assert.IsTrue(alt >= std);
            }
            {
                var std = Bench(10, (n) => {
                    return IPAddress.HostToNetworkOrder(unchecked((int)n));
                });
                var alt = Bench(10, (n) => {
                    return unchecked((int)n).SwapByteOrder();
                });
                Assert.IsTrue(alt >= std);
            }
            {
                var std = Bench(10, (n) => {
                    return IPAddress.HostToNetworkOrder(unchecked((long)n));
                });
                var alt = Bench(10, (n) => {
                    return unchecked((long)n).SwapByteOrder();
                });
                Assert.IsTrue(alt >= std);
            }
        }

        //
        //
        //

        [TestMethod]
        public void TestBigEndian() {
            {
                var src = new byte[] { 0x80, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
                Assert.AreEqual(BigEndian.ToUInt8(src, 1), (byte)0x88);
                Assert.AreEqual(BigEndian.ToUInt16(src, 1), (ushort)0x8899);
                Assert.AreEqual(BigEndian.ToUInt32(src, 1), (uint)0x8899aabb);
                Assert.AreEqual(BigEndian.ToUInt64(src, 1), (ulong)0x8899aabbccddeeff);
                Assert.AreEqual(BigEndian.ToInt8(src, 1), unchecked((sbyte)0x88));
                Assert.AreEqual(BigEndian.ToInt16(src, 1), unchecked((short)0x8899));
                Assert.AreEqual(BigEndian.ToInt32(src, 1), unchecked((int)0x8899aabb));
                Assert.AreEqual(BigEndian.ToInt64(src, 1), unchecked((long)0x8899aabbccddeeff));
                BigEndian.ToDouble(src, 1);

                byte[] tmp;
                tmp = new byte[1 + 1];
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

            unsafe
            {
                var src = new byte[] {
                    0x88,
                    0x99, 0x88,
                    0xbb, 0xaa, 0x99, 0x88,
                    0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x99, 0x88,
                    0x88,
                    0x99, 0x88,
                    0xbb, 0xaa, 0x99, 0x88,
                    0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x99, 0x88,
                };
                var tmp = new byte[30];
                BigEndian.Assign(tmp, 1, (p) => {
                    BigEndian.Assign(p + 0, (byte)0x88);
                    BigEndian.Assign(p + 1, (ushort)0x9988);
                    BigEndian.Assign(p + 3, (uint)0xbbaa9988);
                    BigEndian.Assign(p + 7, (ulong)0xffeeddccbbaa9988);
                    BigEndian.Assign(p + 15, unchecked((sbyte)0x88));
                    BigEndian.Assign(p + 16, unchecked((short)0x9988));
                    BigEndian.Assign(p + 18, unchecked((int)0xbbaa9988));
                    BigEndian.Assign(p + 22, unchecked((long)0xffeeddccbbaa9988));
                });
            }
        }

        [TestMethod]
        public void TestLitleEndian() {
            {
                var src = new byte[] { 0x80, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
                Assert.AreEqual(LittleEndian.ToUInt8(src, 1), (byte)0x88);
                Assert.AreEqual(LittleEndian.ToUInt16(src, 1), (ushort)0x9988);
                Assert.AreEqual(LittleEndian.ToUInt32(src, 1), (uint)0xbbaa9988);
                Assert.AreEqual(LittleEndian.ToUInt64(src, 1), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(LittleEndian.ToInt8(src, 1), unchecked((sbyte)0x88));
                Assert.AreEqual(LittleEndian.ToInt16(src, 1), unchecked((short)0x9988));
                Assert.AreEqual(LittleEndian.ToInt32(src, 1), unchecked((int)0xbbaa9988));
                Assert.AreEqual(LittleEndian.ToInt64(src, 1), unchecked((long)0xffeeddccbbaa9988));
                LittleEndian.ToDouble(src, 1);
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
                tmp = new byte[8 + 1];
                LittleEndian.Assign(tmp, 1, unchecked((long)0xffeeddccbbaa9988));
                Assert.IsTrue(tmp.Skip(1).Take(8).SequenceEqual(src.Skip(1).Take(8)));
            }

            unsafe
            {
                var src = new byte[] {
                    0x88,
                    0x88, 0x99,
                    0xbb, 0xaa, 0x88, 0x99,
                    0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x88, 0x99,
                    0x88,
                    0x88, 0x99,
                    0xbb, 0xaa, 0x88, 0x99,
                    0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x88, 0x99,
                };
                var tmp = new byte[30];
                LittleEndian.Assign(tmp, 1, (p) => {
                    LittleEndian.Assign(p + 0, (byte)0x88);
                    LittleEndian.Assign(p + 1, (ushort)0x9988);
                    LittleEndian.Assign(p + 3, (uint)0xbbaa9988);
                    LittleEndian.Assign(p + 7, (ulong)0xffeeddccbbaa9988);
                    LittleEndian.Assign(p + 15, unchecked((sbyte)0x88));
                    LittleEndian.Assign(p + 16, unchecked((short)0x9988));
                    LittleEndian.Assign(p + 18, unchecked((int)0xbbaa9988));
                    LittleEndian.Assign(p + 22, unchecked((long)0xffeeddccbbaa9988));
                });
            }

        }
    }
}
