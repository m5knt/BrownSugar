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

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe byte ToUInt8(byte* buffer) {
            return buffer[0];
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe sbyte ToInt8(byte* buffer) {
            return ((sbyte*)buffer)[0];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe bool ToBoolean(byte* buffer) {
            return buffer[0] != 0;
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
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
            // il 24 bytes
            return unchecked((uint)((((p[0] << 8 | p[1]) << 8) | p[2]) << 8 | p[3]));
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* p) {
            if ((((int)p) & 7) == 0) {
                if (!BitConverter.IsLittleEndian) {
                    return ((ulong*)p)[0];
                }
                else {
                    return (ulong)
                        ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                        ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
                }
            }
            // il 55 bytes
            return (ulong)
                ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* p) {
            // il 24 bytes
            return ((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3];
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe long ToInt64(byte* p) {
            if ((((int)p) & 7) == 0) {
                if (!BitConverter.IsLittleEndian) {
                    return ((long*)p)[0];
                } else {
                    // il 52 bytes
                    return unchecked((long)
                        ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                        ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
                }
            }
            // il 52 bytes
            return unchecked((long)
                ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]));
        }

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* p) {
            if (((int)p & 3) == 0) {
                if (!BitConverter.IsLittleEndian) {
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

        /// <summary>ビッグエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* p) {
            if ((((int)p) & 7) == 0) {
                if (!BitConverter.IsLittleEndian) {
                    return ((double*)p)[0];
                }
                else {
                    return BitConverter.Int64BitsToDouble(((long*)p)[0].SwapByteOrder());
                }
            }
            return BitConverter.Int64BitsToDouble((
                (long)(((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                (uint)(((p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7])
                ));
        }

        //
        //
        //

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
        public static unsafe void Assign(byte* p, bool value) {
            ((bool*)p)[0] = value;
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = unchecked((byte)(value >> 8));
            buffer[1] = unchecked((byte)value);
        }

        /// <summary>ビッグエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            buffer[0] = ((byte*)&value)[0];
            buffer[1] = ((byte*)&value)[1];
            buffer[2] = ((byte*)&value)[2];
            buffer[3] = ((byte*)&value)[3];
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
        public static uint ToUInt32(byte[] buffer, int index) {
            return (
                (uint)buffer[index] << 24 |
                (uint)buffer[index + 1] << 16 |
                (uint)buffer[index + 2] << 8 |
                buffer[index + 3]
                );
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed(byte* p = &buffer[index])
            {
                if ((((int)p) & 7) == 0) {
                    if (!BitConverter.IsLittleEndian) {
                        return ((ulong*)p)[0];
                    } else {
                        return (ulong)
                           ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                           ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
                    }
                }
                // il 55 bytes
                return (ulong)
                    ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                    ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]);
            }
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe int ToInt32(byte[] buffer, int index) {
            fixed(byte* p = &buffer[index])
            {
                return ((p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3];
            }
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index])
            {
                if ((index & 7) == 0) {
                    if (!BitConverter.IsLittleEndian) {
                        return ((long*)p)[0];
                    }
                    else {
                        return unchecked((long)
                            ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                            ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));

                    }
                }
                // il 52 bytes
                return unchecked((long)
                    ((((uint)p[0] << 8 | p[1]) << 8 | p[2]) << 8 | p[3]) << 32 |
                    ((((uint)p[4] << 8 | p[5]) << 8 | p[6]) << 8 | p[7]));
            }
        }

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            if ((index & 3) == 0) {
                fixed (byte* fix = buffer)
                {
                    var p = fix + index;
                    if (!BitConverter.IsLittleEndian) {
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

        /// <summary>ビッグエンディアン順でバッファ読み込み</summary>
        public static double ToDouble(byte[] p, int i) {
            return BitConverter.Int64BitsToDouble(
                (long)(((p[i + 0] << 8 | p[i + 1]) << 8 | p[i + 2]) << 8 | p[i + 3]) << 32 |
                (uint)(((p[i + 4] << 8 | p[i + 5]) << 8 | p[i + 6]) << 8 | p[i + 7])
                );
        }
    }
}
