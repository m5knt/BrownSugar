﻿/**
 * @file
 * @brief バイトオーダー操作関係
 */

using System;
using System.Reflection;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>バイトオーダー操作関係</summary>
    public static class ByteOrder {

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static char Swap(char value) {
            return unchecked((char)( //
                value >> 8 |
                value << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static UInt16 Swap(UInt16 value) {
            return unchecked((UInt16)( //
                value >> 8 |
                value << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static UInt32 Swap(UInt32 value) {
            return ( //
                value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24);
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static UInt64 Swap(UInt64 value) {
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
        public static Int16 Swap(Int16 value) {
            return unchecked((Int16)( //
                unchecked((ushort)value) >> 8 |
                unchecked((ushort)value) << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static Int32 Swap(Int32 value) {
            return unchecked((Int32)( //
                unchecked((uint)value) >> 24 |
                (unchecked((uint)value) & 0x00ff0000U) >> 8 |
                (unchecked((uint)value) & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static Int64 Swap(Int64 value) {
            return unchecked((Int64)( //
                unchecked((ulong)value) >> 56 |
                (unchecked((ulong)value) & 0x00ff000000000000UL) >> 40 |
                (unchecked((ulong)value) & 0x0000ff0000000000UL) >> 24 |
                (unchecked((ulong)value) & 0x000000ff00000000UL) >> 8 |
                (unchecked((ulong)value) & 0x00000000ff000000UL) << 8 |
                (unchecked((ulong)value) & 0x0000000000ff0000UL) << 24 |
                (unchecked((ulong)value) & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56));
        }

        /// <summary>バッファのバイトオーダーを反転させます</summary>
        public static unsafe void Swap(byte* buffer, int length) {
            // 値の反転
            var p = buffer;
            var q = buffer + length - 1;
            for (var j = length / 2; --j >= 0; ++p, --q) {
                var t = *p;
                *p = *q;
                *q = t;
            }
        }

        /// <summary>バッファのバイトオーダーを反転させます</summary>
        public static void Swap(byte[] buffer, int index, int length) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0 || length < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException("index or length");
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Swap(fix, length);
                }
            }
        }

        /// <summary>
        /// バッファのバイトオーダーを反転させます
        /// CharSet.Autoはサポートしていません
        /// </summary>
        public static void Swap<T>(byte[] buffer, int index, T obj) {
            Swap(buffer, index, typeof(T));
        }

        /// <summary>
        /// バッファのバイトオーダーを反転させます
        /// CharSet.Autoはサポートしていません
        /// </summary>
        public static void Swap(byte[] buffer, int index, Type type) {
            if (buffer == null || type == null) {
                throw new ArgumentNullException("buffer or type");
            }
            var length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException("index");
            }
            unsafe
            {
                fixed(byte* fix = buffer)
                {
                    Swap(fix + index, type);
                }
            }
        }

        /// <summary>
        /// バッファのバイトオーダーを反転させます
        /// CharSet.Autoはサポートしていません
        /// </summary>
        /// <exception cref="InvalidOperationException">CharSet.Auto指定時</exception>
        public static unsafe void Swap(byte* buffer, Type type) {
            // 文字セットの確認
            var cs = type.StructLayoutAttribute.CharSet;
            if (cs == CharSet.Auto) {
                throw new InvalidOperationException("not support Charset.Auto");
            }
            var is_unicode = (cs == CharSet.Unicode);
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for (var i = 0; i < fields.Length; ++i) {
                var f = fields[i];
                if (f.IsStatic) {
                    continue;
                }
                var ty = f.FieldType;
                var offset = Marshal.OffsetOf(type, f.Name).ToInt32();

                // 文字列用処理
                if (ty == typeof(string)) {
                    // utf16処理
                    if (is_unicode) {
                        var attribs = f.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                        var attrib = (MarshalAsAttribute)attribs[0];
                        var p = buffer + offset;
                        for (var j = attrib.SizeConst; --j >= 0; p += 2) {
                            var t = p[0];
                            p[0] = p[1];
                            p[1] = t;
                        }
                    }
                    continue;
                }

                // 文字処理
                if (ty == typeof(char)) {
                    var t = buffer[offset];
                    buffer[offset] = buffer[offset + 1];
                    buffer[offset + 1] = t;
                    continue;
                }

                // 配列用処理
                if (ty.IsArray) {
                    var elem_type = ty.GetElementType();
                    var elem_size = Marshal.SizeOf(elem_type);
                    // 1バイト配列なら何もしない
                    if (elem_size <= 1) {
                        continue;
                    }
                    var attribs = f.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                    var attrib = (MarshalAsAttribute)attribs[0];
                    for (var j = attrib.SizeConst; --j >= 0;) {
                        Swap(buffer + offset, elem_type);
                        offset += elem_size;
                    }
                    continue;
                }

                // 列挙型は基礎型にする
                if (ty.IsEnum) {
                    ty = Enum.GetUnderlyingType(ty);
                }

                // 1バイトなら何もしない
                var size = Marshal.SizeOf(ty);
                if (size <= 1) {
                    continue;
                }

                if (!ty.IsEnum) {
                    // ネストしているか確認
                    var nest_fields = ty.GetFields();
                    bool is_nest = false;
                    for (var j = nest_fields.Length; --j >= 0 && !is_nest;) {
                        is_nest = !nest_fields[j].IsStatic;
                    }
                    // ネストなら再帰
                    if (is_nest) {
                        Swap(buffer + offset, ty);
                        continue;
                    }
                }
                // 反転
                Swap(buffer + offset, size);
            }
        }

        //
        //
        //
#if false
        /// <summary>マーシャルアトリビュートのサイズカウントを返す</summary>
        public static int MarshalCount(Type type, string name) {
            var field = type.GetField(name);
            var field_type = field.FieldType;
            if (field_type == typeof(string)) {
                // 文字列
                var attribs = field.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                var attrib = (MarshalAsAttribute)attribs[0];
                return attrib.SizeConst;
            }
            else if (field_type.IsArray) {
                // 配列
                var attribs = field.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                var attrib = (MarshalAsAttribute)attribs[0];
                return attrib.SizeConst;
            }
            return 0;
        }

        /// <summary>マーシャルアトリビュートのサイズカウントを返す</summary>
        public static int MarshalCount<T>(T obj, string name) {
            return MarshalCount(typeof(T), name);
        }
#endif
        /// <summary>オブジェクトをバイナリ化しバッファへ書き込む</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int MarshalAssign<T>(byte[] buffer, int index, T obj, //
            bool is_little = true) //
        {
            if (buffer == null || obj == null) {
                throw new ArgumentNullException("buffer or obj");
            }
            var type = typeof(T);
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException("index");
            }
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    Marshal.StructureToPtr(obj, new IntPtr(p), false);
                    if (is_little ^ BitConverter.IsLittleEndian) {
                        Swap(p, type);
                    }
                }
            }
            return length;
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] MarshalGetBytes<T>(T obj, bool is_little = true) {
            if (obj == null) {
                throw new ArgumentNullException("obj");
            }
            var type = typeof(T);
            int length = Marshal.SizeOf(typeof(T));
            var buffer = new byte[length];
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.StructureToPtr(obj, new IntPtr(fix), false);
                    if (is_little ^ BitConverter.IsLittleEndian) {
                        Swap(fix, type);
                    }
                }
            }
            return buffer;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int MarshalCopyTo<T>(byte[] buffer, int index, T obj, //
            bool is_little = true) //
            where T : class //
        {
            if (buffer == null || obj == null) {
                throw new ArgumentNullException("buffer or obj");
            }
            var type = typeof(T);
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException("index");
            }
            unsafe
            {
                if (is_little ^ BitConverter.IsLittleEndian) {
                    fixed (byte* fix = (byte[])buffer.Clone())
                    {
                        var p = fix + index;
                        Swap(p, type);
                        Marshal.PtrToStructure(new IntPtr(p), obj);
                    }
                }
                else {
                    fixed (byte* fix = buffer)
                    {
                        var p = fix + index;
                        Marshal.PtrToStructure(new IntPtr(p), obj);
                    }
                }
            }
            return length;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T MarshalTo<T>(byte[] buffer, int index, bool is_little = true) {
            var type = typeof(T);
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException("index");
            }
            unsafe
            {
                if (is_little ^ BitConverter.IsLittleEndian) {
                    fixed (byte* fix = (byte[])buffer.Clone())
                    {
                        var p = fix + index;
                        Swap(p, type);
                        return (T)Marshal.PtrToStructure(new IntPtr(p), type);
                    }
                }
                else { 
                    fixed (byte* fix = buffer)
                    {
                        var p = fix + index;
                        return (T)Marshal.PtrToStructure(new IntPtr(p), type);
                    }
                }
            }
        }

        //
        //
        //

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static ushort ToNetOrder(ushort value) {
            return BitConverter.IsLittleEndian ?
                unchecked((ushort)( //
                value >> 8 |
                value << 8)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static uint ToNetOrder(uint value) {
            return BitConverter.IsLittleEndian ?
                (value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static ulong ToNetOrder(ulong value) {
            return BitConverter.IsLittleEndian ?
                (value >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                value << 56) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static short ToNetOrder(short value) {
            return BitConverter.IsLittleEndian ?
                unchecked((short)( //
                unchecked((ushort)value) >> 8 |
                value << 8)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static int ToNetOrder(int value) {
            return BitConverter.IsLittleEndian ?
                unchecked((int)( //
                unchecked((uint)value) >> 24 |
                (unchecked((uint)value) & 0x00ff0000U) >> 8 |
                (unchecked((uint)value) & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static long ToNetOrder(long value) {
            return BitConverter.IsLittleEndian ?
                unchecked((long)( //
                unchecked((ulong)value) >> 56 |
                (unchecked((ulong)value) & 0x00ff000000000000UL) >> 40 |
                (unchecked((ulong)value) & 0x0000ff0000000000UL) >> 24 |
                (unchecked((ulong)value) & 0x000000ff00000000UL) >> 8 |
                (unchecked((ulong)value) & 0x00000000ff000000UL) << 8 |
                (unchecked((ulong)value) & 0x0000000000ff0000UL) << 24 |
                (unchecked((ulong)value) & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56)) : value;
        }

        //
        //
        //

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static ushort ToHostOrder(ushort value) {
            return BitConverter.IsLittleEndian ? //
                unchecked((ushort)( //
                value >> 8 |
                value << 8)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static uint ToHostOrder(uint value) {
            return BitConverter.IsLittleEndian ? //
                (value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static ulong ToHostOrder(ulong value) {
            return BitConverter.IsLittleEndian ? //
                (value >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                value << 56) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static short ToHostOrder(short value) {
            return BitConverter.IsLittleEndian ?
                unchecked((short)( //
                unchecked((ushort)value) >> 8 |
                value << 8)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static int ToHostOrder(int value) {
            return BitConverter.IsLittleEndian ?
                unchecked((int)( //
                unchecked((uint)value) >> 24 |
                unchecked((uint)value & 0x00ff0000U) >> 8 |
                unchecked((uint)value & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static long ToHostOrder(long value) {
            return BitConverter.IsLittleEndian ?
                unchecked((long)( //
                unchecked((ulong)value) >> 56 |
                unchecked((ulong)value & 0x00ff000000000000UL) >> 40 |
                unchecked((ulong)value & 0x0000ff0000000000UL) >> 24 |
                unchecked((ulong)value & 0x000000ff00000000UL) >> 8 |
                unchecked((ulong)value & 0x00000000ff000000UL) << 8 |
                unchecked((ulong)value & 0x0000000000ff0000UL) << 24 |
                unchecked((ulong)value & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56)) : value;
        }

    }
}
