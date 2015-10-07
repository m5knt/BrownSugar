using System;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar.Byte {

    public static class Sugar {
        /// <summary>
        /// 型のサイズを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int ByteSize<T>(this T t) {
            return Marshal.SizeOf(t);
        }

        public static long DoubleToInt64Bits(this double value) {
            return BitConverter.DoubleToInt64Bits(value);
        }

        public static double Int64BitsToDouble(this long value) {
            return BitConverter.Int64BitsToDouble(value);
        }

        //
        //
        //

        public static byte SwapByteOrder(this byte value) {
            return value;
        }

        public static ushort SwapByteOrder(this ushort value) {
            return unchecked((ushort)( //
                (value & (ushort)0xff00) >> 8 |
                (value & (ushort)0x00ff) << 8));
        }

        public static uint SwapByteOrder(this uint value) {
            return ( //
                (value & (uint)0xff000000) >> 24 |
                (value & (uint)0x00ff0000) >> 8 |
                (value & (uint)0x0000ff00) << 8 |
                (value & (uint)0x000000ff) << 24);
        }

        public static ulong SwapByteOrder(this ulong value) {
            return ( //
                (value & (ulong)0xff00000000000000) >> 56 |
                (value & (ulong)0x00ff000000000000) >> 40 |
                (value & (ulong)0x0000ff0000000000) >> 24 |
                (value & (ulong)0x000000ff00000000) >> 8 |
                (value & (ulong)0x00000000ff000000) << 8 |
                (value & (ulong)0x0000000000ff0000) << 24 |
                (value & (ulong)0x000000000000ff00) << 40 |
                (value & (ulong)0x00000000000000ff) << 56);
        }

        public static sbyte SwapByteOrder(this sbyte value) {
            return value;
        }

        public static short SwapByteOrder(this short value) {
            return unchecked((short)( //
                (unchecked((ushort)value) & (ushort)0xff00) >> 8 |
                (unchecked((ushort)value) & (ushort)0x00ff) << 8));
        }

        public static int SwapByteOrder(this int value) {
            return unchecked((int)( //
                (unchecked((uint)value) & (uint)0xff000000) >> 24 |
                (unchecked((uint)value) & (uint)0x00ff0000) >> 8 |
                (unchecked((uint)value) & (uint)0x0000ff00) << 8 |
                (unchecked((uint)value) & (uint)0x000000ff) << 24));
        }

        public static long SwapByteOrder(this long value) {
            return unchecked((long)( //
                (unchecked((ulong)value) & (ulong)0xff00000000000000) >> 56 |
                (unchecked((ulong)value) & (ulong)0x00ff000000000000) >> 40 |
                (unchecked((ulong)value) & (ulong)0x0000ff0000000000) >> 24 |
                (unchecked((ulong)value) & (ulong)0x000000ff00000000) >> 8 |
                (unchecked((ulong)value) & (ulong)0x00000000ff000000) << 8 |
                (unchecked((ulong)value) & (ulong)0x0000000000ff0000) << 24 |
                (unchecked((ulong)value) & (ulong)0x000000000000ff00) << 40 |
                (unchecked((ulong)value) & (ulong)0x00000000000000ff) << 56));
        }
    }

    /// <summary>
    /// リトルエンディアンバッファ操作
    /// </summary>
    public static class LittleEndian {

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        [Obsolete("Recommend buffer[N]=unchecked((byte)value);")]
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
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

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        [Obsolete("Recommend buffer[N]=unchecked((byte)value);")]
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
            buffer[index + 1] = unchecked((byte)(value >> 8));
            buffer[index + 2] = unchecked((byte)(value >> 16));
            buffer[index + 3] = unchecked((byte)(value >> 24));
        }

        /// <summary>
        /// リトルエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
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

        /*
         *
         */

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        [Obsolete("Recommend value=unchecked((sbyte)buffer[index]);")]
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static short ToInt16(byte[] buffer, int index) {
            return unchecked((short)(
                (ushort)buffer[index + 0] << 0 |
                (ushort)buffer[index + 1] << 8 |
                0));
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static int ToInt32(byte[] buffer, int index) {
            return unchecked((int)(
                (uint)buffer[index + 0] << 0 |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24 |
                0));
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static long ToInt64(byte[] buffer, int index) {
            return unchecked((long)(
                (ulong)buffer[index + 0] << 0 |
                (ulong)buffer[index + 1] << 8 |
                (ulong)buffer[index + 2] << 16 |
                (ulong)buffer[index + 3] << 24 |
                (ulong)buffer[index + 4] << 32 |
                (ulong)buffer[index + 5] << 40 |
                (ulong)buffer[index + 6] << 48 |
                (ulong)buffer[index + 7] << 56 |
                0));
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        [Obsolete("Recommend value=buffer[index];")]
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index + 0];
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static ushort ToUInt16(byte[] buffer, int index) {
            return unchecked((ushort)(
                (ushort)buffer[index + 0] << 0 |
                (ushort)buffer[index + 1] << 8 |
                0));
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static uint ToUInt32(byte[] buffer, int index) {
            return unchecked((uint)(
                (uint)buffer[index + 0] << 0 |
                (uint)buffer[index + 1] << 8 |
                (uint)buffer[index + 2] << 16 |
                (uint)buffer[index + 3] << 24 |
                0));
        }

        /// <summary>
        /// リトルエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return unchecked((ulong)(
                (ulong)buffer[index + 0] << 0 |
                (ulong)buffer[index + 1] << 8 |
                (ulong)buffer[index + 2] << 16 |
                (ulong)buffer[index + 3] << 24 |
                (ulong)buffer[index + 4] << 32 |
                (ulong)buffer[index + 5] << 40 |
                (ulong)buffer[index + 6] << 48 |
                (ulong)buffer[index + 7] << 56 |
                0));
        }
    }

    public static class BigEndian {

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        [Obsolete("Recommend buffer[N]=unchecked((byte)value);")]
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index + 0] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[index + 0] = unchecked((byte)(value >> 24));
            buffer[index + 1] = unchecked((byte)(value >> 16));
            buffer[index + 2] = unchecked((byte)(value >> 8));
            buffer[index + 3] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, long value) {
            buffer[index + 0] = unchecked((byte)(value >> 56));
            buffer[index + 1] = unchecked((byte)(value >> 48));
            buffer[index + 2] = unchecked((byte)(value >> 40));
            buffer[index + 3] = unchecked((byte)(value >> 32));
            buffer[index + 4] = unchecked((byte)(value >> 24));
            buffer[index + 5] = unchecked((byte)(value >> 16));
            buffer[index + 6] = unchecked((byte)(value >> 8));
            buffer[index + 7] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        [Obsolete("Recommend buffer[index]=value;")]
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index + 0] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index + 0] = unchecked((byte)(value >> 8));
            buffer[index + 1] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index + 0] = unchecked((byte)(value >> 24));
            buffer[index + 1] = unchecked((byte)(value >> 16));
            buffer[index + 2] = unchecked((byte)(value >> 8));
            buffer[index + 3] = unchecked((byte)(value >> 0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <param name="value">値</param>
        public static void Assign(byte[] buffer, int index, ulong value) {
            buffer[index + 0] = unchecked((byte)(value >> 56));
            buffer[index + 1] = unchecked((byte)(value >> 48));
            buffer[index + 2] = unchecked((byte)(value >> 40));
            buffer[index + 3] = unchecked((byte)(value >> 32));
            buffer[index + 4] = unchecked((byte)(value >> 24));
            buffer[index + 5] = unchecked((byte)(value >> 16));
            buffer[index + 6] = unchecked((byte)(value >> 8));
            buffer[index + 7] = unchecked((byte)(value >> 0));
        }

        //
        //
        //

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        [Obsolete("Recommend value=unchecked((sbyte)buffer[index]));")]
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index + 0]);
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static short ToInt16(byte[] buffer, int index) {
            return unchecked((short)(
                (ushort)buffer[index + 0] << 8 |
                (ushort)buffer[index + 1] << 0 |
                0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static int ToInt32(byte[] buffer, int index) {
            return unchecked((int)(
                (uint)buffer[index + 0] << 24 |
                (uint)buffer[index + 1] << 16 |
                (uint)buffer[index + 2] << 8 |
                (uint)buffer[index + 3] << 0 |
                0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static long ToInt64(byte[] buffer, int index) {
            return unchecked((long)(
                (ulong)buffer[index + 0] << 56 |
                (ulong)buffer[index + 1] << 48 |
                (ulong)buffer[index + 2] << 40 |
                (ulong)buffer[index + 3] << 32 |
                (ulong)buffer[index + 4] << 24 |
                (ulong)buffer[index + 5] << 16 |
                (ulong)buffer[index + 6] << 8 |
                (ulong)buffer[index + 7] << 0 |
                0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        [Obsolete("Recommend value=buffer[index];")]
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static ushort ToUInt16(byte[] buffer, int index) {
            return unchecked((ushort)(
                (ushort)buffer[index + 0] << 8 |
                (ushort)buffer[index + 1] << 0 |
                0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static uint ToUInt32(byte[] buffer, int index) {
            return unchecked((uint)(
                ((uint)buffer[index + 0] << 24) |
                ((uint)buffer[index + 1] << 16) |
                ((uint)buffer[index + 2] << 8) |
                ((uint)buffer[index + 3] << 0) |
                0));
        }

        /// <summary>
        /// ビッグエンディアンバッファ読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファインデックス</param>
        /// <returns>値</returns>
        public static ulong ToUInt64(byte[] buffer, int index) {
            return unchecked((ulong)(
                (ulong)buffer[index + 0] << 56 |
                (ulong)buffer[index + 1] << 48 |
                (ulong)buffer[index + 2] << 40 |
                (ulong)buffer[index + 3] << 32 |
                (ulong)buffer[index + 4] << 24 |
                (ulong)buffer[index + 5] << 16 |
                (ulong)buffer[index + 6] << 8 |
                (ulong)buffer[index + 7] << 0 |
                0));
        }
    }
}
