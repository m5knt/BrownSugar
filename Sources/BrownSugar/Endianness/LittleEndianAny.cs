﻿/**
 * @file
 * @brief リトルエンディアン順のバッファ操作関係
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>リトルエンディアン順のバッファ操作関係</summary>
    public partial class LittleEndianAny : NoOrder {

        /// <summary>リトルエンディアンであるか</summary>
        static bool IsLittleEndian = true;

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe ushort ToUInt16(byte* b) {
            return unchecked((ushort)(b[1] << 8 | b[0])); // 11
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe short ToInt16(byte* b) {
            return unchecked((short)(b[1] << 8 | b[0]));
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe char ToChar(byte* b) {
            return unchecked((char)(b[1] << 8 | b[0]));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static ushort ToUInt16(byte[] b, int i) {
            return unchecked((ushort)(b[i + 1] << 8 | b[i])); // 13
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static short ToInt16(byte[] b, int i) {
            return unchecked((short)(b[i + 1] << 8 | b[i]));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static char ToChar(byte[] b, int i) {
            return unchecked((char)(b[i + 1] << 8 | b[i]));
        }

        //
        //
        //

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe uint ToUInt32(byte* b) {
            return (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] b, int i) {
            return (uint)(b[i + 3] << 24 | b[i + 2] << 16 | b[i + 1] << 8 | b[i]);
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* b) {
            return b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] b, int i) {
            return b[i + 3] << 24 | b[i + 2] << 16 | b[i + 1] << 8 | b[i];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* b) {
            var tmp = b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0];
            return *(float*)&tmp;
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static float ToSingle(byte[] b, int i) {
            unsafe
            {
                var tmp = b[i + 3] << 24 | b[i + 2] << 16 | b[i + 1] << 8 | b[i];
                return *(float*)&tmp;
            }
        }

        //
        //
        //

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* b) {
            if (((int)b & 7) == 0 && BitConverter.IsLittleEndian) {
                return *(ulong*)b;
            }
            return
                (ulong)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                 (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
        }

        public static unsafe long ToInt64(byte* b) {
            if (((int)b & 7) == 0 && BitConverter.IsLittleEndian) {
                return *(long*)b;
            }
            return
                (long)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);

        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if (((int)b & 7) == 0 && BitConverter.IsLittleEndian) {
                    return *(ulong*)b;
                }
                return
                    (ulong)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                     (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            }
        }

        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if ((index & 7) == 0 && BitConverter.IsLittleEndian) {
                    return *(long*)b;
                }
                return
                    (long)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                    (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            }
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* b) {
            if (((int)b & 7) == 0 && BitConverter.IsLittleEndian) {
                return *(double*)b;
            }
            var tmp =
                (ulong)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                 (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            return *(double*)&tmp;
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if ((index & 7) == 0 && BitConverter.IsLittleEndian) {
                    return *(double*)b;
                }
                var tmp =
                    (ulong)(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]) << 32 |
                     (uint)(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
                return *(double*)&tmp;
            }
        }
        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ushort value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, short value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, char value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
            buffer[index + 2] = (byte)(value >> 16);
            buffer[index + 3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
            buffer[index + 2] = (byte)(value >> 16);
            buffer[index + 3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            var tmp = *(uint*)&value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, float value) {
            var tmp = *(uint*)&value;
            buffer[index] = (byte)tmp;
            buffer[index + 1] = (byte)(tmp >> 8);
            buffer[index + 2] = (byte)(tmp >> 16);
            buffer[index + 3] = (byte)(tmp >> 24);
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ulong value) {
            if (((int)buffer & 7) == 0 && BitConverter.IsLittleEndian) {
                *(ulong*)(buffer) = value;
                return;
            }
            var tmp = (uint)value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
            tmp = (uint)(value >> 32);
            buffer[4] = (byte)(value);
            buffer[5] = (byte)(value >> 8);
            buffer[6] = (byte)(value >> 16);
            buffer[7] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            if (((int)buffer & 7) == 0 && BitConverter.IsLittleEndian) {
                *(long*)(buffer) = value;
                return;
            }
            var tmp = (uint)value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
            tmp = (uint)(value >> 32);
            buffer[4] = (byte)(value);
            buffer[5] = (byte)(value >> 8);
            buffer[6] = (byte)(value >> 16);
            buffer[7] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, long value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (((int)index & 7) == 0 && BitConverter.IsLittleEndian) {
                    *(long*)pointer = value;
                    return;
                }
                else {
                    var tmp = (uint)value;
                    pointer[0] = (byte)tmp;
                    pointer[1] = (byte)(tmp >> 8);
                    pointer[2] = (byte)(tmp >> 16);
                    pointer[3] = (byte)(tmp >> 24);
                    tmp = (uint)(value >> 32);
                    pointer[4] = (byte)(tmp);
                    pointer[5] = (byte)(tmp >> 8);
                    pointer[6] = (byte)(tmp >> 16);
                    pointer[7] = (byte)(tmp >> 24);
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, ulong value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (((int)index & 7) == 0 && BitConverter.IsLittleEndian) {
                    *(ulong*)pointer = value;
                    return;
                }
                else {
                    var tmp = (uint)value;
                    pointer[0] = (byte)tmp;
                    pointer[1] = (byte)(tmp >> 8);
                    pointer[2] = (byte)(tmp >> 16);
                    pointer[3] = (byte)(tmp >> 24);
                    tmp = (uint)(value >> 32);
                    pointer[4] = (byte)(tmp);
                    pointer[5] = (byte)(tmp >> 8);
                    pointer[6] = (byte)(tmp >> 16);
                    pointer[7] = (byte)(tmp >> 24);
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            if (((int)buffer & 7) == 0 && BitConverter.IsLittleEndian) {
                *(double*)(buffer) = value;
                return;
            }
            var val = *(ulong*)&value;
            var tmp = (uint)val;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
            tmp = (uint)(val >> 32);
            buffer[4] = (byte)(tmp);
            buffer[5] = (byte)(tmp >> 8);
            buffer[6] = (byte)(tmp >> 16);
            buffer[7] = (byte)(tmp >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, double value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (((int)index & 7) == 0 && BitConverter.IsLittleEndian) {
                    *(double*)pointer = value;
                    return;
                }
                else {
                    var tmp = (uint)((ulong*)&value)[0];
                    pointer[0] = (byte)tmp;
                    pointer[1] = (byte)(tmp >> 8);
                    pointer[2] = (byte)(tmp >> 16);
                    pointer[3] = (byte)(tmp >> 24);
                    tmp = (uint)(((ulong*)&value)[0] >> 32);
                    pointer[4] = (byte)(tmp);
                    pointer[5] = (byte)(tmp >> 8);
                    pointer[6] = (byte)(tmp >> 16);
                    pointer[7] = (byte)(tmp >> 24);
                }
            }
        }
    }
}

//
//
//

namespace ThunderEgg.BrownSugar {
    using System.Runtime.InteropServices;

    /// <summary>エンディアンを知っている型</summary>
    using EndianHolder = LittleEndianAny;

    public partial class LittleEndianAny {

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
        }

        //
        //
        //

        /// <summary>オブジェクトをバイナリ化しバッファへ書き込む</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int MarshalAssign<T>(byte[] buffer, int index, T obj) //
        {
            return ByteOrder.MarshalAssign<T>(buffer, index, obj, //
                EndianHolder.IsLittleEndian);
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] MarshalGetBytes<T>(T obj) {
            return ByteOrder.MarshalGetBytes<T>(obj, EndianHolder.IsLittleEndian);
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int MarshalCopyTo<T>(byte[] buffer, int index, T obj) //
            where T : class //
        {
            return ByteOrder.MarshalCopyTo<T>(buffer, index, obj, //
                EndianHolder.IsLittleEndian);
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T MarshalTo<T>(byte[] buffer, int index) {
            return ByteOrder.MarshalTo<T>(buffer, index, //
                EndianHolder.IsLittleEndian);
        }
    }
}
