﻿/**
 * @file
 * @brief バイト・ビットに関係するシンタックスシュガーを纏めています
 */

using System;
using System.Reflection;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class SugarByte {

        /// <summary>倍精度値をビットイメージとして収納します</summary>
        public static long DoubleToInt64Bits(this double value) {
            return BitConverter.DoubleToInt64Bits(value);
        }

        /// <summary>倍精度値をビットイメージから復元します</summary>
        public static double Int64BitsToDouble(this long value) {
            return BitConverter.Int64BitsToDouble(value);
        }

        // /// <summary>バイトオーダーを反転させた値を返す</summary>
        // public static byte SwapByteOrder(this byte value) { return value; }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static ushort SwapByteOrder(this ushort value) {
            return unchecked((ushort)( //
                value >> 8 |
                value << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static uint SwapByteOrder(this uint value) {
            return ( //
                value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24);
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static ulong SwapByteOrder(this ulong value) {
            return ( //
                value >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                value << 56);
        }

        // /// <summary>バイトオーダーを反転させた値を返す</summary>
        // public static sbyte SwapByteOrder(this sbyte value) { return value; }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static short SwapByteOrder(this short value) {
            return unchecked((short)( //
                unchecked((ushort)value) >> 8 |
                unchecked((ushort)value) << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static int SwapByteOrder(this int value) {
            return unchecked((int)( //
                unchecked((uint)value) >> 24 |
                (unchecked((uint)value) & 0x00ff0000U) >> 8 |
                (unchecked((uint)value) & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static long SwapByteOrder(this long value) {
            return unchecked((long)( //
                unchecked((ulong)value) >> 56 |
                (unchecked((ulong)value) & 0x00ff000000000000UL) >> 40 |
                (unchecked((ulong)value) & 0x0000ff0000000000UL) >> 24 |
                (unchecked((ulong)value) & 0x000000ff00000000UL) >> 8 |
                (unchecked((ulong)value) & 0x00000000ff000000UL) << 8 |
                (unchecked((ulong)value) & 0x0000000000ff0000UL) << 24 |
                (unchecked((ulong)value) & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56));
        }

        /// <summary>マーシャルなオブジェクトのサイズを返します</summary>
        public static int MarshalSize(this object obj) {
            return Marshal.SizeOf(obj);
        }
    }

    //
    //
    //

    public static class ByteOrder {

        /// <summary>マーシャルなオブジェクトをバッファへ書き込み</summary>
        public static void Assign<T>(byte[] buffer, int index, T data)
            where T : class //
        {
            if (Marshal.SizeOf(data) > (buffer.Length - index)) {
                throw new Exception();
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.StructureToPtr(data, new IntPtr(fix + index), false);
                }
//                if (BitConverter.IsLittleEndian) {
//                    SwapByteOrder(typeof(T), buffer, index);
//                }
            }
        }

        /// <summary>マーシャルなオブジェクトの読み込み</summary>
        public static T To<T>(byte[] buffer, int index) {
            var tmp = new byte[buffer.Length - index];
            Buffer.BlockCopy(buffer, index, tmp, 0, tmp.Length);
//            if (BitConverter.IsLittleEndian) {
//                SwapByteOrder(typeof(T), tmp, index);
//            }
            unsafe
            {
                fixed (byte* fix = tmp)
                {
                    return (T)Marshal.PtrToStructure(new IntPtr(fix), typeof(T));
                }
            }
        }

        /// <summary>型情報をもとにバイトオーダーを反転させます</summary>
        static void SwapByteOrder(Type type, byte[] buffer, int index) {
            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; ++i) {
                var f = fields[i];
                if (f.IsStatic) {
                    continue;
                }
                else if (f.FieldType == typeof(string)) {
                    // TODO ごにょ enum もごにょ
                }
                // 子の確認
                bool has_child = false;
                {
                    var child = f.FieldType.GetFields();
                    for (var j = 0; !has_child && j < child.Length; ++j) {
                        has_child = !child[j].IsStatic;
                    }
                }
                var offset = Marshal.OffsetOf(type, f.Name).ToInt32();
                if (has_child) {
                    SwapByteOrder(f.FieldType, buffer, index + offset);
                }
                else {
                    Array.Reverse(buffer, index + offset, Marshal.SizeOf(f.FieldType));
                }
            }
        }

#if false

    [AttributeUsage(AttributeTargets.Class)]
    public class ByteOrderAttribute : Attribute {
        public bool IsLittleEndian { get; private set; }
        public ByteOrderAttribute(ByteOrderType bo) {
            IsLittleEndian = (bo == ByteOrderType.LittleEndian);
        }
    }

    public enum ByteOrderType {
        BigEndian,
        LittleEndian
    }

        /// <summary>バイトオーダーを調整します</summary>
        static void Adjust(Type type, byte[] buffer, int index) {
            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; ++i) {
                var f = fields[i];
                if (!f.IsDefined(typeof(ByteOrderAttribute), false)) continue;
                //
                var attr = (ByteOrderAttribute)f.GetCustomAttributes(
                    typeof(ByteOrderAttribute), false)[0];
                var offset = Marshal.OffsetOf(type, f.Name).ToInt32();
                if (attr.IsLittleEndian != BitConverter.IsLittleEndian) {
                    Array.Reverse(buffer, index + offset, Marshal.SizeOf(f.FieldType));
                }
            }
        }
#endif
    }


}
