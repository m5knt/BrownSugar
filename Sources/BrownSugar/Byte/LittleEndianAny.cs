/**
 * @file
 * @brief リトルエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>バッファをリトルエンディアン順で操作します</summary>
    public class LittleEndianAny : OneByte {

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
            return *(float*)&tmp;
        }

        public static unsafe float ToSingle(byte[] p, int i) {
            var tmp = ((p[i + 3] << 8 | p[i + 2]) << 8 | p[i+1]) << 8 | p[i];
            return *(float*)&tmp;
        }

        //
        //
        //

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(ulong*)p;
            }
            // il 55 bytes
            return (ulong)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
        }

        public static unsafe long ToInt64(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(long*)p;
            }
            return unchecked((long)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));

        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                    return *(ulong*)p;
                }
                return (ulong)
                    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]);
            }
        }

        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (BitConverter.IsLittleEndian && (index & 7) == 0) {
                    return *(long*)p;
                }
                return unchecked((long)
                    ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                    ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
            }
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(double*)p;
            }
            var tmp = unchecked((long)
                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
            return *(double*)&tmp;
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (BitConverter.IsLittleEndian && (index & 7) == 0) {
                    return *(double*)p;
                }
                var tmp = unchecked((long)
                                ((((uint)p[7] << 8 | p[6]) << 8 | p[5]) << 8 | p[4]) << 32 |
                                ((((uint)p[3] << 8 | p[2]) << 8 | p[1]) << 8 | p[0]));
                return *(double*)&tmp;
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
        public static unsafe void Assign(byte* buffer, short value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, ushort value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, short value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, char value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, uint value) {
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
            buffer[index + 2] = (byte)(value >> 16);
            buffer[index + 3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static void Assign(byte[] buffer, int index, int value) {
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            var tmp = *(uint*)&value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, float value) {
            var tmp = *(uint*)&value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
        }

        //
        //
        //

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, ulong value) {
            if (BitConverter.IsLittleEndian && ((int)buffer & 7) == 0) {
                *(ulong*)(buffer) = value;
                return;
            }
            byte* tmp = (byte*)&value;
            buffer[0] = tmp[0];
            buffer[1] = tmp[1];
            buffer[2] = tmp[2];
            buffer[3] = tmp[3];
            buffer[4] = tmp[4];
            buffer[5] = tmp[5];
            buffer[6] = tmp[6];
            buffer[7] = tmp[7];
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            if (BitConverter.IsLittleEndian && ((int)buffer & 7) == 0) {
                *(long*)(buffer) = value;
                return;
            }
            byte* tmp = (byte*)&value;
            buffer[0] = tmp[0];
            buffer[1] = tmp[1];
            buffer[2] = tmp[2];
            buffer[3] = tmp[3];
            buffer[4] = tmp[4];
            buffer[5] = tmp[5];
            buffer[6] = tmp[6];
            buffer[7] = tmp[7];
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, long value) {
            fixed (byte* fix = buffer)
            {
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(long*)(fix + index) = value;
                    return;
                }
                else {
                    var tmp = (byte*)&value;
                    var pointer = fix + index;
                    pointer[0] = tmp[0];
                    pointer[1] = tmp[1];
                    pointer[2] = tmp[2];
                    pointer[3] = tmp[3];
                    pointer[4] = tmp[4];
                    pointer[5] = tmp[5];
                    pointer[6] = tmp[6];
                    pointer[7] = tmp[7];
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, ulong value) {
            fixed (byte* fix = buffer)
            {
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(ulong*)(fix + index) = value;
                    return;
                }
                else {
                    var tmp = (byte*)&value;
                    var pointer = fix + index;
                    pointer[0] = tmp[0];
                    pointer[1] = tmp[1];
                    pointer[2] = tmp[2];
                    pointer[3] = tmp[3];
                    pointer[4] = tmp[4];
                    pointer[5] = tmp[5];
                    pointer[6] = tmp[6];
                    pointer[7] = tmp[7];
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            if (BitConverter.IsLittleEndian && ((int)buffer & 7) == 0) {
                *(double*)(buffer) = value;
                return;
            }
            byte* tmp = (byte*)&value;
            buffer[0] = tmp[0];
            buffer[1] = tmp[1];
            buffer[2] = tmp[2];
            buffer[3] = tmp[3];
            buffer[4] = tmp[4];
            buffer[5] = tmp[5];
            buffer[6] = tmp[6];
            buffer[7] = tmp[7];
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, double value) {
            fixed (byte* fix = buffer)
            {
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(double*)(fix + index) = value;
                    return;
                }
                else {
                    var tmp = (byte*)&value;
                    var pointer = fix + index;
                    pointer[0] = tmp[0];
                    pointer[1] = tmp[1];
                    pointer[2] = tmp[2];
                    pointer[3] = tmp[3];
                    pointer[4] = tmp[4];
                    pointer[5] = tmp[5];
                    pointer[6] = tmp[6];
                    pointer[7] = tmp[7];
                }
            }
        }

        /*
         *
         */

    }
}
