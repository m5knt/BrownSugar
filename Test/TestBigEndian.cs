using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extends;

namespace Test {

    using Order = BigEndianAny;

    public partial class ByteEndian {

        [TestMethod]
        public unsafe void TestBigEndian() {

            // 読み込みの確認
            for (var i = 0; i < 8; ++i) {
                var tmp = new List<byte>();
                tmp.AddRange(Enumerable.Range(0, i).Select(e => e.CastUInt8()));
                tmp.AddRange(Big);
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
                Assert.AreEqual(Order.ToBoolean(src, 14 + i), true);
                Assert.AreEqual(Order.ToBoolean(src, 15 + i), false);

                byte[] hoge;
                hoge = new byte[i + 1];
                Order.Assign(hoge, i, UInt8);
                Assert.AreEqual(Order.ToUInt8(hoge, i), UInt8);
                hoge = new byte[i + 2];
                Order.Assign(hoge, i, UInt16);
                Assert.AreEqual(Order.ToUInt16(hoge, i), UInt16);
                hoge = new byte[i + 4];
                Order.Assign(hoge, i, UInt32);
                Assert.AreEqual(Order.ToUInt32(hoge, i), UInt32);
                hoge = new byte[i + 8];
                Order.Assign(hoge, i, UInt64);
                Assert.AreEqual(Order.ToUInt64(hoge, i), UInt64);

                hoge = new byte[i + 1];
                Order.Assign(hoge, i, Int8);
                Assert.AreEqual(Order.ToInt8(hoge, i), Int8);
                hoge = new byte[i + 2];
                Order.Assign(hoge, i, Int16);
                Assert.AreEqual(Order.ToInt16(hoge, i), Int16);
                hoge = new byte[i + 4];
                Order.Assign(hoge, i, Int32);
                Assert.AreEqual(Order.ToInt32(hoge, i), Int32);
                hoge = new byte[i + 8];
                Order.Assign(hoge, i, Int64);
                Assert.AreEqual(Order.ToInt64(hoge, i), Int64);

                hoge = new byte[i + 1];
                Order.Assign(hoge, i, UInt8);
                Assert.AreEqual(Order.ToUInt8(hoge, i), UInt8);
                hoge = new byte[i + 2];
                Order.Assign(hoge, i, UInt16);
                Assert.AreEqual(Order.ToUInt16(hoge, i), UInt16);
                hoge = new byte[i + 4];
                Order.Assign(hoge, i, UInt32);
                Assert.AreEqual(Order.ToUInt32(hoge, i), UInt32);
                hoge = new byte[i + 8];
                Order.Assign(hoge, i, UInt64);
                Assert.AreEqual(Order.ToUInt64(hoge, i), UInt64);

                hoge = new byte[i + 1];
                Order.Assign(hoge, i, Boolean);
                Assert.AreEqual(Order.ToBoolean(hoge, i), Boolean);
                hoge = new byte[i + 2];
                Order.Assign(hoge, i, Char);
                Assert.AreEqual(Order.ToChar(hoge, i), Char);
                hoge = new byte[i + 4];
                Order.Assign(hoge, i, Single);
                Assert.AreEqual(Order.ToSingle(hoge, i), Single);
                hoge = new byte[i + 8];
                Order.Assign(hoge, i, Double);
                Assert.AreEqual(Order.ToDouble(hoge, i), Double);
            }
        }
    }
}

