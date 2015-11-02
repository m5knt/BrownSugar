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
    public class LittleEndianAny : NoOrder {

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
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
            return (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static uint ToUInt32(byte[] p, int i) {
            return (uint)(p[i + 3] << 24 | p[i + 2] << 16 | p[i + 1] << 8 | p[i]);
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe int ToInt32(byte* p) {
            return p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0];
        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static int ToInt32(byte[] p, int i) {
            return p[i + 3] << 24 | p[i + 2] << 16 | p[i + 1] << 8 | p[i];
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe float ToSingle(byte* p) {
            var tmp = p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0];
            return *(float*)&tmp;
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static float ToSingle(byte[] p, int i) {
            unsafe
            {
                var tmp = p[i + 3] << 24 | p[i + 2] << 16 | p[i + 1] << 8 | p[i];
                return *(float*)&tmp;
            }
        }

        //
        //
        //

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe ulong ToUInt64(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(ulong*)p;
            }
            return 
                (ulong)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                 (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
        }

        public static unsafe long ToInt64(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(long*)p;
            }
            return 
                (long)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);

        }

        /// <summary>リトルエンディアン順でバッファ読み込み</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                    return *(ulong*)p;
                }
                return
                    (ulong)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                     (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
            }
        }

        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* fix = buffer)
            {
                var p = fix + index;
                if (BitConverter.IsLittleEndian && (index & 7) == 0) {
                    return *(long*)p;
                }
                return 
                    (long)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                    (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
            }
        }

        /// <summary>リトルエンディアン順で値を読み込みます</summary>
        public static unsafe double ToDouble(byte* p) {
            if (BitConverter.IsLittleEndian && ((int)p & 7) == 0) {
                return *(double*)p;
            }
            var tmp = 
                (ulong)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                 (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
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
                var tmp = 
                    (ulong)(p[7] << 24 | p[6] << 16 | p[5] << 8 | p[4]) << 32 |
                     (uint)(p[3] << 24 | p[2] << 16 | p[1] << 8 | p[0]);
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
            buffer[index] = (byte)value;
            buffer[index + 1] = (byte)(value >> 8);
            buffer[index + 2] = (byte)(value >> 16);
            buffer[index + 3] = (byte)(value >> 24);
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
            buffer[index] = (byte)tmp;
            buffer[index + 1] = (byte)(tmp >> 8);
            buffer[index + 2] = (byte)(tmp >> 16);
            buffer[index + 3] = (byte)(tmp >> 24);
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
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
            buffer[4] = (byte)(value >> 32);
            buffer[5] = (byte)(value >> 40);
            buffer[6] = (byte)(value >> 48);
            buffer[7] = (byte)(value >> 56);
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, long value) {
            if (BitConverter.IsLittleEndian && ((int)buffer & 7) == 0) {
                *(long*)(buffer) = value;
                return;
            }
            buffer[0] = (byte)value;
            buffer[1] = (byte)(value >> 8);
            buffer[2] = (byte)(value >> 16);
            buffer[3] = (byte)(value >> 24);
            buffer[4] = (byte)(value >> 32);
            buffer[5] = (byte)(value >> 40);
            buffer[6] = (byte)(value >> 48);
            buffer[7] = (byte)(value >> 56);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, long value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(long*)pointer = value;
                    return;
                }
                else {
                    pointer[0] = (byte)value;
                    pointer[1] = (byte)(value >> 8);
                    pointer[2] = (byte)(value >> 16);
                    pointer[3] = (byte)(value >> 24);
                    pointer[4] = (byte)(value >> 32);
                    pointer[5] = (byte)(value >> 40);
                    pointer[6] = (byte)(value >> 48);
                    pointer[7] = (byte)(value >> 56);
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, ulong value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(ulong*)pointer = value;
                    return;
                }
                else {
                    pointer[0] = (byte)value;
                    pointer[1] = (byte)(value >> 8);
                    pointer[2] = (byte)(value >> 16);
                    pointer[3] = (byte)(value >> 24);
                    pointer[4] = (byte)(value >> 32);
                    pointer[5] = (byte)(value >> 40);
                    pointer[6] = (byte)(value >> 48);
                    pointer[7] = (byte)(value >> 56);
                }
            }
        }

        /// <summary>リトルエンディアン順でバッファに書き込みます</summary>
        public static unsafe void Assign(byte* buffer, double value) {
            if (BitConverter.IsLittleEndian && ((int)buffer & 7) == 0) {
                *(double*)(buffer) = value;
                return;
            }
            var tmp = *(ulong*)&value;
            buffer[0] = (byte)tmp;
            buffer[1] = (byte)(tmp >> 8);
            buffer[2] = (byte)(tmp >> 16);
            buffer[3] = (byte)(tmp >> 24);
            buffer[4] = (byte)(tmp >> 32);
            buffer[5] = (byte)(tmp >> 40);
            buffer[6] = (byte)(tmp >> 48);
            buffer[7] = (byte)(tmp >> 56);
        }

        /// <summary>リトルエンディアン順でバッファに値を書き込みます</summary>
        public static unsafe void Assign(byte[] buffer, int index, double value) {
            fixed (byte* fix = buffer)
            {
                var pointer = fix + index;
                if (BitConverter.IsLittleEndian && ((int)index & 7) == 0) {
                    *(double*)pointer = value;
                    return;
                }
                else {
                    var tmp = *(ulong*)&value;
                    pointer[0] = (byte)tmp;
                    pointer[1] = (byte)(tmp >> 8);
                    pointer[2] = (byte)(tmp >> 16);
                    pointer[3] = (byte)(tmp >> 24);
                    pointer[4] = (byte)(tmp >> 32);
                    pointer[5] = (byte)(tmp >> 40);
                    pointer[6] = (byte)(tmp >> 48);
                    pointer[7] = (byte)(tmp >> 56);
                }
            }
        }

        /*
         *
         */

    }
}
