/**
 * @file
 * @brief 日付関連
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>日付関連</summary>
    public class DateTimeTool {

        /// <summary>Unixエポックタイム</summary>
        static DateTime UnixEpoch = new DateTime(1970, 1, 1);

        /// <summary>Unixタイムを返す</summary>
        public static long NowUnixTime() {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }
    }
}


