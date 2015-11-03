using System;
using System.Collections.Generic;

namespace ThunderEgg.BrownSugar.Extentions {

    public static partial class Collections {

        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action) {
            foreach(var t in self) {
                action(t);
            }
        }

        public static T[] MakeArray<T>(this T value) {
            return new T[] { value };
        }

        public static List<T> MakeList<T>(this T value) {
            return new List<T>() { value };
        }

#if false
        public static IList<T> Add<T>(this IList<T> self, IEnumerable<T> collection) {
            foreach (var t in collection) {
                self.Add(t);
            }
            return self;
        }

        public static IList<T> Add<T>(this IList<T> self, T value) {
            self.Add(value);
            return self;
        }
#endif
        /// <summary>例外を投げない辞書登録</summary>
        public static void Update<D, K, V>(this D idictionary, K key, V value)
            where D : IDictionary<K, V> //
        {
            if (idictionary.ContainsKey(key)) {
                idictionary[key] = value;
            }
            else {
                idictionary.Add(key, value);
            }
        }

        //
        //
        //

    }
}
