/**
 * @file
 * @brief 文字列回りのシュガー
 */

using System.Globalization;

//
//
//

namespace ThunderEgg.BrownSugar.Extentions {

    public static class Chars {

        /// <summary>文字を倍精度値で返す</summary>
        /// <seealso cref="char.GetNumericValue(char)"/>
        public static double GetNumericValue(this char c) {
            return char.GetNumericValue(c);
        }

        /// <summary>文字のユニコードカテゴリを返す</summary>
        /// <seealso cref="char.GetUnicodeCategory(char)"/>
        public static UnicodeCategory GetUnicodeCategory(this char c) {
            return char.GetUnicodeCategory(c);
        }

        /// <summary>制御文字であるかを返す</summary>
        /// <seealso cref="char.IsControl(char)"/>
        public static bool IsControl(this char c) {
            return char.IsControl(c);
        }

        /// <summary>数字であるかを返す</summary>
        /// <seealso cref="char.IsDigit(char)"/>
        public static bool IsDigit(this char c) {
            return char.IsDigit(c);
        }

        /// <summary>上位サロゲートであるかを返す</summary>
        /// <seealso cref="char.IsHighSurrogate(char)"/>
        public static bool IsHighSurrogate(this char c) {
            return char.IsHighSurrogate(c);
        }

        /// <summary>文字であるかを返す</summary>
        /// <seealso cref="char.IsLetter(char)"/>
        public static bool IsLetter(this char c) {
            return char.IsLetter(c);
        }

        /// <summary>文字または数字であるかを返す</summary>
        /// <seealso cref="char.IsLetterOrDigit(char)"/>
        public static bool IsLetterOrDigit(this char c) {
            return char.IsLetterOrDigit(c);
        }

        /// <summary>小文字であるかを返す</summary>
        /// <seealso cref="char.IsLower(char)"/>
        public static bool IsLower(this char c) {
            return char.IsLower(c);
        }

        /// <summary>下位サロゲートであるか返す</summary>
        /// <seealso cref="char.IsLowSurrogate(char)"/>
        public static bool IsLowSurrogate(this char c) {
            return char.IsLowSurrogate(c);
        }

        /// <summary>数字であるかを返す</summary>
        /// <seealso cref="char.IsNumber(char)"/>
        public static bool IsNumber(this char c) {
            return char.IsNumber(c);
        }

        /// <summary>区切り文字であるかを返す</summary>
        /// <seealso cref="char.IsPunctuation(char)"/>
        public static bool IsPunctuation(this char c) {
            return char.IsPunctuation(c);
        }

        /// <summary>区切り文字であるかを返す</summary>
        /// <seealso cref="char.IsSeparator(char)"/>
        public static bool IsSeparator(this char c) {
            return char.IsSeparator(c);
        }

        /// <summary>サロゲートコード単位を持つか</summary>
        /// <seealso cref="char.IsSurrogate(char)"/>
        public static bool IsSurrogate(this char c) {
            return char.IsSurrogate(c);
        }

        /// <summary>記号であるかを返す</summary>
        /// <seealso cref="char.IsSymbol(char)"/>
        public static bool IsSymbol(this char c) {
            return char.IsSymbol(c);
        }

        /// <summary>大文字であるかを返す</summary>
        /// <seealso cref="char.IsUpper(char)"/>
        public static bool IsUpper(this char c) {
            return char.IsUpper(c);
        }

        /// <summary>空白であるかを返す</summary>
        /// <seealso cref="char.IsWhiteSpace(char)"/>
        public static bool IsWhiteSpace(this char c) {
            return char.IsWhiteSpace(c);
        }

        /// <summary>カレントカルチャ規則で小文字化して返す</summary>
        /// <seealso cref="char.ToLower(char)"/>
        public static char ToLower(this char c) {
            return char.ToLower(c);
        }

        /// <summary>指定カルチャ規則で小文字化して返す</summary>
        /// <seealso cref="char.ToLower(char, CultureInfo)"/>
        public static char ToLower(this char c, CultureInfo culture) {
            return char.ToLower(c, culture);
        }

        /// <summary>インバリアントカルチャ規則で小文字化して返す</summary>
        /// <seealso cref="char.ToLowerInvariant(char)"/>
        public static char ToLowerInvariant(this char c) {
            return char.ToLowerInvariant(c);
        }

        /// <summary>文字列にして返す</summary>
        /// <seealso cref="char.ToString(char)"/>
        public static string ToString(this char c) {
            return char.ToString(c);
        }

        /// <summary>カレントカルチャ規則で大文字化して返す</summary>
        /// <seealso cref="char.ToUpper(char)"/>
        public static char ToUpper(this char c) {
            return char.ToUpper(c);
        }

        /// <summary>指定カルチャ規則で大文字化して返す</summary>
        /// <seealso cref="char.ToUpper(char, CultureInfo)"/>
        public static char ToUpper(this char c, CultureInfo culture) {
            return char.ToUpper(c, culture);
        }

        /// <summary>インバリアントカルチャ規則で大文字化して返す</summary>
        /// <seealso cref="char.ToUpperInvariant(char)"/>
        public static char ToUpperInvariant(this char c) {
            return char.ToUpperInvariant(c);
        }
    }
}
