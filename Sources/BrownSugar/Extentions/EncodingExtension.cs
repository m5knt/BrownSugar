/**
 * @file
 */

using System;
using System.Text;

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>Encodingの拡張メソッド関係</summary>
	public static class EncodingExtension {

		static Encoding UTF8N_ = new UTF8Encoding(false);

        public static Encoding UTF8N(this string self) {
			return UTF8N_;
        }
    }
}

