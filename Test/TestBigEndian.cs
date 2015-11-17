using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {
    using System.Reflection;
    using Order = BigEndianAny;

    interface IEndianess {
         unsafe byte ToUInt8(byte* b);
#if false
        unsafe sbyte ToInt8(byte* b);
        unsafe bool ToBool(byte* b);
        unsafe UInt16 ToUnt16(byte* b);
        unsafe Int16 ToInt16(byte* b);
        unsafe char ToChar(byte* b);

        unsafe UInt32 ToUnt32(byte* b);
        unsafe Int32 ToInt32(byte* b);
        unsafe float ToSingle(byte* b);

        unsafe UInt64 ToUnt64(byte* b);
        unsafe Int64 ToInt64(byte* b);
        unsafe double ToDouble(byte* b);

        unsafe byte ToUInt8(byte[] b, int i);
        unsafe sbyte ToInt8(byte[] b, int i);
        unsafe bool ToBool(byte[] b, int i);

        unsafe UInt16 ToUnt16(byte[] b, int i);
        unsafe Int16 ToInt16(byte[] b, int i);
        unsafe char ToChar(byte[] b, int i);

        unsafe UInt32 ToUnt32(byte[] b, int i);
        unsafe Int32 ToInt32(byte[] b, int i);
        unsafe float ToSingle(byte[] b, int i);

        unsafe UInt64 ToUnt64(byte[] b, int i);
        unsafe Int64 ToInt64(byte[] b, int i);
        unsafe double ToDouble(byte[] b, int i);
#endif
    }


    public class EndiannessReflection {
        Type Type;

        public EndiannessReflection(Type type) {
            Type = type;
        }

        public unsafe T To<T>(string name, byte[] b, int i) //
            where T : struct //
        {
            // Type type = Type.GetType(inputString);
            // object o = Activator.CreateInstance(type);
            var ty = Type.GetType("System." + name);
            var n = Type.GetMethod("To" + name, new Type[] { b.GetType(), i.GetType() });
            var ret = n.Invoke(null, new object[] { b, i });
            //return Convert.ChangeType(ret, ty);
            return (T)ret;
        }

        public unsafe UInt16 ToUInt16(byte[] b, int i) {
            return To<UInt16>("UInt16", b, i);
        }

        public unsafe UInt32 ToUInt32(byte[] b, int i) {
            return To<UInt32>("UInt32", b, i);
        }

        public unsafe UInt64 ToUInt64(byte[] b, int i) {
            return To<UInt64>("UInt64", b, i);
        }

        public unsafe Int16 ToInt16(byte[] b, int i) {
            return To<Int16>("Int16", b, i);
        }

        public unsafe Int32 ToInt32(byte[] b, int i) {
            return To<Int32>("Int32", b, i);
        }

        public unsafe Int64 ToInt64(byte[] b, int i) {
            return To<Int64>("Int64", b, i);
        }

        public unsafe char ToChar(byte[] b, int i) {
            return To<char>("Char", b, i);
        }

        public unsafe bool ToBool(byte[] b, int i) {
            return To<bool>("Bool", b, i);
        }

        public unsafe Single ToSingle(byte[] b, int i) {
            return To<Single>("Single", b, i);
        }

        public unsafe Double ToDouble(byte[] b, int i) {
            return To<Double>("Double", b, i);
        }
    }

    public partial class Endianness : Values {

        byte[] MakeLittle(int pad, byte[] buffer, int take) {
            var tmp = Enumerable.Range(0, pad).Select(x => x.CastUInt8()).ToList();
            tmp.AddRange(buffer.Take(take).Reverse());
            return tmp.ToArray();
        }

        byte[] MakeBig(int pad, byte[] buffer, int take) {
            var tmp = Enumerable.Range(0, pad).Select(x => x.CastUInt8()).ToList();
            tmp.AddRange(buffer.Take(take));
            return tmp.ToArray();
        }

        public unsafe void Endian(Type type, Func<int, byte[], int, byte[]> fn) {
            for (var i = 0; i < 8; ++i) {
                var e = new EndiannessReflection(type);
                byte[] t;
                t = fn(i, values, 2);
                Assert.AreEqual(u16, e.ToUInt16(t, i));
                t = fn(i, values, 4);
                Assert.AreEqual(u32, e.ToUInt32(t, i));
                t = fn(i, values, 8);
                Assert.AreEqual(u64, e.ToUInt64(t, i));
                t = fn(i, values, 2);
                Assert.AreEqual(s16, e.ToInt16(t, i));
                t = fn(i, values, 4);
                Assert.AreEqual(s32, e.ToInt32(t, i));
                t = fn(i, values, 8);
                Assert.AreEqual(s64, e.ToInt64(t, i));
                t = fn(i, chars, 2);
                Assert.AreEqual(c16, e.ToChar(t, i));

                t = BitConverter.GetBytes(1.1f).Reverse().ToArray();
                Assert.AreEqual(f32, e.ToSingle(fn(i, t, 4), i));
                t = BitConverter.GetBytes(1.2).Reverse().ToArray();
                Assert.AreEqual(f64, e.ToDouble(fn(i, t, 8), i));
            }
        }

        [TestMethod]
        public unsafe void Endianness_() {
            Endian(typeof(BigEndianAny), MakeBig);
            Endian(typeof(LittleEndianAny), MakeLittle);
            if (BitConverter.IsLittleEndian) {
                Endian(typeof(HostOrderAligned), MakeLittle);
            }
        }

        [TestMethod]
        public unsafe void BigEndianAny_() {

            // 読み込みの確認
            for (var i = 0; i < 8; ++i) {
                var tmp = new List<byte>();
                tmp.AddRange(Enumerable.Range(0, i).Select(e => e.CastUInt8()));
                tmp.AddRange(Big);
                var src = tmp.ToArray();

                fixed (byte* p = src)
                {
                    Assert.AreEqual<byte>(Order.ToUInt8(p + i + 14), (byte)0x88);
                    Assert.AreEqual<UInt16>(Order.ToUInt16(p + i + 12), (UInt16)0x9988);
                    Assert.AreEqual<UInt32>(Order.ToUInt32(p + i + 8), (UInt32)0xbbaa9988);
                    Assert.AreEqual<UInt64>(Order.ToUInt64(p + i + 0), (UInt64)0xffeeddccbbaa9988);
                    Assert.AreEqual<sbyte>(Order.ToInt8(p + i + 14), unchecked((sbyte)0x88));
                    Assert.AreEqual<Int16>(Order.ToInt16(p + i + 12), unchecked((Int16)0x9988));
                    Assert.AreEqual<Int32>(Order.ToInt32(p + i + 8), unchecked((Int32)0xbbaa9988));
                    Assert.AreEqual<Int64>(Order.ToInt64(p + i + 0), unchecked((Int64)0xffeeddccbbaa9988));
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

