﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class TestByte {

#if false
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
#endif

        [TestMethod]
        public unsafe void TestMisc() {

#if false
            Assert.AreEqual(LittleEndian.ToUInt32(dst, 0), 0x8899aabb);
            Assert.AreEqual(LittleEndian.ToUInt16(dst, 4), 0x8899);
            Assert.AreEqual(LittleEndian.ToUInt8(dst, 5), 0x88);
            var restore = new Hoge();
            HostOrder.CopyTo(dst, 0, restore);
#endif

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
            }
            else {
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
#if false
        [TestMethod]
        public unsafe void PerfShort() {
            var buf = new byte[3];
            var fix = stackalloc byte[3];
            var std = Bench.Run(5, (n) => {
                //BitConverter.Assign(fix, unchecked((short)0x8899));
            });
            var alt = Bench.Run(5, (n) => {
                for(var i = 0; i < 2; ++i) {
                    LittleEndian.Assign(fix, unchecked((short)0x8899));
                    LittleEndian.Assign(fix, unchecked((short)0x8899));
                    LittleEndian.Assign(fix, unchecked((short)0x8899));
                    LittleEndian.Assign(fix, unchecked((short)0x8899));
                    LittleEndian.Assign(fix, unchecked((short)0x8899));
                }
            });
            Assert.IsTrue(alt >= std);
        }
#endif
        [TestMethod]
        public void PerfLittleEndianShort() {
            var buffer = new byte[] { 0x88, 0x99 };
            var std = Bench.Run(5, (n) => {
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
                BitConverter.ToInt16(buffer, 0);
            });
            var alt = Bench.Run(5, (n) => {
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
            var std = Bench.Run(5, (n) => {
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
                BitConverter.ToInt32(buffer, 0);
            });
            var alt = Bench.Run(5, (n) => {
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
                var std = Bench.Run(10, (n) => {
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                    t = BitConverter.ToInt64(buffer, 1);
                });
                var alt = Bench.Run(10, (n) => {
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
                var std = Bench.Run(5, (n) => {
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                    t = unchecked((ulong)BitConverter.ToInt64(buffer, 1));
                });
                var alt = Bench.Run(5, (n) => {
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
            var std = Bench.Run(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, 0));
            });
            var alt = Bench.Run(5, (n) => {
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
            var std = Bench.Run(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            });
            var alt = Bench.Run(5, (n) => {
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
            var std = Bench.Run(5, (n) => {
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
                IPAddress.NetworkToHostOrder(BitConverter.ToInt64(buffer, 0));
            });
            var alt = Bench.Run(5, (n) => {
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
                fixed (byte* p = tmp)
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

        public void Clear(byte[] buffer) {
            buffer.Initialize();
        }

        [TestMethod]
        public void TestLitleEndian() {

            var aligned = new byte[] {
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, // long
                0x88, 0x99, 0xaa, 0xbb, // int
                0x88, 0x99, // short
                0x88,       // byte
                0x00,       // bool
                0x40, 0x00, // char
            };
            var offset = new byte[] {
                0x00,
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, // long
                0x88, 0x99, 0xaa, 0xbb, // int
                0x88, 0x99, // short
                0x88,       // byte
                0x00,       // bool
                0x40, 0x00, // char
            };

            // 読み込みの確認
            for (var i = 0; i < 2; ++i) {
                var src = i == 0 ? aligned : offset;
                Assert.AreEqual(LittleEndian.ToUInt64(src, 0 + i), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(LittleEndian.ToUInt32(src, 8 + i), (uint)0xbbaa9988);
                Assert.AreEqual(LittleEndian.ToUInt16(src, 12 + i), (ushort)0x9988);
                Assert.AreEqual(LittleEndian.ToUInt8(src, 14 + i), (byte)0x88);
                Assert.AreEqual(LittleEndian.ToInt64(src, 0 + i), unchecked((long)0xffeeddccbbaa9988));
                Assert.AreEqual(LittleEndian.ToInt32(src, 8 + i), unchecked((int)0xbbaa9988));
                Assert.AreEqual(LittleEndian.ToInt16(src, 12 + i), unchecked((short)0x9988));
                Assert.AreEqual(LittleEndian.ToInt8(src, 14 + i), unchecked((sbyte)0x88));
            }

            // ポインタ系書き込みの確認 あまり厳密ではない
            var buffer = new byte[offset.Length];
            unsafe
            {
                fixed (byte* pointer = buffer)
                for (var i = 0; i < 2; ++i) {
                    var p = pointer + i;
                    buffer.Initialize();
                    LittleEndian.Assign(p + 0, (ulong)0xffeeddccbbaa9988);
                    LittleEndian.Assign(p + 8, (uint)0xbbaa9988);
                    LittleEndian.Assign(p + 12, (ushort)0x9988);
                    LittleEndian.Assign(p + 14, (byte)0x88);
                    LittleEndian.Assign(p + 15, false);
                    LittleEndian.Assign(p + 16, '@');
                    Assert.IsTrue(buffer.Skip(i).Take(17).SequenceEqual(aligned.Take(17)));
                    LittleEndian.Assign(p + 0, unchecked((long)0xffeeddccbbaa9988));
                    buffer.Initialize();
                    LittleEndian.Assign(p + 8, unchecked((int)0xbbaa9988));
                    LittleEndian.Assign(p + 12, unchecked((short)0x9988));
                    LittleEndian.Assign(p + 14, unchecked((sbyte)0x88));
                    LittleEndian.Assign(p + 15, false);
                    LittleEndian.Assign(p + 16, '@');
                    Assert.IsTrue(buffer.Skip(i).Take(17).SequenceEqual(aligned.Take(17)));
                    LittleEndian.Assign(p, 1.1f);
                    LittleEndian.Assign(p, 1.2);
                    LittleEndian.Assign(p, 1.3m);
                }
            }
            for (var i = 0; i < 2; ++i) {
                buffer.Initialize();
                LittleEndian.Assign(buffer, i + 0, (ulong)0xffeeddccbbaa9988);
                LittleEndian.Assign(buffer, i + 8, (uint)0xbbaa9988);
                LittleEndian.Assign(buffer, i + 12, (ushort)0x9988);
                LittleEndian.Assign(buffer, i + 14, (byte)0x88);
                LittleEndian.Assign(buffer, i + 15, false);
                LittleEndian.Assign(buffer, i + 16, '@');
                Assert.IsTrue(buffer.Skip(i).Take(17).SequenceEqual(aligned.Take(17)));
                //
                buffer.Initialize();
                LittleEndian.Assign(buffer, i + 0, unchecked((long)0xffeeddccbbaa9988));
                LittleEndian.Assign(buffer, i + 8, unchecked((int)0xbbaa9988));
                LittleEndian.Assign(buffer, i + 12, unchecked((short)0x9988));
                LittleEndian.Assign(buffer, i + 14, unchecked((sbyte)0x88));
                LittleEndian.Assign(buffer, i + 15, false);
                LittleEndian.Assign(buffer, i + 16, '@');
                Assert.IsTrue(buffer.Skip(i).Take(17).SequenceEqual(aligned.Take(17)));
                //
                LittleEndian.Assign(buffer, i + 0, 1.1f);
                LittleEndian.Assign(buffer, i + 0, 1.2);
                LittleEndian.Assign(buffer, i + 0, 1.3m);
            }

#if false
            {
                LittleEndian.Assign(tmp, 0, '@');
                    Assert.IsTrue(tmp.Skip(0).Take(2).SequenceEqual(new byte[] { 0x40, 0x00 }));
                    Assert.AreEqual(LittleEndian.ToChar(tmp, 0), '@');

                    LittleEndian.Assign(tmp, 0, 1.1);
                    Assert.AreEqual(LittleEndian.ToDouble(tmp, 0), 1.1);

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
#endif
        }
        }
}
