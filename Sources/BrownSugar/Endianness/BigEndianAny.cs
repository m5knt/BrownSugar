/**
 * @file
 * @brief ビッグエンディアン順のバッファ操作関係
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>ビッグエンディアン順のバッファ操作関係</summary>
    public partial class BigEndianAny : NoOrder {

        /// <summary>リトルエンディアンであるか</summary>
        static bool IsLittleEndian = false;

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe ushort ToUInt16(byte* b) {
            return unchecked((ushort)(b[0] << 8 | b[1])); // 11
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe short ToInt16(byte* b) {
            return unchecked((short)(b[0] << 8 | b[1]));
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe char ToChar(byte* b) {
            return unchecked((char)(b[0] << 8 | b[1]));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static ushort ToUInt16(byte[] b, int i) {
            return unchecked((ushort)(b[i] << 8 | b[i + 1])); // 13
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static short ToInt16(byte[] b, int i) {
            return unchecked((short)(b[i] << 8 | b[i + 1]));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static char ToChar(byte[] b, int i) {
            return unchecked((char)(b[i] << 8 | b[i + 1]));
        }

        //
        //
        //

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe uint ToUInt32(byte* b) {
            return (uint)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]);
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* b) {
            return b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] b, int i) {
            return (uint)(b[i] << 24 | b[i + 1] << 16 | b[i + 2] << 8 | b[i + 3]);
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] b, int i) {
            return b[i] << 24 | b[i + 1] << 16 | b[i + 2] << 8 | b[i + 3];
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* b) {
            if (!BitConverter.IsLittleEndian && ((int)b & 3) == 0) {
                return *(float*)b;
            }
            var tmp = b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3];
            return *(float*)&tmp;
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if (!BitConverter.IsLittleEndian && (index & 3) == 0) {
                    return *(float*)b;
                }
                var tmp = b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3];
                return *(float*)&tmp;
            }
        }

        //
        //
        //

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* b) {
            if (!BitConverter.IsLittleEndian && ((long)b & 7) == 0) {
                return *(ulong*)b;
            }
            return
                (ulong)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                 (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]);
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe long ToInt64(byte* b) {
            if (!BitConverter.IsLittleEndian && ((long)b & 7) == 0) {
                return *(long*)b;
            }
            return unchecked(
                (long)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)b & 7) == 0) {
                    return *(long*)b;
                }
                return unchecked(
                    (long)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                    (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]));
            }
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)b & 7) == 0) {
                    return *(ulong*)b;
                }
                return
                    (ulong)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                     (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]);
            }
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* b) {
            if (!BitConverter.IsLittleEndian && ((int)b & 7) == 0) {
                return *(double*)b;
            }
            var tmp =
                (ulong)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                 (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]);
            return ((double*)&tmp)[0];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var b = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)b & 7) == 0) {
                    return *(double*)b;
                }
                var tmp =
                    (ulong)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]) << 32 |
                     (uint)(b[4] << 24 | b[5] << 16 | b[6] << 8 | b[7]);
                return ((double*)&tmp)[0];
            }
        }
        //
        //
        //

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ushort value) {
            buffer[0] = unchecked((byte)(value >> 8));
            buffer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            buffer[0] = unchecked((byte)(value >> 24));
            buffer[1] = unchecked((byte)(value >> 16));
            buffer[2] = unchecked((byte)(value >> 8));
            buffer[3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ulong value) {
            var tmp = (uint)(value >> 32);
            buffer[0] = unchecked((byte)(value >> 24));
            buffer[1] = unchecked((byte)(value >> 16));
            buffer[2] = unchecked((byte)(value >> 8));
            buffer[3] = unchecked((byte)value);
            tmp = (uint)value;
            buffer[4] = unchecked((byte)(value >> 24));
            buffer[5] = unchecked((byte)(value >> 16));
            buffer[6] = unchecked((byte)(value >> 8));
            buffer[7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, short value) {
            buffer[0] = unchecked((byte)(value >> 8));
            buffer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = unchecked((byte)(value >> 8));
            buffer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            buffer[0] = unchecked((byte)(value >> 24));
            buffer[1] = unchecked((byte)(value >> 16));
            buffer[2] = unchecked((byte)(value >> 8));
            buffer[3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            var tmp = *(uint*)&value;
            buffer[0] = unchecked((byte)(tmp >> 24));
            buffer[1] = unchecked((byte)(tmp >> 16));
            buffer[2] = unchecked((byte)(tmp >> 8));
            buffer[3] = unchecked((byte)tmp);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            var tmp = (uint)(value >> 32);
            buffer[0] = unchecked((byte)(value >> 24));
            buffer[1] = unchecked((byte)(value >> 16));
            buffer[2] = unchecked((byte)(value >> 8));
            buffer[3] = unchecked((byte)value);
            tmp = (uint)value;
            buffer[4] = unchecked((byte)(value >> 24));
            buffer[5] = unchecked((byte)(value >> 16));
            buffer[6] = unchecked((byte)(value >> 8));
            buffer[7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            var val = *(ulong*)&value;
            var tmp = (uint)(val >> 32);
            buffer[0] = unchecked((byte)(tmp >> 24));
            buffer[1] = unchecked((byte)(tmp >> 16));
            buffer[2] = unchecked((byte)(tmp >> 8));
            buffer[3] = unchecked((byte)tmp);
            tmp = (uint)val;
            buffer[4] = unchecked((byte)(tmp >> 24));
            buffer[5] = unchecked((byte)(tmp >> 16));
            buffer[6] = unchecked((byte)(tmp >> 8));
            buffer[7] = unchecked((byte)tmp);
        }

        //
        //
        //

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, char value) {
            buffer[index] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index] = unchecked((byte)(value >> 24));
            buffer[index + 1] = unchecked((byte)(value >> 16));
            buffer[index + 2] = unchecked((byte)(value >> 8));
            buffer[index + 3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index] = unchecked((byte)(value >> 24));
            buffer[index + 1] = unchecked((byte)(value >> 16));
            buffer[index + 2] = unchecked((byte)(value >> 8));
            buffer[index + 3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, float value) {
            unsafe
            {
                var tmp = *(uint*)&value;
                buffer[index] = unchecked((byte)(tmp >> 24));
                buffer[index + 1] = unchecked((byte)(tmp >> 16));
                buffer[index + 2] = unchecked((byte)(tmp >> 8));
                buffer[index + 3] = unchecked((byte)(tmp));
            }
        }
        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ulong value) {
            var tmp = (uint)(value >> 32);
            buffer[index] = unchecked((byte)(tmp >> 24));
            buffer[index + 1] = unchecked((byte)(tmp >> 16));
            buffer[index + 2] = unchecked((byte)(tmp >> 8));
            buffer[index + 3] = unchecked((byte)tmp);
            tmp = (uint)value;
            buffer[index + 4] = unchecked((byte)(tmp >> 24));
            buffer[index + 5] = unchecked((byte)(tmp >> 16));
            buffer[index + 6] = unchecked((byte)(tmp >> 8));
            buffer[index + 7] = unchecked((byte)tmp);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, long value) {
            var tmp = (uint)(value >> 32);
            buffer[index] = unchecked((byte)(tmp >> 24));
            buffer[index + 1] = unchecked((byte)(tmp >> 16));
            buffer[index + 2] = unchecked((byte)(tmp >> 8));
            buffer[index + 3] = unchecked((byte)tmp);
            tmp = (uint)value;
            buffer[index + 4] = unchecked((byte)(tmp >> 24));
            buffer[index + 5] = unchecked((byte)(tmp >> 16));
            buffer[index + 6] = unchecked((byte)(tmp >> 8));
            buffer[index + 7] = unchecked((byte)tmp);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, double value) {
            unsafe
            {
                var val = *(ulong*)&value;
                var tmp = (uint)(val >> 32);
                buffer[index] = unchecked((byte)(tmp >> 24));
                buffer[index + 1] = unchecked((byte)(tmp >> 16));
                buffer[index + 2] = unchecked((byte)(tmp >> 8));
                buffer[index + 3] = unchecked((byte)tmp);
                tmp = (uint)val;
                buffer[index + 4] = unchecked((byte)(tmp >> 24));
                buffer[index + 5] = unchecked((byte)(tmp >> 16));
                buffer[index + 6] = unchecked((byte)(tmp >> 8));
                buffer[index + 7] = unchecked((byte)tmp);
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
    using EndianHolder = BigEndianAny;

    public partial class BigEndianAny {

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
