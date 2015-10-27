using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ThunderEgg.BrownSugar;

namespace Test {

    using Order = HostOrderAligned;

    [TestClass]
    public partial class ByteEndian {

        byte[] Little = new byte[] {
                0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, // 0: long
                0x88, 0x99, 0xaa, 0xbb, // 8: int
                0x88, 0x99, // 12: short
                0x88,       // 14: byte
                0x00,       // 15: bool
                0x40, 0x00, // 16: char
                0x00, 0x00, 0x00, 0x00, // 20: float
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 24: double
            };

        byte[] Big = new byte[] {
                0xff, 0xee, 0xdd, 0xcc, 0xbb, 0xaa, 0x99, 0x88, // 0: long
                0xbb, 0xaa, 0x99, 0x88, // 8: int
                0x99, 0x88, // 12: short
                0x88,       // 14: byte
                0x00,       // 15: bool
                0x00, 0x40, // 16: char
                0x00, 0x00, 0x00, 0x00, // 20: float
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 24: double
            };

        [TestMethod]
        public unsafe void TestHostOrder() {

            // 読み込みの確認
            for (var i = 0; i < 8; ++i) {
                var tmp = new List<byte>();
                tmp.AddRange(Enumerable.Range(0, i).Select(e => e.CastUInt8()));
                tmp.AddRange(Little);
                var src = tmp.ToArray();

                fixed (byte* p = src)
                {
                    Assert.AreEqual<byte>(Order.ToUInt8(p + i + 14), (byte)0x88);
                    Assert.AreEqual<ushort>(Order.ToUInt16(p + i + 12), (ushort)0x9988);
                    Assert.AreEqual<uint>(Order.ToUInt32(p + i + 8), (uint)0xbbaa9988);
                    Assert.AreEqual<ulong>(Order.ToUInt64(p + i + 0), (ulong)0xffeeddccbbaa9988);
                    Assert.AreEqual<sbyte>(Order.ToInt8(p + i + 14), unchecked((sbyte)0x88));
                    Assert.AreEqual<int>(Order.ToInt32(p + i + 8), unchecked((int)0xbbaa9988));
                    Assert.AreEqual<short>(Order.ToInt16(p + i + 12), unchecked((short)0x9988));
                    Assert.AreEqual<long>(Order.ToInt64(p + i + 0), unchecked((long)0xffeeddccbbaa9988));
                    Assert.AreEqual<bool>(Order.ToBoolean(p + i + 15), false);
                    Assert.AreEqual<char>(Order.ToChar(p + i + 16), '@');
                    Order.Assign(p + i + 20, 1.1f);
                    Order.Assign(p + i + 24, 2.2);
                    Assert.AreEqual<float>(Order.ToSingle(p + i + 20), 1.1f);
                    Assert.AreEqual<double>(Order.ToDouble(p + i + 24), 2.2);
                }
                Assert.AreEqual<ulong>(Order.ToUInt64(src, 0 + i), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual<uint>(Order.ToUInt32(src, 8 + i), (uint)0xbbaa9988);
                Assert.AreEqual<ushort>(Order.ToUInt16(src, 12 + i), (ushort)0x9988);
                Assert.AreEqual<byte>(Order.ToUInt8(src, 14 + i), (byte)0x88);
                Assert.AreEqual<long>(Order.ToInt64(src, 0 + i), unchecked((long)0xffeeddccbbaa9988));
                Assert.AreEqual<int>(Order.ToInt32(src, 8 + i), unchecked((int)0xbbaa9988));
                Assert.AreEqual<short>(Order.ToInt16(src, 12 + i), unchecked((short)0x9988));
                Assert.AreEqual<sbyte>(Order.ToInt8(src, 14 + i), unchecked((sbyte)0x88));

                var seg = new ArraySegment<byte>(src, i, src.Length - i);
                Assert.AreEqual<ulong>(Order.ToUInt64(seg, 0), (ulong)0xffeeddccbbaa9988);
                Assert.AreEqual<uint>(Order.ToUInt32(seg, 8), (uint)0xbbaa9988);
                Assert.AreEqual<ushort>(Order.ToUInt16(seg, 12), (ushort)0x9988);
                Assert.AreEqual<byte>(Order.ToUInt8(seg, 14), (byte)0x88);
                Assert.AreEqual<long>(Order.ToInt64(seg, 0), unchecked((long)0xffeeddccbbaa9988));
                Assert.AreEqual<int>(Order.ToInt32(seg, 8), unchecked((int)0xbbaa9988));
                Assert.AreEqual<short>(Order.ToInt16(seg, 12), unchecked((short)0x9988));
                Assert.AreEqual<sbyte>(Order.ToInt8(seg, 14), unchecked((sbyte)0x88));

            }
        }
    }
}

