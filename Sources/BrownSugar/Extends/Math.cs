using System;

//
//
//

namespace ThunderEgg.BrownSugar.Extentions {

    public static class Maths {

        /// <summary>無限大であるかを返す</summary>
        public static bool IsInfinity(this float value) {　return float.IsInfinity(value);　}
        /// <summary>無限大であるかを返す</summary>
        public static bool IsInfinity(this double value) {　return double.IsInfinity(value);　}

        /// <summary>非数であるかを返す</summary>
        public static bool IsNaN(this float value) {　return float.IsNaN(value);　}
        /// <summary>非数であるかを返す</summary>
        public static bool IsNaN(this double value) {　return double.IsNaN(value);　}

        /// <summary>負の無限大であるかを返す</summary>
        public static bool IsNegativeInfinity(this float value) {　return float.IsNegativeInfinity(value);　}
        /// <summary>負の無限大であるかを返す</summary>
        public static bool IsNegativeInfinity(this double value) {　return double.IsNegativeInfinity(value);　}
        
        /// <summary>正の無限大であるかを返す</summary>
        public static bool IsPositiveInfinity(this float value) {　return float.IsPositiveInfinity(value);　}
        /// <summary>正の無限大であるかを返す</summary>
        public static bool IsPositiveInfinity(this double value) {　return double.IsPositiveInfinity(value);　}

        /// <summary>小数点以下切り上げを返す</summary>
        public static double Ceiling(this double value) { return Math.Ceiling(value); }
        /// <summary>小数点以下切り上げを返す</summary>
        public static decimal Ceiling(this decimal value) { return Math.Ceiling(value); }
        /// <summary>小数点以下切り捨てを返す</summary>
        public static double Floor(this double value) { return Math.Floor(value); }
        /// <summary>小数点以下切り捨てを返す</summary>
        public static decimal Floor(this decimal value) { return Math.Floor(value); }
        /// <summary>整数部を返す</summary>
        public static double Truncate(this double value) { return Math.Truncate(value); }
        /// <summary>整数部を返す</summary>
        public static decimal Truncate(this decimal value) { return Math.Truncate(value); }
        /// <summary>銀行丸め整数部を返す</summary>
        public static double Round(this double value) { return Math.Round(value); }
        /// <summary>銀行丸め整数部を返す</summary>
        public static decimal Round(this decimal value) { return Math.Round(value); }

        /// <summary>絶対値を返す</summary>
        public static float Abs(this sbyte value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static float Abs(this short value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static float Abs(this int value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static float Abs(this long value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static float Abs(this float value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static double Abs(this double value) { return Math.Abs(value); }
        /// <summary>絶対値を返す</summary>
        public static decimal Abs(this decimal value) { return Math.Abs(value); }

        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this sbyte value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this short value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this int value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this long value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this float value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this double value) { return Math.Sign(value); }
        /// <summary>符号(-1, 0, 1)を返す</summary>
        public static int Sign(this decimal value) { return Math.Sign(value); }
    }
}
