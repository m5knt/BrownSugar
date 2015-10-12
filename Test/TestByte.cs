using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class TestByte {

        public long Bench(int limit, Action<long> action) {
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
        public void TestNetOrder() {
            var us = (ushort)0x8899;
            var ui = (uint)0x8899aabb;
            var ul = (ulong)0x8899aabbccddeeff;
            var ss = unchecked((short)0x8899);
            var si = unchecked((int)0x8899aabb);
            var sl = unchecked((long)0x8899aabbccddeeff);

            if (BitConverter.IsLittleEndian) {
                Assert.AreEqual(us.ToNetOrder(), (ushort)0x9988);
                Assert.AreEqual(ui.ToNetOrder(), (uint)0xbbaa9988);
                Assert.AreEqual(ul.ToNetOrder(), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(ss.ToNetOrder(), unchecked((short)0x9988));
                Assert.AreEqual(si.ToNetOrder(), unchecked((int)0xbbaa9988));
                Assert.AreEqual(sl.ToNetOrder(), unchecked((long)0xffeeddccbbaa9988));
                //
                Assert.AreEqual(us.ToHostOrder(), (ushort)0x9988);
                Assert.AreEqual(ui.ToHostOrder(), (uint)0xbbaa9988);
                Assert.AreEqual(ul.ToHostOrder(), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(ss.ToHostOrder(), unchecked((short)0x9988));
                Assert.AreEqual(si.ToHostOrder(), unchecked((int)0xbbaa9988));
                Assert.AreEqual(sl.ToHostOrder(), unchecked((long)0xffeeddccbbaa9988));
            } else {
                Assert.AreEqual(us.ToNetOrder(), us);
                Assert.AreEqual(ui.ToNetOrder(), ui);
                Assert.AreEqual(ul.ToNetOrder(), ul);
                Assert.AreEqual(ss.ToNetOrder(), ss);
                Assert.AreEqual(si.ToNetOrder(), si);
                Assert.AreEqual(sl.ToNetOrder(), sl);
                //
                Assert.AreEqual(us.ToHostOrder(), us);
                Assert.AreEqual(ui.ToHostOrder(), ui);
                Assert.AreEqual(ul.ToHostOrder(), ul);
                Assert.AreEqual(ss.ToHostOrder(), ss);
                Assert.AreEqual(si.ToHostOrder(), si);
                Assert.AreEqual(sl.ToHostOrder(), sl);
            }
        }

        [TestMethod]
        public void PerfLittleEndianShort() {
            var buffer = new byte[] { 0x88, 0x99 };
            var std = Bench(5, (n) => {
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
            });
            var alt = Bench(5, (n) => {
                LittleEndian.ToInt16(buffer, 0);
                LittleEndian.ToInt16(buffer, 0);
                LittleEndian.ToInt16(buffer, 0);
                LittleEndian.ToInt16(buffer, 0);
                LittleEndian.ToInt16(buffer, 0);
            });
            Assert.IsTrue(alt >= std);
        }

        [TestMethod]
        public void PerfLittleEndianInt() {
            var buffer = new byte[] { 0x88, 0x99, 0xaa, 0xbb };
            var std = Bench(5, (n) => {
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
            });
            var alt = Bench(5, (n) => {
                LittleEndian.ToInt32(buffer, 0);
                LittleEndian.ToInt32(buffer, 0);
                LittleEndian.ToInt32(buffer, 0);
                LittleEndian.ToInt32(buffer, 0);
                LittleEndian.ToInt32(buffer, 0);
            });
            Assert.IsTrue(alt >= std);
        }

        [TestMethod]
        public void PerfLittleEndianLong() {
            var buffer = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x11 };
            if (Always.False) {
                long t = 0;
                var std = Bench(10, (n) => {
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                });
                var alt = Bench(10, (n) => {
                    t = LittleEndian.ToInt64(buffer, 1);
                    t = LittleEndian.ToInt64(buffer, 1);
                    t = LittleEndian.ToInt64(buffer, 1);
                    t = LittleEndian.ToInt64(buffer, 1);
                    t = LittleEndian.ToInt64(buffer, 1);
                });
                Assert.IsTrue(alt >= std); 
            }
            {
                ulong t = 0;
                var std = Bench(5, (n) => {
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                });
                var alt = Bench(5, (n) => {
                    t = LittleEndian.ToUInt64(buffer, 1);
                    t = LittleEndian.ToUInt64(buffer, 1);
                    t = LittleEndian.ToUInt64(buffer, 1);
                    t = LittleEndian.ToUInt64(buffer, 1);
                    t = LittleEndian.ToUInt64(buffer, 1);
                });
                Assert.IsTrue(alt >= std);
            }
        }

        [TestMethod]
        public void PerfBigEndianShort() {
            var buffer = new byte[] { 0x88, 0x99 };
            var std = Bench(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
            });
            var alt = Bench(5, (n) => {
                BigEndian.ToInt16(buffer, 0);
                BigEndian.ToInt16(buffer, 0);
                BigEndian.ToInt16(buffer, 0);
                BigEndian.ToInt16(buffer, 0);
                BigEndian.ToInt16(buffer, 0);
            });
            Assert.IsTrue(alt >= std);
        }

        [TestMethod]
        public void PerfBigEndianInt() {
            var buffer = new byte[] { 0x88, 0x99, 0xaa, 0xbb };
            var std = Bench(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            });
            var alt = Bench(5, (n) => {
                BigEndian.ToInt32(buffer, 0);
                BigEndian.ToInt32(buffer, 0);
                BigEndian.ToInt32(buffer, 0);
                BigEndian.ToInt32(buffer, 0);
                BigEndian.ToInt32(buffer, 0);
            });
            Assert.IsTrue(alt >= std);
        }

        [TestMethod]
        public void PerfBigEndianLong() {
            var buffer = new byte[] { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff };
            var std = Bench(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
            });
            var alt = Bench(5, (n) => {
                BigEndian.ToInt64(buffer, 0);
                BigEndian.ToInt64(buffer, 0);
                BigEndian.ToInt64(buffer, 0);
                BigEndian.ToInt64(buffer, 0);
                BigEndian.ToInt64(buffer, 0);
            });
            Assert.IsTrue(alt >= std);
        }

        //
        //
        //

        [TestMethod]
        public void TestBigEndian() {
            {
                var src = new byte[] {
                    0x80, 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
                };

                // 読み取り確認
                Assert.AreEqual(BigEndian.ToUInt8(src, 1), (byte)0x88);
                Assert.AreEqual(BigEndian.ToUInt16(src, 1), (ushort)0x8899);
                Assert.AreEqual(BigEndian.ToUInt32(src, 1), (uint)0x8899aabb);
                Assert.AreEqual(BigEndian.ToUInt64(src, 1), (ulong)0x8899aabbccddeeff);
                Assert.AreEqual(BigEndian.ToInt8(src, 1), unchecked((sbyte)0x88));
                Assert.AreEqual(BigEndian.ToInt16(src, 1), unchecked((short)0x8899));
                Assert.AreEqual(BigEndian.ToInt32(src, 1), unchecked((int)0x8899aabb));
                Assert.AreEqual(BigEndian.ToInt64(src, 1), unchecked((long)0x8899aabbccddeeff));

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
                //
                BigEndian.Assign(tmp, 0, '@');
                Assert.IsTrue(tmp.Take(2).SequenceEqual(new byte[] { 0x00, 0x40 }));
                Assert.AreEqual(BigEndian.ToChar(tmp, 0), '@');
                BigEndian.Assign(tmp, 0, 1.1);
                Assert.AreEqual(BigEndian.ToDouble(tmp, 0), 1.1);
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
                    0x00, // false
                    0x01, // true
                    0x00, 0x40, // '@'
                };
                var tmp = new byte[src.Length];
                fixed(byte* p = tmp)
                {
                    BigEndian.Assign(p + 0, (byte)0x88);
                    BigEndian.Assign(p + 1, (ushort)0x9988);
                    BigEndian.Assign(p + 3, (uint)0xbbaa9988);
                    BigEndian.Assign(p + 7, (ulong)0xffeeddccbbaa9988);
                    BigEndian.Assign(p + 15, unchecked((sbyte)0x88));
                    BigEndian.Assign(p + 16, unchecked((short)0x9988));
                    BigEndian.Assign(p + 18, unchecked((int)0xbbaa9988));
                    BigEndian.Assign(p + 22, unchecked((long)0xffeeddccbbaa9988));
                    BigEndian.Assign(p + 30, false);
                    BigEndian.Assign(p + 31, true);
                    BigEndian.Assign(p + 32, '@');
                }
                Assert.IsTrue(tmp.SequenceEqual(src));
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

                LittleEndian.Assign(tmp, 0, '@');
                Assert.IsTrue(tmp.Skip(0).Take(2).SequenceEqual(new byte[] { 0x40, 0x00 }));
                Assert.AreEqual(LittleEndian.ToChar(tmp, 0), '@');

                LittleEndian.Assign(tmp, 0, 1.1);
                Assert.AreEqual(LittleEndian.ToDouble(tmp, 0), 1.1);
            }

            unsafe
            {
                var src = new byte[] {
                    0x88,
                    0x88, 0x99,
                    0x88, 0x99, 0xaa, 0xbb,
                    0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
                    0x88,
                    0x88, 0x99,
                    0x88, 0x99, 0xaa, 0xbb,
                    0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff,
                    0x00, // false
                    0x01, // true
                    0x40, 0x00, // '@'
                };
                var tmp = new byte[src.Length];
                fixed(byte* p = tmp)
                {
                    LittleEndian.Assign(p + 0, (byte)0x88);
                    LittleEndian.Assign(p + 1, (ushort)0x9988);
                    LittleEndian.Assign(p + 3, (uint)0xbbaa9988);
                    LittleEndian.Assign(p + 7, (ulong)0xffeeddccbbaa9988);
                    LittleEndian.Assign(p + 15, unchecked((sbyte)0x88));
                    LittleEndian.Assign(p + 16, unchecked((short)0x9988));
                    LittleEndian.Assign(p + 18, unchecked((int)0xbbaa9988));
                    LittleEndian.Assign(p + 22, unchecked((long)0xffeeddccbbaa9988));
                    LittleEndian.Assign(p + 30, false);
                    LittleEndian.Assign(p + 31, true);
                    LittleEndian.Assign(p + 32, '@');
                }
                Assert.IsTrue(tmp.SequenceEqual(src)); 
            }
        }
    }
}
