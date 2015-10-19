using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    [TestClass]
    public class TestEndian {

        byte[] Aligned = new byte[] {
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, // 0: long
                0x88, 0x99, 0xaa, 0xbb, // 8: int
                0x88, 0x99, // 12: short
                0x88,       // 14: byte
                0x00,       // 15: bool
                0x40, 0x00, // 16: char
                0x00, 0x00, 0x00, 0x00, // 20: float
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 24: double
            };

        byte[] Offset {
            get {
                return Offset_ ?? (Offset_ = 0.CastUInt8().ToList().Add(Aligned).ToArray());
            }
        }
        byte[] Offset_;

        [TestMethod]
        public unsafe void TestLitleEndian() {

            // 読み込みの確認
            for (var i = 0; i < 2; ++i) {
                var src = i == 0 ? Aligned : Offset;
                fixed (byte* p = src)
                {
                    Assert.AreEqual(LittleEndian.ToUInt8(p + i + 14), (byte)0x88);
                    Assert.AreEqual(LittleEndian.ToUInt16(p + i + 12), (ushort)0x9988);
                    Assert.AreEqual(LittleEndian.ToUInt32(p + i + 8), (uint)0xbbaa9988);
                    Assert.AreEqual(LittleEndian.ToUInt64(p + i + 0), (ulong)0xffeeddccbbaa9988);
                    Assert.AreEqual(LittleEndian.ToInt8(p + i + 14), unchecked((sbyte)0x88));
                    Assert.AreEqual(LittleEndian.ToInt32(p + i + 8), unchecked((int)0xbbaa9988));
                    Assert.AreEqual(LittleEndian.ToInt16(p + i + 12), unchecked((short)0x9988));
                    Assert.AreEqual(LittleEndian.ToInt64(p + i + 0), unchecked((long)0xffeeddccbbaa9988));
                    Assert.AreEqual(LittleEndian.ToBoolean(p + i + 15), false);
                    Assert.AreEqual(LittleEndian.ToChar(p + i + 16), '@');
                    LittleEndian.Assign(p + i + 20, 1.1f);
                    LittleEndian.Assign(p + i + 24, 2.2);
//                    Assert.AreEqual(LittleEndian.ToSingle(p + i + 20), 1.1f);
                    Assert.AreEqual(LittleEndian.ToDouble(p + i + 24), 2.2);
                }
#if false
                Assert.AreEqual(LittleEndian.ToUInt64(src, 0 + i), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual(LittleEndian.ToUInt32(src, 8 + i), (uint)0xbbaa9988);
                Assert.AreEqual(LittleEndian.ToUInt16(src, 12 + i), (ushort)0x9988);
                Assert.AreEqual(LittleEndian.ToUInt8(src, 14 + i), (byte)0x88);
                Assert.AreEqual(LittleEndian.ToInt64(src, 0 + i), unchecked((long)0xffeeddccbbaa9988));
                Assert.AreEqual(LittleEndian.ToInt32(src, 8 + i), unchecked((int)0xbbaa9988));
                Assert.AreEqual(LittleEndian.ToInt16(src, 12 + i), unchecked((short)0x9988));
                Assert.AreEqual(LittleEndian.ToInt8(src, 14 + i), unchecked((sbyte)0x88));
#endif
            }
        }

        [TestMethod]
        public unsafe void TestBigEndian() {

            // 読み込みの確認
            for (var i = 0; i < 2; ++i) {
                var src = i == 0 ? Aligned : Offset;
#if false
                Assert.AreEqual(BigEndian.ToUInt64(src, 0 + i), (ulong)0x8899aabbccddeeff);
                Assert.AreEqual(BigEndian.ToUInt32(src, 8 + i), (uint)0x8899aabb);
                Assert.AreEqual(BigEndian.ToUInt16(src, 12 + i), (ushort)0x8899);
                Assert.AreEqual(BigEndian.ToUInt8(src, 14 + i), (byte)0x88);
                Assert.AreEqual(BigEndian.ToInt64(src, 0 + i), unchecked((long)0x8899aabbccddeeff));
                Assert.AreEqual(BigEndian.ToInt32(src, 8 + i), unchecked((int)0x8899aabb));
                Assert.AreEqual(BigEndian.ToInt16(src, 12 + i), unchecked((short)0x8899));
                Assert.AreEqual(BigEndian.ToInt8(src, 14 + i), unchecked((sbyte)0x88));
#endif
                fixed (byte* p = src)
                {
                    Assert.AreEqual(BigEndian.ToUInt64(p + i + 0), (ulong)0x8899aabbccddeeff);
                    Assert.AreEqual(BigEndian.ToUInt32(p + i + 8), (uint)0x8899aabb);
                    Assert.AreEqual(BigEndian.ToUInt16(p + i + 12), (ushort)0x8899);
                    Assert.AreEqual(BigEndian.ToUInt8(p + i + 14), (byte)0x88);
                    Assert.AreEqual(BigEndian.ToInt64(p + i + 0), unchecked((long)0x8899aabbccddeeff));
                    Assert.AreEqual(BigEndian.ToInt32(p + i + 8), unchecked((int)0x8899aabb));
                    Assert.AreEqual(BigEndian.ToInt16(p + i + 12), unchecked((short)0x8899));
                    Assert.AreEqual(BigEndian.ToInt8(p + i + 14), unchecked((sbyte)0x88));
                    Assert.AreEqual(BigEndian.ToBoolean(p + i + 15), false);
                    Assert.AreEqual(BigEndian.ToChar(p + i + 16), (char)('@' << 8));
                    BigEndian.Assign(p + i + 20, 1.1f);
                    BigEndian.Assign(p + i + 24, 2.2);
//                    Assert.AreEqual(BigEndian.ToSingle(p + i + 20), 1.1f);
                    Assert.AreEqual(BigEndian.ToDouble(p + i + 24), 2.2);
                }
            }
        }

#if false


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

