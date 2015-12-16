/**
 * @file
 * @brief DateTimeの拡張メソッド関係
 */

using System;
using System.Runtime.CompilerServices;

namespace ThunderEgg.BrownSugar.Extentions {

    /// <summary>DateTimeの拡張メソッド関係</summary>
    public static class DateTimeExtension {
		static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		// DateTimeではロケールがあいまいなのでよくない
		public static long NowUnicTimeToUnixTime(this DateTime self) {
			return (long)(self - UnixEpoch).TotalSeconds;
		}
	}
}

