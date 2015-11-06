﻿/**
 * @file
 */

using System;
using System.Reflection;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class ByteOrder {

        public static ushort Swap(ushort value) {
            return unchecked((ushort)( //
                value >> 8 |
                value << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static uint Swap(uint value) {
            return ( //
                value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24);
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static ulong Swap(ulong value) {
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

        //
        //
        //

        /// <summary>
        /// 型のサイズを求めます
        /// PODもしくはマーシャルアトリビュートがある型が扱えます
        /// </summary>
        /// <seealso cref="Marshal.SizeOf(object)"/>
        public static int SizeOf<T>(T obj) {
            return Marshal.SizeOf(obj);
        }

        /// <summary>オブジェクトをバイナリ化しバッファへ書き込む</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int Assign<T>(byte[] buffer, int index, T obj) {
            if (buffer == null || obj == null) {
                throw new ArgumentNullException();
            }
            int length = Marshal.SizeOf(typeof(T));
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    Marshal.StructureToPtr(obj, new IntPtr(fix), false);
                }
            }
            return length;
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] GetBytes<T>(T obj) {
            if (obj == null) {
                throw new ArgumentNullException();
            }
            int length = Marshal.SizeOf(typeof(T));
            var buffer = new byte[length];
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.StructureToPtr(obj, new IntPtr(fix), false);
                }
            }
            return buffer;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int CopyTo<T>(byte[] buffer, int index, T obj)
            where T : class //
        {
            if (buffer == null || obj == null) {
                throw new ArgumentNullException();
            }
            var type = typeof(T);
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    Marshal.PtrToStructure(new IntPtr(fix), obj);
                }
            }
            return length;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T To<T>(byte[] buffer, int index) {
            var type = typeof(T);
            if (buffer == null) {
                throw new ArgumentNullException();
            }
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    return (T)Marshal.PtrToStructure(new IntPtr(fix), type);
                }
            }
        }

        static Type MarshalAttributeType = typeof(MarshalAsAttribute);

        /// <summary>型情報をもとにバイトオーダーを反転させます</summary>
        public static void Swap(byte[] buffer, int index, Type type) {
            var fields = type.GetFields();
            for (var i = 0; i < fields.Length; ++i) {
                var f = fields[i];
                if (f.IsStatic) {
                    continue;
                }
                var ty = f.FieldType;
                var offset = index + Marshal.OffsetOf(type, f.Name).ToInt32();

                // 配列か確認
                if (ty.IsArray) {
                    var elem_type = ty.GetElementType();
                    var elem_size = Marshal.SizeOf(elem_type);
                    // 1バイトなら何もしない
                    if (elem_size <= 1) {
                        continue;
                    }
                    var attribute = f.GetCustomAttributes(MarshalAttributeType, false);
                    var marshal = (MarshalAsAttribute)attribute[0];
                    for(var j = 0; j < marshal.SizeConst; ++j) {
                        Swap(buffer, offset, elem_type);
                        offset += elem_size;
                    }
                    continue;
                }

                // ネストしているか確認
                var nest_fields = ty.GetFields();
                bool has_nest = false;
                for (var j = nest_fields.Length; --j >= 0 && !has_nest;) {
                    has_nest = !nest_fields[j].IsStatic;
                }
                if (has_nest) {
                    Swap(buffer, offset, ty);
                    continue;
                }

                // 1バイトなら何もしない
                var size = Marshal.SizeOf(ty);
                if (size <= 1) {
                    continue;
                }

                // 値の反転
                Array.Reverse(buffer, offset, size);
            }
        }
    }
}
