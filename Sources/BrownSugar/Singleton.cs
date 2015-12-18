/**
 * @file
 * @brief シングルトン
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>シングルトン</summary>
    public class Singleton<T>
        where T : new()
    {
        public static T Instance;

        static Singleton() {
            Instance = new T();
        }
    }
}


