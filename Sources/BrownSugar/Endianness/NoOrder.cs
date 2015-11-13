/**
 * @file
 * @brief エンディアンに影響されないバッファ操作関係
 */

using System;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>エンディアンに影響されないバッファ操作関係</summary>
    public class NoOrder {

        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe byte ToUInt8(byte* buffer) {
            return *buffer;
        }
        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe sbyte ToInt8(byte* buffer) {
            return *(sbyte*)buffer;
        }
        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe bool ToBoolean(byte* buffer) {
            return *buffer != 0;
        }

        /// <summary>バッファ内の値を取得</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>バッファ内の値を取得</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>バッファ内の値を取得</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        //
        //
        //

        /// <summary>マーシャル時のサイズを求めます</summary>
        /// <seealso cref="Marshal.SizeOf(object)"/>
        public static int MarshalSizeOf<T>(T obj) {
            return Marshal.SizeOf(obj);
        }

    }
}
