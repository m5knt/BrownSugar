/**
* @file
* @brief マーシャル関係のエクステンション
*/

using System;
using System.Runtime.InteropServices;

//
//
//

namespace ThunderEgg.BrownSugar {

    /// <summary>マーシャル関係のエクステンション</summary>
    public static class MarshalEgg {

        /// <summary>マーシャルアトリビュートのサイズカウントを返す</summary>
        public static int Count(Type type, string name) {
            var field = type.GetField(name);
            var field_type = field.FieldType;
            if (field_type == typeof(string)) {
                // 文字列
                var attribs = field.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                var attrib = (MarshalAsAttribute)attribs[0];
                return attrib.SizeConst;
            }
            else if (field_type.IsArray) {
                // 配列
                var attribs = field.GetCustomAttributes(typeof(MarshalAsAttribute), false);
                var attrib = (MarshalAsAttribute)attribs[0];
                return attrib.SizeConst;
            }
            return 0;
        }
    }

}


namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>マーシャル関係のエクステンション</summary>
    public static partial class MarshalExtension {

        /// <summary>マーシャル時のサイズを返す</summary>
        public static int MarshalSize(this Type type) {
            return Marshal.SizeOf(type);
        }

        /// <summary>マーシャル時のサイズを返す</summary>
        public static int MarshalSize<T>(this T self) {
            return Marshal.SizeOf(self);
        }

        /// <summary>自動プロパティーのフィールド名を得る</summary>
        public static string ToBackingField(this string self) {
            return string.Format("<{0}>k__BackingField", self);
        }

        /// <summary>マーシャルアトリビュートのサイズカウントを返す</summary>
        public static int MarshalCount(this Type type, string name) {
            return MarshalEgg.Count(type, name);
        }

        /// <summary>マーシャルアトリビュートのサイズカウントを返す</summary>
        public static int MarshalCount<T>(this T self, string name) {
            return MarshalEgg.Count(self.GetType(), name);
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

