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
    /// </summary>
    public static class LittleEndian {

        /// <summary>ポインタを利用したバッファ操作を行うためのコールバック</summary>
        public unsafe delegate void CallBack(byte* pointer);

        /// <summary>連続してリトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, CallBack callback) {
            callback(buffer);
        }

        /// <summary>連続してリトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, CallBack callback) {
            fixed (byte* fix = buffer)
            {
                callback(fix + index);
            }
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, byte value) {
            pointer[0] = value;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, ushort value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, uint value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, ulong value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            pointer[4] = unchecked((byte)(value >> 32));
            pointer[5] = unchecked((byte)(value >> 40));
            pointer[6] = unchecked((byte)(value >> 48));
            pointer[7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, sbyte value) {
            pointer[0] = unchecked((byte)value);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, short value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, int value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, long value) {
            pointer[0] = unchecked((byte)value);
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            pointer[4] = unchecked((byte)(value >> 32));
            pointer[5] = unchecked((byte)(value >> 40));
            pointer[6] = unchecked((byte)(value >> 48));
            pointer[7] = unchecked((byte)(value >> 56));
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* pointer, double value) {
            var t = BitConverter.DoubleToInt64Bits(value);
            pointer[0] = unchecked((byte)t);
            pointer[1] = unchecked((byte)(t >> 8));
            pointer[2] = unchecked((byte)(t >> 16));
            pointer[3] = unchecked((byte)(t >> 24));
            pointer[4] = unchecked((byte)(t >> 32));
            pointer[5] = unchecked((byte)(t >> 40));
            pointer[6] = unchecked((byte)(t >> 48));
            pointer[7] = unchecked((byte)(t >> 56));
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
            return (
                buffer[index] |
                (ulong)buffer[index + 1] << 8 |
                (ulong)buffer[index + 2] << 16 |
                (ulong)buffer[index + 3] << 24 |
                (ulong)buffer[index + 4] << 32 |
                (ulong)buffer[index + 5] << 40 |
                (ulong)buffer[index + 6] << 48 |
                (ulong)buffer[index + 7] << 56
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

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static long ToInt64(byte[] buffer, int index) {
            return unchecked((long)(
                buffer[index] |
                (ulong)buffer[index + 1] << 8 |
                (ulong)buffer[index + 2] << 16 |
                (ulong)buffer[index + 3] << 24 |
                (ulong)buffer[index + 4] << 32 |
                (ulong)buffer[index + 5] << 40 |
                (ulong)buffer[index + 6] << 48 |
                (ulong)buffer[index + 7] << 56
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
