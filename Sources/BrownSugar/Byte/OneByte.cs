/**
 * @file
 * @brief バイトオーダー不要のバッファ操作回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>バイトオーダー不要のバッファ操作回り</summary>
    public class OneByte {

        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe byte ToUInt8(byte* b) {
            return *b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe sbyte ToInt8(byte* b) {
            return *(sbyte*)b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe bool ToBoolean(byte* b) {
            return *(bool*)b;
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        /// <summary>byte/遅いです</summary>
        public static byte ToUInt8(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(ArraySegment<byte> buffer, int index) {
            return unchecked((sbyte)buffer.Array[buffer.Offset + index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index] != 0;
        }

        //
        //
        //

        /// <summary>ポインタ位置に値を書/遅いですく</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)0 : (byte)1;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)0 : (byte)1;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, byte value) {
            buffer.Array[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, sbyte value) {
            buffer.Array[buffer.Offset + index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, bool value) {
            buffer.Array[buffer.Offset + index] = value ? (byte)0 : (byte)1;
        }
    }
}
