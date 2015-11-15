/**
* @file
* @brief マーシャル関係のエクステンション
*/

using System;
using System.Runtime.InteropServices;

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>マーシャル関係のエクステンション</summary>
    public static class MarshalExtension {

        /// <summary>マーシャル時のサイズを返す</summary>
        public static int MarshalSize(this Type type) {
            return Marshal.SizeOf(type);
        }

        /// <summary>マーシャル時のサイズを返す</summary>
        public static int MarshalSize<T>(this T self) {
            return Marshal.SizeOf(self);
        }

        /// <summary>マーシャルアトリビュートのサイズを返す</summary>
        public static int MarshalCount(this Type type, string name) {
            return ByteOrder.MarshalCount(type, name);
        }

        /// <summary>マーシャルアトリビュートのサイズを返す</summary>
        public static int MarshalCount<T>(this T self, string name) {
            return ByteOrder.MarshalCount(self, name);
        }

        /// <summary>マーシャルアトリビュートのオフセットを返す</summary>
        public static int MarshalOffset(this Type type, string name) {
            return Marshal.OffsetOf(type, name).ToInt32();
        }

        /// <summary>マーシャルアトリビュートのオフセットを返す</summary>
        public static int MarshalOffset<T>(this T self, string name) {
            return Marshal.OffsetOf(typeof(T), name).ToInt32();
        }

    }
}

