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

    /// <summary>
    /// ビッグエンディアン順でバッファ操作をします
    /// 配列版よりポインタ版の方が速度は速く
    /// もちろんBitConverter.GetBytesで何かするより速いです
    /// 1バイトアクセス系は遅いです
    /// </summary>
    public class BigEndian {

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            buffer[0] = value;
        }

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
        public static unsafe void Assign(byte* buffer, sbyte value) {
            buffer[0] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, short value) {
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
        public static unsafe void Assign(byte* buffer, bool value) {
            buffer[0] = value ? (byte)1 : (byte)0;
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = unchecked((byte)(value >> 8));
            buffer[1] = unchecked((byte)value);
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
                buffer[index] << 8 |
                buffer[index + 1]
                ));
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] buffer, int index) {
            return (
                (uint)buffer[index] << 24 |
                (uint)buffer[index + 1] << 16 |
                (uint)buffer[index + 2] << 8 |
                buffer[index + 3]
                );
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return (
                (ulong)buffer[index] << 56 |
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
        public static bool ToBool(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static char ToChar(byte[] buffer, int index) {
            return unchecked((char)(
                buffer[index] << 8 |
                buffer[index + 1]
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
