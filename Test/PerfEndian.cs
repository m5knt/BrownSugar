using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class PerfEndianAll {

        public PerfEndian PerfEndian = new PerfEndian();

        [TestMethod]
        public void NoEndianByte() {
            PerfEndian.NoByteOrderToUInt8();
            PerfEndian.NoByteOrderToInt8();
            PerfEndian.NoByteOrderToBoolean();
        }

        [TestMethod]
        public void LittleEndian2Bytes() {
            PerfEndian.LittleEndianToUInt16();
            PerfEndian.LittleEndianToInt16();
            PerfEndian.LittleEndianToChar();
        }

        [TestMethod]
        public void BigEndian2Bytes() {
            PerfEndian.BigEndianToUInt16();
            PerfEndian.BigEndianToInt16();
            PerfEndian.BigEndianToChar();
        }

        [TestMethod]
        public void LittleEndian4Bytes() {
            PerfEndian.LittleEndianToUInt32();
            PerfEndian.LittleEndianToInt32();
            PerfEndian.LittleEndianToSingle();
        }

        [TestMethod]
        public void BigEndian4Bytes() {
            PerfEndian.BigEndianToUInt32();
            PerfEndian.BigEndianToInt32();
            PerfEndian.BigEndianToSingle();
        }

        [TestMethod]
        public void LittleEndian8Bytes() {
            PerfEndian.LittleEndianToUInt64();
            PerfEndian.LittleEndianToInt64();
            PerfEndian.LittleEndianToDouble();
        }

        [TestMethod]
        public void BigEndian8Bytes() {
            PerfEndian.BigEndianToUInt64();
            PerfEndian.BigEndianToInt64();
            PerfEndian.BigEndianToDouble();
        }

        [TestMethod]
        public void LittleEndian() {
            Console.WriteLine("1");
            PerfEndian.LittleEndianToBoolean();
            Console.WriteLine("2");
            PerfEndian.LittleEndianToUInt16();
            PerfEndian.LittleEndianToInt16();
            PerfEndian.LittleEndianToChar();
            /**/
            Console.WriteLine("4");
            PerfEndian.LittleEndianToUInt32();
            PerfEndian.LittleEndianToInt32();
            PerfEndian.LittleEndianToSingle();
            /**/
            Console.WriteLine("8");
            PerfEndian.LittleEndianToUInt64();
            PerfEndian.LittleEndianToInt64();
            PerfEndian.LittleEndianToDouble();
        }

        [TestMethod]
        public void BigEndian() {
            Console.WriteLine("1");
            PerfEndian.BigEndianToBoolean();
            Console.WriteLine("2");
            PerfEndian.BigEndianToUInt16();
            PerfEndian.BigEndianToInt16();
            PerfEndian.BigEndianToChar();
            /**/
            Console.WriteLine("4");
            PerfEndian.BigEndianToUInt32();
            PerfEndian.BigEndianToInt32();
            PerfEndian.BigEndianToSingle();
            /**/
            Console.WriteLine("8");
            PerfEndian.BigEndianToUInt64();
            PerfEndian.BigEndianToInt64();
            PerfEndian.BigEndianToDouble();
        }
    }


    [TestClass]
    public class PerfEndian {

        const double Limit = 2;
        const int Boost = 100;

        [TestMethod]
        public unsafe void PerfInt32() {
            // おそらく差が出ない
            var limit = 60;
            var result = 0L;
            var b = stackalloc byte[16];
            result = Bench.Run(limit, (n) => {
                for (var j = 0; j < Boost; ++j) {
                    var p = (((b[3] << 8 | b[2]) << 8 | b[1]) << 8 | b[0]);
                }
            });
            Console.WriteLine("type 1 : " + result);
            /**/
            result = Bench.Run(limit, (n) => {
                for (var j = 0; j < Boost; ++j) {
                    var p = (b[3] << 8 | b[2]) << 16 | b[1] << 8 | b[0];
                }
            });
            Console.WriteLine("type 2 : " + result);
            /**/
            result = Bench.Run(limit, (n) => {
                for (var j = 0; j < Boost; ++j) {
                    var p = b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0];
                }
            });
            Console.WriteLine("type 3 : " + result);
        }

        [TestMethod]
        public unsafe void NoByteOrderToUInt8() {
            var ret = Duel(Limit, "ToUInt8",
                (b, i) => { BitConverter.ToBoolean(b, i); },
                (b, i) => { LittleEndian.ToUInt8(b, i); },
                (b) => { LittleEndian.ToUInt8(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void NoByteOrderToInt8() {
            var ret = Duel(Limit, "ToInt8",
                (b, i) => { BitConverter.ToBoolean(b, i); },
                (b, i) => { LittleEndian.ToInt8(b, i); },
                (b) => { LittleEndian.ToInt8(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void NoByteOrderToBoolean() {
            var ret = Duel(Limit, "ToInt8",
                (b, i) => { BitConverter.ToBoolean(b, i); },
                (b, i) => { LittleEndian.ToBoolean(b, i); },
                (b) => { LittleEndian.ToBoolean(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToUInt16() {
            var ret = Duel(Limit, "PerfLittleEndianToUInt16",
                (b, i) => { BitConverter.ToUInt16(b, i); },
                (b, i) => { LittleEndian.ToUInt16(b, i); },
                (b) => { LittleEndian.ToUInt16(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToInt16() {
            var ret = Duel(Limit, "PerfLittleEndianToInt16",
                (b, i) => { BitConverter.ToInt16(b, i); },
                (b, i) => { LittleEndian.ToInt16(b, i); },
                (b) => { LittleEndian.ToInt16(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToUInt32() {
            var ret = Duel(Limit, "PerfLittleEndianToUInt32",
                (b, i) => { BitConverter.ToUInt32(b, i); },
                (b, i) => { LittleEndian.ToUInt32(b, i); },
                (b) => { LittleEndian.ToUInt32(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToInt32() {
            var ret = Duel(Limit, "PerfLittleEndianToInt32",
                (b, i) => { BitConverter.ToInt32(b, i); },
                (b, i) => { LittleEndian.ToInt32(b, i); },
                (b) => { LittleEndian.ToInt32(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToUInt64() {
            var ret = Duel(Limit, "PerfLittleEndianToUInt64",
                (b, i) => { BitConverter.ToUInt64(b, i); },
                (b, i) => { LittleEndian.ToUInt64(b, i); },
                (b) => { LittleEndian.ToUInt64(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToInt64() {
            var ret = Duel(Limit, "PerfLittleEndianToInt64",
                (b, i) => { BitConverter.ToInt64(b, i); },
                (b, i) => { LittleEndian.ToInt64(b, i); },
                (b) => { LittleEndian.ToInt64(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToBoolean() {
            var ret = Duel(Limit, "PerfLittleEndianToBoolean",
                (b, i) => { BitConverter.ToBoolean(b, i); },
                (b, i) => { LittleEndian.ToBoolean(b, i); },
                (b) => { LittleEndian.ToBoolean(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToChar() {
            var ret = Duel(Limit, "PerfLittleEndianToChar",
                (b, i) => { BitConverter.ToChar(b, i); },
                (b, i) => { LittleEndian.ToChar(b, i); },
                (b) => { LittleEndian.ToChar(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToSingle() {
            var ret = Duel(Limit, "PerfLittleEndianToSingle",
                (b, i) => { BitConverter.ToSingle(b, i); },
                (b, i) => { LittleEndian.ToSingle(b, i); },
                (b) => { LittleEndian.ToSingle(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void LittleEndianToDouble() {
            var ret = Duel(Limit, "PerfLittleEndianToDouble",
                (b, i) => { BitConverter.ToDouble(b, i); },
                (b, i) => { LittleEndian.ToDouble(b, i); },
                (b) => { LittleEndian.ToDouble(b); });
            Assert.IsTrue(ret);
        }

        byte[] ReadData = new byte[] {
                0x88, 0x99, 0xaa, 0xbb,
                0xcc, 0xdd, 0xee, 0xff, 0x11
            };

        //
        //
        //

        [TestMethod]
        public unsafe void BigEndianToUInt16() {
            var ret = Duel(Limit, "PerfBigEndianToUInt16",
                (b, i) => { var n = unchecked((ushort)IPAddress.NetworkToHostOrder(unchecked((short)BitConverter.ToUInt16(b, i)))); },
                (b, i) => { var n = BigEndian.ToUInt16(b, i); },
                (b) => { BigEndian.ToUInt16(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToInt16() {
            var ret = Duel(Limit, "PerfBigEndianToInt16",
                (b, i) => { IPAddress.NetworkToHostOrder(BitConverter.ToInt16(b, i)); },
                (b, i) => { BigEndian.ToInt16(b, i); },
                (b) => { BigEndian.ToInt16(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToUInt32() {
            var ret = Duel(Limit, "PerfBigEndianToUInt32",
                (b, i) => { var n = unchecked((uint)IPAddress.NetworkToHostOrder(unchecked((int)BitConverter.ToUInt32(b, i)))); },
                (b, i) => { var n = BigEndian.ToUInt32(b, i); },
                (b) => { BigEndian.ToUInt32(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToInt32() {
            var ret = Duel(Limit, "PerfBigEndianToInt32",
                (b, i) => { IPAddress.NetworkToHostOrder(BitConverter.ToInt32(b, i)); },
                (b, i) => { BigEndian.ToInt32(b, i); },
                (b) => { BigEndian.ToInt32(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToUInt64() {
            var ret = Duel(Limit, "PerfBigEndianToUInt64",
                (b, i) => { var n = unchecked((ulong)IPAddress.NetworkToHostOrder(unchecked((long)BitConverter.ToUInt64(b, i)))); },
                (b, i) => { var n = BigEndian.ToUInt64(b, i); },
                (b) => { BigEndian.ToUInt64(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToInt64() {
            var ret = Duel(Limit, "PerfBigEndianToInt64",
                (b, i) => { IPAddress.NetworkToHostOrder(BitConverter.ToInt64(b, i)); },
                (b, i) => { BigEndian.ToInt64(b, i); },
                (b) => { BigEndian.ToInt64(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToBoolean() {
            var ret = Duel(Limit, "PerfBigEndianToBoolean",
                (b, i) => { BitConverter.ToBoolean(b, i); },
                (b, i) => { BigEndian.ToBoolean(b, i); },
                (b) => { BigEndian.ToBoolean(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToChar() {
            var ret = Duel(Limit, "PerfBigEndianToChar",
                (b, i) => { var n = unchecked((char)IPAddress.NetworkToHostOrder(unchecked((ushort)BitConverter.ToChar(b, i)))); },
                (b, i) => { var n = BigEndian.ToChar(b, i); },
                (b) => { BigEndian.ToChar(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToSingle() {
            var ret = Duel(Limit, "PerfBigEndianToSingle",
                (b, i) => { BitConverter.ToSingle(b, i); },
                (b, i) => { BigEndian.ToSingle(b, i); },
                (b) => { BigEndian.ToSingle(b); });
            Assert.IsTrue(ret);
        }

        [TestMethod]
        public unsafe void BigEndianToDouble() {
            var ret = Duel(Limit, "PerfBigEndianToDouble",
                (b, i) => { BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(BitConverter.ToInt64(b, i))); },
                (b, i) => { BigEndian.ToDouble(b, i); },
                (b) => { BigEndian.ToDouble(b); });
            Assert.IsTrue(ret);
        }

        //
        //
        //

        void Result(string title, long std, long ptr, long alt) {
            float p = ((float)ptr - std) / Boost;
            float a = ((float)alt - std) / Boost;
            float ratio_ptr = (p + std) / std;
            float ratio_alt = (a + std) / std;
            Console.WriteLine(title + " ptr:" + ratio_ptr + " alt:" + ratio_alt);
        }

        public unsafe struct BytePointer {
            byte* Value;

            public BytePointer(byte* p) {
                Value = p;
            }
            public byte* ToPointer() {
                return Value;
            }

            public static explicit operator byte* (BytePointer n) {
                return n.Value;
            }
        }

        public unsafe delegate void DuelPointer(byte* p);

        public unsafe bool Duel(double limit, string title,
            Action<byte[], int> std,
            Action<byte[], int> alt,
            DuelPointer ptr)
        {
            fixed(byte* fix = ReadData)
            {
                var ret = true;
                for (var i = 0; i < 2; ++i) {
                    var p = fix;
                    var ret_std = Bench.Run(limit, (n) => {
                        for (var j = 0; j < Boost; ++j) std(ReadData, i);
                    });
                    var ret_atr = Bench.Run(limit, (n) => {
                        for (var j = 0; j < Boost; ++j) alt(ReadData, i);
                    });
                    var ret_ptr = Bench.Run(limit, (n) => {
                        for (var j = 0; j < Boost; ++j) ptr(p + i);
                    });
                    Result(title, ret_std, ret_ptr, ret_atr);
                    ret &= ret_ptr >= ret_std;
                    ret &= ret_atr >= ret_std;
                }
                return ret;
            }
        }

#if false
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
#endif
    }
}

