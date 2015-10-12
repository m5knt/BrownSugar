/**
 * @file
 * @brief リトルエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>
    /// リトルエンディアン順でバッファを操作します
    /// 配列版よりポインタ版の方が速度は速く
    /// もちろんBitConverter.GetBytesで何かするより速いです
    /// 1バイトアクセス系は遅いです
    /// </summary>
    public static class LittleEndian {

#if false
        /// <summary>ポインタを利用したバッファ操作を行うためのコールバック</summary>
        public unsafe delegate void CallBack(byte* pointer);

        /// <summary>連続してリトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Unsafe(byte* buffer, CallBack callback) {
            callback(buffer);
        }

        /// <summary>連続してリトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Unsafe(byte[] buffer, int index, CallBack callback) {
            fixed (byte* fix = buffer)
            {
                callback(fix + index);
            }
        }
#endif
        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            buffer[0] = value;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ushort value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
            buffer[2] = unchecked((byte)(value >> 16));
            buffer[3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ulong value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
            buffer[2] = unchecked((byte)(value >> 16));
            buffer[3] = unchecked((byte)(value >> 24));
            buffer[4] = unchecked((byte)(value >> 32));
            buffer[5] = unchecked((byte)(value >> 40));
            buffer[6] = unchecked((byte)(value >> 48));
            buffer[7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            buffer[0] = unchecked((byte)value);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, short value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
            buffer[2] = unchecked((byte)(value >> 16));
            buffer[3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
            buffer[2] = unchecked((byte)(value >> 16));
            buffer[3] = unchecked((byte)(value >> 24));
            buffer[4] = unchecked((byte)(value >> 32));
            buffer[5] = unchecked((byte)(value >> 40));
            buffer[6] = unchecked((byte)(value >> 48));
            buffer[7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            buffer[0] = value ? (byte)1 : (byte)0;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = unchecked((byte)value);
            buffer[1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            var n = BitConverter.DoubleToInt64Bits(value);
            buffer[0] = unchecked((byte)n);
            buffer[1] = unchecked((byte)(n >> 8));
            buffer[2] = unchecked((byte)(n >> 16));
            buffer[3] = unchecked((byte)(n >> 24));
            buffer[4] = unchecked((byte)(n >> 32));
            buffer[5] = unchecked((byte)(n >> 40));
            buffer[6] = unchecked((byte)(n >> 48));
            buffer[7] = unchecked((byte)(n >> 56));
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ulong value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
            buffer[index + 4] = unchecked((byte)(value >> 32));
            buffer[index + 5] = unchecked((byte)(value >> 40));
            buffer[index + 6] = unchecked((byte)(value >> 48));
            buffer[index + 7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, long value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
            buffer[index + 4] = unchecked((byte)(value >> 32));
            buffer[index + 5] = unchecked((byte)(value >> 40));
            buffer[index + 6] = unchecked((byte)(value >> 48));
            buffer[index + 7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, double value) {
            var tmp = BitConverter.DoubleToInt64Bits(value);
            buffer[index] = unchecked((byte)tmp);
            buffer[index + 1] = unchecked((byte)(tmp >> 8));
            buffer[index + 2] = unchecked((byte)(tmp >> 16));
            buffer[index + 3] = unchecked((byte)(tmp >> 24));
            buffer[index + 4] = unchecked((byte)(tmp >> 32));
            buffer[index + 5] = unchecked((byte)(tmp >> 40));
            buffer[index + 6] = unchecked((byte)(tmp >> 48));
            buffer[index + 7] = unchecked((byte)(tmp >> 56));
        }

        /*
         *
         */

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static ushort ToUInt16(byte[] buffer, int index) {
            return unchecked((ushort)(
                buffer[index] |
                buffer[index + 1] << 8
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] buffer, int index) {
            return (
                buffer[index] |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24
                );
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return ((ulong)(
                ((uint)buffer[index+7] << 8 | buffer[index+6]) << 16 |
                ((uint)buffer[index+5] << 8 | buffer[index+4])
                ) << 32) |
                (
                ((uint)buffer[index+3] << 8 | buffer[index+2]) << 16 |
                ((uint)buffer[index+1] << 8 | buffer[index+0])
                );
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static short ToInt16(byte[] buffer, int index) {
            return unchecked((short)(
                buffer[index] |
                buffer[index + 1] << 8
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] buffer, int index) {
            return unchecked((int)(
                buffer[index] |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24
                ));
        }

        public static long ToInt64(byte[] buffer, int index) {
            return (long)(
                (ulong)(
                (buffer[index + 6] | (uint)buffer[index + 7] << 8) << 16 |
                (buffer[index + 4] | (uint)buffer[index + 5] << 8)
                ) << 32) |
                (
                (buffer[index + 2] | (uint)buffer[index + 3] << 8) << 16 |
                (buffer[index + 0] | (uint)buffer[index + 1] << 8)
                );
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static bool ToBool(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static char ToChar(byte[] buffer, int index) {
            return unchecked((char)(
                buffer[index] |
                buffer[index + 1] << 8
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static double ToDouble(byte[] buffer, int index) {
            return BitConverter.Int64BitsToDouble(
                unchecked((long)(
                buffer[index] |
                (ulong)buffer[index + 1] << 8 |
                (ulong)buffer[index + 2] << 16 |
                (ulong)buffer[index + 3] << 24 |
                (ulong)buffer[index + 4] << 32 |
                (ulong)buffer[index + 5] << 40 |
                (ulong)buffer[index + 6] << 48 |
                (ulong)buffer[index + 7] << 56
                )));
        }
    }
}
