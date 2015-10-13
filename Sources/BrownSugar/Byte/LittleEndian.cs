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
    /// </summary>
    public static class LittleEndian {

		/// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            buffer[0] = value;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ushort value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ulong value) {
            if ((((int)buffer) & 7) == 0) {
                *(ulong*)(buffer) = BitConverter.IsLittleEndian ?
                    value : value.SwapByteOrder();
                return;
            }
            byte* v = (byte*)&value;
            buffer[0] = v[0];
            buffer[1] = v[1];
            buffer[2] = v[2];
            buffer[3] = v[3];
            buffer[4] = v[4];
            buffer[5] = v[5];
            buffer[6] = v[6];
            buffer[7] = v[7];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            buffer[0] = (byte)value;
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, short value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            if ((((int)buffer) & 7) == 0) {
                *(long*)(buffer) = BitConverter.IsLittleEndian ?
                    value : value.SwapByteOrder();
                return;
            }
            byte* v = (byte*)&value;
            buffer[0] = v[0];
            buffer[1] = v[1];
            buffer[2] = v[2];
            buffer[3] = v[3];
            buffer[4] = v[4];
            buffer[5] = v[5];
            buffer[6] = v[6];
            buffer[7] = v[7];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            buffer[0] = ((byte*)&value)[0];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            byte* v = (byte*)&value;
            buffer[0] = v[0];
            buffer[1] = v[1];
            buffer[2] = v[2];
            buffer[3] = v[3];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            if ((((int)buffer) & 7) == 0) {
                *(long*)(buffer) = BitConverter.IsLittleEndian ?
                    ((long*)&value)[0] : ((long*)&value)[0].SwapByteOrder();
                return;
            }
            byte* v = (byte*)&value;
            buffer[0] = v[0];
            buffer[1] = v[1];
            buffer[2] = v[2];
            buffer[3] = v[3];
            buffer[4] = v[4];
            buffer[5] = v[5];
            buffer[6] = v[6];
            buffer[7] = v[7];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, decimal value) {
            if ((((int)buffer) & 7) == 0) {
                if (BitConverter.IsLittleEndian) {
                    ((ulong*)buffer)[0] = ((ulong*)&value)[0];
                    ((ulong*)buffer)[1] = ((ulong*)&value)[1];
                    return;
                }
                else {
                    ((ulong*)buffer)[0] = ((ulong*)&value)[1].SwapByteOrder();
                    ((ulong*)buffer)[1] = ((ulong*)&value)[0].SwapByteOrder();
                    return;
                }
            }
            byte* v = (byte*)&value;
            buffer[0] = v[0];
            buffer[1] = v[1];
            buffer[2] = v[2];
            buffer[3] = v[3];
            buffer[4] = v[4];
            buffer[5] = v[5];
            buffer[6] = v[6];
            buffer[7] = v[7];
            buffer[8] = v[8];
            buffer[9] = v[9];
            buffer[10] = v[10];
            buffer[11] = v[11];
            buffer[12] = v[12];
            buffer[13] = v[13];
            buffer[14] = v[14];
            buffer[15] = v[15];
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
        public static unsafe void Assign(byte[] buffer, int index, ulong value) {
            fixed (byte* pointer = &buffer[index])
            {
                if ((((int)pointer) & 7) == 0) {
                    *(ulong*)(pointer) = BitConverter.IsLittleEndian ?
                        value : value.SwapByteOrder();
                    return;
                }
                byte* v = (byte*)&value;
                pointer[0] = v[0];
                pointer[1] = v[1];
                pointer[2] = v[2];
                pointer[3] = v[3];
                pointer[4] = v[4];
                pointer[5] = v[5];
                pointer[6] = v[6];
                pointer[7] = v[7];
            }
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
            unsafe
            {
                fixed (byte* p = &buffer[index]) Assign(p, value);
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, long value) {
            unsafe
            {
                fixed (byte* p = &buffer[index]) Assign(p, value);
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, char value) {
            buffer[index] = unchecked((byte)value);
            buffer[index + 1] = unchecked((byte)(value >> 8));
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, float value) {
            unsafe
            {
                fixed (byte* p = &buffer[index]) Assign(p, value);
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, double value) {
            unsafe
            {
                fixed (byte* p = &buffer[index]) Assign(p, value);
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, decimal value) {
            unsafe
            {
                fixed (byte* p = &buffer[index]) Assign(p, value);
            }
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
