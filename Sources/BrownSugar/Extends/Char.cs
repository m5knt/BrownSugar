/**
 * @file
 * @brief 文字列回りのシュガー
 * @author Yukio KANEDA
 */

using System.Globalization;

//
//
//

namespace ThunderEgg.BrownSugar.Sugar {

    public static class SugarChar {

        public static double GetNumericValue(this char c) {
            return char.GetNumericValue(c);
        }

        public static UnicodeCategory GetUnicodeCategory(this char c) {
            return char.GetUnicodeCategory(c);
        }

        /// <summary>制御文字</summary>
        public static bool IsControl(this char c) {
            return char.IsControl(c);
        }

        /// <summary>数字であるかを返す</summary>
        public static bool IsDigit(this char c) {
            return char.IsDigit(c);
        }

        /// <summary>上位サロゲートであるかを返す</summary>
        public static bool IsHighSurrogate(this char c) {
            return char.IsHighSurrogate(c);
        }

        /// <summary>文字であるかを返す</summary>
        public static bool IsLetter(this char c) {
            return char.IsLetter(c);
        }

        /// <summary>文字または数字であるかを返す</summary>
        public static bool IsLetterOrDigit(this char c) {
            return char.IsLetterOrDigit(c);
        }

        /// <summary>小文字であるかを返す</summary>
        public static bool IsLower(this char c) {
            return char.IsLower(c);
        }

        /// <summary>下位サロゲートであるか返す</summary>
        public static bool IsLowSurrogate(this char c) {
            return char.IsLowSurrogate(c);
        }

        /// <summary>数字であるかを返す</summary>
        public static bool IsNumber(this char c) {
            return char.IsNumber(c);
        }

        /// <summary>区切り文字であるかを返す</summary>
        public static bool IsPunctuation(this char c) {
            return char.IsPunctuation(c);
        }

        /// <summary>区切り文字であるかを返す</summary>
        public static bool IsSeparator(this char c) {
            return char.IsSeparator(c);
        }

        /// <summary>サロゲートコード単位を持つか</summary>
        public static bool IsSurrogate(this char c) {
            return char.IsSurrogate(c);
        }

        /// <summary>記号であるかを返す</summary>
        public static bool IsSymbol(this char c) {
            return char.IsSymbol(c);
        }

        /// <summary>大文字であるかを返す</summary>
        public static bool IsUpper(this char c) {
            return char.IsUpper(c);
        }

        /// <summary>空白であるかを返す</summary>
        public static bool IsWhiteSpace(this char c) {
            return char.IsWhiteSpace(c);
        }

        /// <summary>カレントカルチャ規則で小文字化して返す</summary>
        public static char ToLower(this char c) {
            return char.ToLower(c);
        }

        /// <summary>指定カルチャ規則で小文字化して返す</summary>
        public static char ToLower(this char c, CultureInfo culture) {
            return char.ToLower(c, culture);
        }

        /// <summary>インバリアントカルチャ規則で小文字化して返す</summary>
        public static char ToLowerInvariant(this char c) {
            return char.ToLowerInvariant(c);
        }

        /// <summary>文字列にして返す</summary>
        public static string ToString(this char c) {
            return char.ToString(c);
        }

        /// <summary>カレントカルチャ規則で大文字化して返す</summary>
        public static char ToUpper(this char c) {
            return char.ToUpper(c);
        }

        /// <summary>指定カルチャ規則で大文字化して返す</summary>
        public static char ToUpper(this char c, CultureInfo culture) {
            return char.ToUpper(c, culture);
        }

        /// <summary>インバリアントカルチャ規則で大文字化して返す</summary>
        public static char ToUpperInvariant(this char c) {
            return char.ToUpperInvariant(c);
        }
    }
}
