/**
 * @file
 * @brief バイトオーダー操作関係
 */

using System;
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
                throw new ArgumentNullException();
            }
            if (index < 0 || length < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
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
        public static void Swap(byte[] buffer, int index, Type type) {
            if (buffer == null || type == null) {
                throw new ArgumentNullException();
            }
            var length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
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
            var fields = type.GetFields();
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
                    // バイト配列なら何もしない
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

                // 1バイトなら何もしない
                var size = Marshal.SizeOf(ty);
                if (size <= 1) {
                    continue;
                }

                // ネストしているか確認
                var nest_fields = ty.GetFields();
                bool has_nest = false;
                for (var j = nest_fields.Length; --j >= 0 && !has_nest;) {
                    has_nest = !nest_fields[j].IsStatic;
                }
                if (has_nest) {
                    Swap(buffer + offset, ty);
                    continue;
                }

                // 反転
                Swap(buffer + offset, size);
            }
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
        public static int Assign<T>(byte[] buffer, int index, T obj, //
            bool is_littleendian = true) //
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
                    Marshal.StructureToPtr(obj, new IntPtr(p), false);
                    if (is_littleendian ^ BitConverter.IsLittleEndian) {
                        Swap(p, type);
                    }
                }
            }
            return length;
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] GetBytes<T>(T obj, bool is_littleendian = true) {
            if (obj == null) {
                throw new ArgumentNullException();
            }
            var type = typeof(T);
            int length = Marshal.SizeOf(typeof(T));
            var buffer = new byte[length];
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.StructureToPtr(obj, new IntPtr(fix), false);
                    if (is_littleendian ^ BitConverter.IsLittleEndian) {
                        Swap(fix, type);
                    }
                }
            }
            return buffer;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int CopyTo<T>(byte[] buffer, int index, T obj, //
            bool is_littleendian = true) //
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
                // TODO 
                fixed (byte* fix = (byte[])buffer.Clone())
                {
                    var p = fix + index;
                    if (is_littleendian ^ BitConverter.IsLittleEndian) {
                        Swap(p, type);
                    }
                    Marshal.PtrToStructure(new IntPtr(p), obj);
                }
            }
            return length;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T To<T>(byte[] buffer, int index, //
            bool is_littleendian = true) //
        {
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
                // TODO
                fixed (byte* fix = (byte[])buffer.Clone())
                {
                    var p = fix + index;
                    if (is_littleendian ^ BitConverter.IsLittleEndian) {
                        Swap(p, type);
                    }
                    return (T)Marshal.PtrToStructure(new IntPtr(p), type);
                }
            }
        }

    }
}
