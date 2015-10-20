﻿/**
 * @file
 * @brief リトルエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>リトルエンディアン順でバッファを操作します</summary>
    public class LittleEndian : NoByteOrder {

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
        public static unsafe uint ToUInt32(byte* p) {
            return (((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe uint ToUInt32(byte[] p, int i) {
            return (((uint)p[i + 3] << 8 | p[i + 2]) << 8 | p[i + 1]) << 8 | p[i];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* p) {
            return ((p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] p, int i) {
            return ((p[i + 3] << 8 | p[i + 2]) << 8 | p[i + 1]) << 8 | p[i];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* p) {
            var tmp = ((p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0];
            return ((float*)&tmp)[0];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte[] p, int index) {
            var tmp = ((p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0];
            return ((float*)&tmp)[0];
        }

        //
        //
        //

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* p) {
            if ((((int)p) & 7) == 0) {
                var t = ((ulong*)p)[0];
                return BitConverter.IsLittleEndian ? t : t.SwapByteOrder();
            }
            // il 55 bytes
            return (ulong)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
        }

        public static unsafe long ToInt64(byte* p) {
            if ((((int)p) & 7) == 0) {
                var t = ((long*)p)[0];
                return BitConverter.IsLittleEndian ? t : t.SwapByteOrder();
            }
            // il 55
            //return unchecked((long)
            //    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
            //    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
            return unchecked((long)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));

        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index])
            {
                if ((index & 7) == 0) {
                    return BitConverter.IsLittleEndian ?
                        ((ulong*)p)[0] : ((ulong*)p)[0].SwapByteOrder();
                }
                return (ulong)
                    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
            }
        }

        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index])
            {
                if ((index & 7) == 0) {
                    return BitConverter.IsLittleEndian ?
                        ((long*)p)[0] : ((long*)p)[0].SwapByteOrder();
                }
                return unchecked((long)
                    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
            }
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* p) {
            if ((((int)p) & 7) == 0) {
                if (BitConverter.IsLittleEndian) {
                    return ((double*)p)[0];
                }
                else {
                    return BitConverter.Int64BitsToDouble(((long*)p)[0].SwapByteOrder());
                 }
            }
            return BitConverter.Int64BitsToDouble(unchecked((long)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]))
                );
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index])
            {
                if ((index & 7) == 0) {
                    if (BitConverter.IsLittleEndian) {
                        return BitConverter.Int64BitsToDouble(((long*)p)[0]);
                    }
                    else {
                        return BitConverter.Int64BitsToDouble(unchecked((long)
                            ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                            ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0])));
                    }
                }
                return BitConverter.Int64BitsToDouble(unchecked((long)
                    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0])));
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

        //
        //
        //

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

        /*
         *
         */

    }
}
