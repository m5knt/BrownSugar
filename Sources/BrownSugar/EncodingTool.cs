/**
 * @file
 * @brief エンコード関係
 */

using System;
using System.Text;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>エンコード関係</summary>
    public class EncodingTool {

        /// <summary>ボム無しUTF8エンコーダー</summary>
        public static Encoding UTF8N;

        static EncodingTool() {
            UTF8N = new UTF8Encoding(false);
        }
    }
}


