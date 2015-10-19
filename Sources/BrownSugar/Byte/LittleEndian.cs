/**
 * @file
 * @brief リトルエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>リトルエンディアン順でバッファを操作します</summary>
    public static class LittleEndian {

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe byte ToUInt8(byte* buffer) {
            return buffer[0];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe sbyte ToInt8(byte* buffer) {
            return ((sbyte*)buffer)[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe bool ToBoolean(byte* buffer) {
            return buffer[0] != 0;
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        //
        //
        //

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
            // il 24 bytes 
            return (((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]; // 24
#if false
            // il 26 bytes 
            return (((uint)p[3] << 24) | ((uint)p[2] << 16) | ((uint)p[1] << 8) | p[0]);
#endif
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] p, int i) {
            return (p[i] | (uint)p[i + 1] << 8 | (uint)p[i + 2] << 16 | (uint)p[i + 3] << 24);
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* p) {
            return ((p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] b, int i) {
            return unchecked((int)(
                b[i] |
                (uint)b[i + 1] << 8 |
                (uint)b[i + 2] << 16 |
                (uint)b[i + 3] << 24
                ));
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* p) {
            if (((int)p & 3) == 0) {
                if (BitConverter.IsLittleEndian) {
                    return ((float*)p)[0];
                }
                else {
                    float tmp;
                    ((uint*)&tmp)[0] = ((uint*)p)[0].SwapByteOrder();
                    return tmp;
                }
            }
            else {
                float tmp;
                ((byte*)&tmp)[0] = p[0];
                ((byte*)&tmp)[1] = p[1];
                ((byte*)&tmp)[2] = p[2];
                ((byte*)&tmp)[3] = p[3];
                return tmp;
            }
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            if ((index & 3) == 0) {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    if (BitConverter.IsLittleEndian) {
                        return ((float*)p)[0];
                    }
                    else {
                        float tmp;
                        ((uint*)&tmp)[0] = ((uint*)p)[0].SwapByteOrder();
                        return tmp;
                    }
                }
            }
            else {
                float tmp;
                ((byte*)&tmp)[0] = buffer[0];
                ((byte*)&tmp)[1] = buffer[1];
                ((byte*)&tmp)[2] = buffer[2];
                ((byte*)&tmp)[3] = buffer[3];
                return tmp;
            }
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

        /*
         *
         */

    }
}
