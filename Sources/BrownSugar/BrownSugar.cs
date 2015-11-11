/*
 * @file
 * @brief 定数定義など
 */

using System;
using System.Net;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>定数定義など</summary>
    public static class Const {
        /// <summary>常に真</summary>
        public const bool True = true;
        /// <summary>常に偽</summary>
        public const bool False = false;
#if DEBUG
        /// <summary>デバッグビルドであるか</summary>
        public const bool IsDebug = true;
#else
        /// <summary>デバッグビルドであるか</summary>
        public const bool IsDebug = false;
#endif
    }
}

