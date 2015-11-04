/**
 * @file
 * @brief 計算関係
 */

using System;

//
//
//

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>計算関係</summary>
    public static class MathExtension {

        /// <summary>無限大であるかを返す</summary>
        /// <seealso cref="float.IsInfinity(float)"/>
        public static bool IsInfinity(this float value) {　return float.IsInfinity(value);　}
        /// <summary>無限大であるかを返す</summary>
        /// <seealso cref="double.IsInfinity(double)"/>
        public static bool IsInfinity(this double value) {　return double.IsInfinity(value);　}

        /// <summary>非数であるかを返す</summary>
        /// <seealso cref="float.IsNaN(float)"/>
        public static bool IsNaN(this float value) {　return float.IsNaN(value);　}
        /// <summary>非数であるかを返す</summary>
        /// <seealso cref="double.IsNaN(double)"/>
        public static bool IsNaN(this double value) {　return double.IsNaN(value);　}

        /// <summary>負の無限大であるかを返す</summary>
        /// <seealso cref="float.IsNegativeInfinity(float)"/>
        public static bool IsNegativeInfinity(this float value) {　return float.IsNegativeInfinity(value);　}
        /// <summary>負の無限大であるかを返す</summary>
        /// <seealso cref="double.IsNegativeInfinity(double)"/>
        public static bool IsNegativeInfinity(this double value) {　return double.IsNegativeInfinity(value);　}
        
        /// <summary>正の無限大であるかを返す</summary>
        /// <seealso cref="float.IsPositiveInfinity(float)"/>
        public static bool IsPositiveInfinity(this float value) {　return float.IsPositiveInfinity(value);　}
        /// <summary>正の無限大であるかを返す</summary>
        /// <seealso cref="double.IsPositiveInfinity(double)"/>
        public static bool IsPositiveInfinity(this double value) {　return double.IsPositiveInfinity(value);　}

        /// <summary>小数点以下切り上げを返す</summary>
        /// <seealso cref="Math.Ceiling(double)"/>
        public static double Ceiling(this double value) { return Math.Ceiling(value); }
        /// <summary>小数点以下切り上げを返す</summary>
        /// <seealso cref="Math.Ceiling(decimal)"/>
        public static decimal Ceiling(this decimal value) { return Math.Ceiling(value); }
        /// <summary>小数点以下切り捨てを返す</summary>
        /// <seealso cref="Math.Floor(double)"/>
        public static double Floor(this double value) { return Math.Floor(value); }
        /// <summary>小数点以下切り捨てを返す</summary>
        /// <seealso cref="Math.Floor(decimal)"/>
        public static decimal Floor(this decimal value) { return Math.Floor(value); }
        /// <summary>整数部を返す</summary>
        /// <seealso cref="Math.Truncate(double)"/>
        public static double Truncate(this double value) { return Math.Truncate(value); }
        /// <summary>整数部を返す</summary>
        /// <seealso cref="Math.Truncate(decimal)"/>
        public static decimal Truncate(this decimal value) { return Math.Truncate(value); }
        /// <summary>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(double)"/>
        public static double Round(this double value) { return Math.Round(value); }
        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(decimal)"/>
        public static decimal Round(this decimal value) { return Math.Round(value); }

        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(double, MidpointRounding)"/>
        public static double Round(this double value, MidpointRounding mode) { return Math.Round(value, mode); }
        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(decimal, MidpointRounding)"/>
        public static decimal Round(this decimal value, MidpointRounding mode) { return Math.Round(value, mode); }
        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(double, int)"/>
        public static double Round(this double value, int digits) { return Math.Round(value, digits); }
        /// <seealso cref="Math.Round(decimal, int)"/>
        public static decimal Round(this decimal value, int digits) { return Math.Round(value, digits); }
        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(double, int, MidpointRounding)"/>
        public static double Round(this double value, int digits, MidpointRounding mode) { return Math.Round(value, digits, mode); }
        /// <summary>>整数に丸め値を返す</summary>
        /// <seealso cref="Math.Round(decimal, int, MidpointRounding)"/>
        public static decimal Round(this decimal value, int digits, MidpointRounding mode) { return Math.Round(value, digits, mode); }

        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(sbyte)"/>
        public static float Abs(this sbyte value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(short)"/>
        public static float Abs(this short value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(int)"/>
        public static float Abs(this int value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(long)"/>
        public static float Abs(this long value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(float)"/>
        public static float Abs(this float value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(double)"/>
        public static double Abs(this double value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        /// <seealso cref="Math.Abs(decimal)"/>
        public static decimal Abs(this decimal value) { return Math.Abs(value); }

        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(sbyte)"/>
        public static int Sign(this sbyte value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(short)"/>
        public static int Sign(this short value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(int)"/>
        public static int Sign(this int value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(long)"/>
        public static int Sign(this long value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(float)"/>
        public static int Sign(this float value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(double)"/>
        public static int Sign(this double value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        /// <seealso cref="Math.Sign(decimal)"/>
        public static int Sign(this decimal value) { return Math.Sign(value); }
    }
}
