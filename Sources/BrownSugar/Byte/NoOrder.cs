/**
 * @file
 * @brief ビッグエンディアン回り
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public class NoOrder {

        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe byte ToUInt8(byte* b) {
            return *b;
        }
        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe sbyte ToInt8(byte* b) {
            return *(sbyte*)b;
        }
        /// <summary>ポインタ位置の値を取得</summary>
        public static unsafe bool ToBoolean(byte* b) {
            return *b != 0;
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

    }
}
