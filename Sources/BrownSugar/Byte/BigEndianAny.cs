/**
 * @file
 * @brief ビッグエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar.Byte {

    /// <summary>バッファをビッグエンディアン順で操作します</summary>
    public class BigEndianAny {

        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe byte ToUInt8(byte* b) {
            return *b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe sbyte ToInt8(byte* b) {
            return *(sbyte*)b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe bool ToBoolean(byte* b) {
            return *(bool*)b;
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        /// <summary>byte/遅いです</summary>
        public static byte ToUInt8(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(ArraySegment<byte> buffer, int index) {
            return unchecked((sbyte)buffer.Array[buffer.Offset + index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index] != 0;
        }

        //
        //
        //

        /// <summary>ポインタ位置に値を書/遅いですく</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, byte value) {
            buffer.Array[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, sbyte value) {
            buffer.Array[buffer.Offset + index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, bool value) {
            buffer.Array[buffer.Offset + index] = value ? (byte)1 : (byte)0;
        }

        //
        //
        //

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
        public static unsafe uint ToUInt32(byte* p) {
            return ((((uint)p[0] << 8 | p[1]) << 8) | p[2]) << 8 | p[3];
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* p) {
            // il 24 bytes
            return ((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe int ToInt32(byte[] p, int i) {
            return ((p[i] << 8 | p[i + 1]) << 8 | p[i + 2]) << 8 | p[i + 3];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] p, int i) {
            return ((((uint)p[i] << 8 | p[i + 1]) << 8) | p[i + 2]) << 8 | p[i + 3];
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* p) {
            if (!BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(float*)p;
            }
            var tmp = ((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3];
            return *(float*)&tmp;
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            fixed(byte* fix = buffer)
            {
                var p = fix + index;
                if (!BitConverter.IsLittleEndian && (index & 7) == 0) {
                    return *(float*)p;
                }
                var tmp = ((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3];
                return *(float*)&tmp;
            }
        }
    
        //
        //
        //

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* p) {
            if (!BitConverter.IsLittleEndian && ((long)p & 7) == 0) {
                return *(ulong*)p;
            }
            // il 55 bytes
            return (ulong)
                ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe long ToInt64(byte* p) {
            if (!BitConverter.IsLittleEndian && ((long)p & 7) == 0) {
                return *(long*)p;
            }
            return unchecked((long)
                ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                    return *(long*)p;
                }
                // il 52 bytes
                return unchecked((long)
                    ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                    ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]));
            }
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                    return *(ulong*)p;
                }
                // il 55 bytes
                return (ulong)
                    ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                    ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
            }
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* p) {
            if (!BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(double*)p;
            }
            var tmp = 
                (long)(((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                (uint)(((p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
            return ((double*)&tmp)[0];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed(byte* fix = buffer)
            {
                var p = fix + index;
                if (!BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                    return *(double*)p;
                }
                var tmp =
                    (long)(((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                    (uint)(((p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
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
            buffer[0] = unchecked((byte)(value >> 56));
            buffer[1] = unchecked((byte)(value >> 48));
            buffer[2] = unchecked((byte)(value >> 40));
            buffer[3] = unchecked((byte)(value >> 32));
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
            buffer[0] = unchecked((byte)(value >> 56));
            buffer[1] = unchecked((byte)(value >> 48));
            buffer[2] = unchecked((byte)(value >> 40));
            buffer[3] = unchecked((byte)(value >> 32));
            buffer[4] = unchecked((byte)(value >> 24));
            buffer[5] = unchecked((byte)(value >> 16));
            buffer[6] = unchecked((byte)(value >> 8));
            buffer[7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            var n = BitConverter.DoubleToInt64Bits(value);
            buffer[0] = unchecked((byte)(n >> 56));
            buffer[1] = unchecked((byte)(n >> 48));
            buffer[2] = unchecked((byte)(n >> 40));
            buffer[3] = unchecked((byte)(n >> 32));
            buffer[4] = unchecked((byte)(n >> 24));
            buffer[5] = unchecked((byte)(n >> 16));
            buffer[6] = unchecked((byte)(n >> 8));
            buffer[7] = unchecked((byte)n);
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
            buffer[index] = unchecked((byte)(value >> 56));
            buffer[index + 1] = unchecked((byte)(value >> 48));
            buffer[index + 2] = unchecked((byte)(value >> 40));
            buffer[index + 3] = unchecked((byte)(value >> 32));
            buffer[index + 4] = unchecked((byte)(value >> 24));
            buffer[index + 5] = unchecked((byte)(value >> 16));
            buffer[index + 6] = unchecked((byte)(value >> 8));
            buffer[index + 7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, long value) {
            buffer[index] = unchecked((byte)(value >> 56));
            buffer[index + 1] = unchecked((byte)(value >> 48));
            buffer[index + 2] = unchecked((byte)(value >> 40));
            buffer[index + 3] = unchecked((byte)(value >> 32));
            buffer[index + 4] = unchecked((byte)(value >> 24));
            buffer[index + 5] = unchecked((byte)(value >> 16));
            buffer[index + 6] = unchecked((byte)(value >> 8));
            buffer[index + 7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, double value) {
            var n = BitConverter.DoubleToInt64Bits(value);
            buffer[index] = unchecked((byte)(n >> 56));
            buffer[index + 1] = unchecked((byte)(n >> 48));
            buffer[index + 2] = unchecked((byte)(n >> 40));
            buffer[index + 3] = unchecked((byte)(n >> 32));
            buffer[index + 4] = unchecked((byte)(n >> 24));
            buffer[index + 5] = unchecked((byte)(n >> 16));
            buffer[index + 6] = unchecked((byte)(n >> 8));
            buffer[index + 7] = unchecked((byte)n);
        }

        //
        //
        //
    }
}
