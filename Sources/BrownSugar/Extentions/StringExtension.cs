/**
 * @file
 */

using System;

namespace ThunderEgg.BrownSugar.Extentions {

    public static class StringExtension {

        public static string Copy(this string self) {
            return string.Copy(self);
        }

        public static string Format(this string format, params object[] args) {
            return string.Format(format, args);
        }

        public static string Format(this string format, object arg0) {
            return string.Format(format, arg0);
        }

        public static string Format(this string format, object arg0, object arg1) {
            return string.Format(format, arg0, arg1);
        }

        public static string Format(this string format, object arg0, object arg1, object arg2) {
            return string.Format(format, arg0, arg1, arg2);
        }

        public static string Intern(this string self) {
            return string.Intern(self);
        }

        public static string IsInterned(this string self) {
            return string.IsInterned(self);
        }

        public static bool IsNullOrEmpty(this string self) {
            return string.IsNullOrEmpty(self);
        }

        public static string Join(this string separator, string[] value) {
            return string.Join(separator, value);
        }

        public static string Join(this string separator, string[] value, int startIndex, int count) {
            return string.Join(separator, value, startIndex, count);
        }

        //
        //
        //

        public static byte ToUInt8(this string self) {
            return Convert.ToByte(self);
        }

        public static UInt16 ToUInt16(this string self) {
            return Convert.ToUInt16(self);
        }

        public static UInt32 ToUInt32(this string self) {
            return Convert.ToUInt32(self);
        }

        public static UInt64 ToUInt64(this string self) {
            return Convert.ToUInt64(self);
        }

        public static sbyte ToInt8(this string self) {
            return Convert.ToSByte(self);
        }

        public static Int16 ToInt16(this string self) {
            return Convert.ToInt16(self);
        }

        public static Int32 ToInt32(this string self) {
            return Convert.ToInt32(self);
        }

        public static Int64 ToInt64(this string self) {
            return Convert.ToInt64(self);
        }

        public static byte ToUInt8(this string self, int fbase) {
            return Convert.ToByte(self, fbase);
        }

        public static UInt16 ToUInt16(this string self, int fbase) {
            return Convert.ToUInt16(self, fbase);
        }

        public static UInt32 ToUInt32(this string self, int fbase) {
            return Convert.ToUInt32(self, fbase);
        }

        public static UInt64 ToUInt64(this string self, int fbase) {
            return Convert.ToUInt64(self, fbase);
        }

        public static sbyte ToInt8(this string self, int fbase) {
            return Convert.ToSByte(self, fbase);
        }

        public static Int16 ToInt16(this string self, int fbase) {
            return Convert.ToInt16(self, fbase);
        }

        public static Int32 ToInt32(this string self, int fbase) {
            return Convert.ToInt32(self, fbase);
        }

        public static Int64 ToInt64(this string self, int fbase) {
            return Convert.ToInt64(self, fbase);
        }

        public static bool ToBoolean(this string self) {
            return Convert.ToBoolean(self);
        }

        public static float ToSingle(this string self) {
            return Convert.ToSingle(self);
        }

        public static double ToDouble(this string self) {
            return Convert.ToDouble(self);
        }
    }
}

