/**
 * @brief StringBuffer回り
 */

using System;
using System.Text;

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>StringBuffer回り</summary>
    public static class StringBufferExtension {
        public static void Clear(this StringBuilder self) {
            self.Length = 0;
        }
    }
}

