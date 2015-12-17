/**
 * @brief StringBuffer回り
 */

using System;
using System.Text;

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>StringBuffer回り</summary>
    public static class StringBuilderExtension {
        /// <summary>バッファ内容をクリアする</summary>
        public static StringBuilder Clear(this StringBuilder self) {
            self.Length = 0;
            return self;
        }
    }
}

