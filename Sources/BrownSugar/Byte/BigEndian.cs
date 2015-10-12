/**
 * @file
 * @brief ビッグエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>ネットワークバイトオーダーでバッファ操作をします</summary>
    public class NetOrder : BigEndian {
    }

    /// <summary>ビッグエンディアン順でバッファ操作をします</summary>
    public class BigEndian {

        /// <summary>ポインタを利用したバッファ操作を行うためのコールバック</summary>
        public unsafe delegate void CallBack(byte* pointer);

        /// <summary>連続してビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, CallBack callback) {
            callback(buffer);
        }

        /// <summary>連続してビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, CallBack callback) {
            fixed (byte* fix = buffer)
            {
                callback(fix + index);
            }
        }

        //
        //
        //

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, byte value) {
            pointer[0] = value;
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, ushort value) {
            pointer[0] = unchecked((byte)(value >> 8));
            pointer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, uint value) {
            pointer[0] = unchecked((byte)(value >> 24));
            pointer[1] = unchecked((byte)(value >> 16));
            pointer[2] = unchecked((byte)(value >> 8));
            pointer[3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, ulong value) {
            pointer[0] = unchecked((byte)(value >> 56));
            pointer[1] = unchecked((byte)(value >> 48));
            pointer[2] = unchecked((byte)(value >> 40));
            pointer[3] = unchecked((byte)(value >> 32));
            pointer[4] = unchecked((byte)(value >> 24));
            pointer[5] = unchecked((byte)(value >> 16));
            pointer[6] = unchecked((byte)(value >> 8));
            pointer[7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, sbyte value) {
            pointer[0] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, short value) {
            pointer[0] = unchecked((byte)(value >> 8));
            pointer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, int value) {
            pointer[0] = unchecked((byte)(value >> 24));
            pointer[1] = unchecked((byte)(value >> 16));
            pointer[2] = unchecked((byte)(value >> 8));
            pointer[3] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, long value) {
            pointer[0] = unchecked((byte)(value >> 56));
            pointer[1] = unchecked((byte)(value >> 48));
            pointer[2] = unchecked((byte)(value >> 40));
            pointer[3] = unchecked((byte)(value >> 32));
            pointer[4] = unchecked((byte)(value >> 24));
            pointer[5] = unchecked((byte)(value >> 16));
            pointer[6] = unchecked((byte)(value >> 8));
            pointer[7] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, double value) {
            Assign(pointer, BitConverter.DoubleToInt64Bits(value));
        }

        //
        //
        //

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
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
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index] = unchecked((byte)(value >> 24));
            buffer[index + 1] = unchecked((byte)(value >> 16));
            buffer[index + 2] = unchecked((byte)(value >> 8));
            buffer[index + 3] = unchecked((byte)value);
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

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static ushort ToUInt16(byte[] buffer, int index) {
            return unchecked((ushort)(
                buffer[index + 0] << 8 |
                buffer[index + 1]
                ));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] buffer, int index) {
            return (
                (uint)buffer[index + 0] << 24 |
                (uint)buffer[index + 1] << 16 |
                (uint)buffer[index + 2] << 8 |
                buffer[index + 3]
                );
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return (
                (ulong)buffer[index + 0] << 56 |
                (ulong)buffer[index + 1] << 48 |
                (ulong)buffer[index + 2] << 40 |
                (ulong)buffer[index + 3] << 32 |
                (ulong)buffer[index + 4] << 24 |
                (ulong)buffer[index + 5] << 16 |
                (ulong)buffer[index + 6] << 8 |
                buffer[index + 7]
                );
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static short ToInt16(byte[] buffer, int index) {
            return unchecked((short)(
                buffer[index] << 8 |
                buffer[index + 1]
                ));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] buffer, int index) {
            return unchecked((int)(
                (uint)buffer[index] << 24 |
                (uint)buffer[index + 1] << 16 |
                (uint)buffer[index + 2] << 8 |
                buffer[index + 3]
                ));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static long ToInt64(byte[] buffer, int index) {
            return unchecked((long)(
                (ulong)buffer[index] << 56 |
                (ulong)buffer[index + 1] << 48 |
                (ulong)buffer[index + 2] << 40 |
                (ulong)buffer[index + 3] << 32 |
                (ulong)buffer[index + 4] << 24 |
                (ulong)buffer[index + 5] << 16 |
                (ulong)buffer[index + 6] << 8 |
                buffer[index + 7]
                ));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static double ToDouble(byte[] buffer, int index) {
            return BitConverter.Int64BitsToDouble(
                unchecked((long)(
                (ulong)buffer[index] << 56 |
                (ulong)buffer[index + 1] << 48 |
                (ulong)buffer[index + 2] << 40 |
                (ulong)buffer[index + 3] << 32 |
                (ulong)buffer[index + 4] << 24 |
                (ulong)buffer[index + 5] << 16 |
                (ulong)buffer[index + 6] << 8 |
                buffer[index + 7]
                )));
        }
    }
}
