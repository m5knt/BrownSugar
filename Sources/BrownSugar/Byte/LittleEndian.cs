/**
 * @file
 * @brief リトルエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class LittleEndianSugar {
    }

    /// <summary>
    /// リトルエンディアン順でバッファを操作します
    /// </summary>
    public static class LittleEndian {

        public unsafe delegate byte* CallBack(byte* pointer);

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
        public static unsafe byte* Assign(byte* pointer, byte value) {
            *pointer++ = unchecked((byte)(value >> 0));
            return pointer;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, ushort value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            return pointer + 2;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, uint value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            return pointer + 4;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, ulong value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            pointer[4] = unchecked((byte)(value >> 32));
            pointer[5] = unchecked((byte)(value >> 40));
            pointer[6] = unchecked((byte)(value >> 48));
            pointer[7] = unchecked((byte)(value >> 56));
            return pointer + 8;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, sbyte value) {
            *pointer++ = unchecked((byte)(value >> 0));
            return pointer;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, short value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            return pointer + 2;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, int value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            return pointer + 4;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, long value) {
            pointer[0] = unchecked((byte)(value >> 0));
            pointer[1] = unchecked((byte)(value >> 8));
            pointer[2] = unchecked((byte)(value >> 16));
            pointer[3] = unchecked((byte)(value >> 24));
            pointer[4] = unchecked((byte)(value >> 32));
            pointer[5] = unchecked((byte)(value >> 40));
            pointer[6] = unchecked((byte)(value >> 48));
            pointer[7] = unchecked((byte)(value >> 56));
            return pointer + 8;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe byte* Assign(byte* pointer, double value) {
            var n = BitConverter.DoubleToInt64Bits(value);
            return Assign(pointer, n);
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        [Obsolete("速度がほしいときは右記がお勧め : buffer[index]=unchecked((byte)value);")]
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ulong value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
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
            buffer[index + 0] = unchecked((byte)value);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, long value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
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
            buffer[index + 0] = unchecked((byte)(tmp >> 0));
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
            return buffer[index + 0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static ushort ToUInt16(byte[] buffer, int index) {
            return unchecked((ushort)(
                buffer[index + 0] << 0 |
                buffer[index + 1] << 8
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] buffer, int index) {
            return unchecked((uint)(
                (uint)buffer[index + 0] << 0 |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return unchecked((ulong)(
                (ulong)buffer[index + 0] << 0 |
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
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static short ToInt16(byte[] buffer, int index) {
            return unchecked((short)(
                buffer[index + 0] << 0 |
                buffer[index + 1] << 8
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] buffer, int index) {
            return unchecked((int)(
                (uint)buffer[index + 0] << 0 |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24
                ));
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static long ToInt64(byte[] buffer, int index) {
            return unchecked((long)(
                (ulong)buffer[index + 0] << 0 |
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
            return BitConverter.Int64BitsToDouble(unchecked((long)(
                (ulong)buffer[index + 0] << 0 |
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
