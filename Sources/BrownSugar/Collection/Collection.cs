using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ThunderEgg.BrownSugar {

    public static partial class SugarCollection {

        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action) {
            foreach(var t in self) {
                action(t);
            }
        }

        public static IList<T> Add<T>(this IList<T> self, IEnumerable<T> collection) {
            foreach (var t in collection) {
                self.Add(t);
            }
            return self;
        }

#if false
        public static IList<T> Add<T>(this IList<T> self, T value) {
            self.Add(value);
            return self;
        }
#endif
            /// <summary>例外を投げない辞書登録</summary>
        public static void Update<D, K, V>(
            this D idictionary, K key, V value)
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

        /// <summary>アレイを作成します</summary>
        public static byte[] ToArray(this byte value) { return new byte[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static ushort[] ToArray(this ushort value) { return new ushort[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static uint[] ToArray(this uint value) { return new uint[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static ulong[] ToArray(this ulong value) { return new ulong[] { value }; }

        /// <summary>アレイを作成します</summary>
        public static sbyte[] ToArray(this sbyte value) { return new sbyte[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static short[] ToArray(this short value) { return new short[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static int[] ToArray(this int value) { return new int[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static long[] ToArray(this long value) { return new long[] { value }; }

        /// <summary>アレイを作成します</summary>
        public static bool[] ToArray(this bool value) { return new bool[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static char[] ToArray(this char value) { return new char[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static float[] ToArray(this float value) { return new float[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static double[] ToArray(this double value) { return new double[] { value }; }
        /// <summary>アレイを作成します</summary>
        public static string[] ToArray(this string value) { return new string[] { value }; }

        //
        //
        //

        /// <summary>リストを作成します</summary>
        public static List<byte> ToList(this byte value) { return new List<byte>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<ushort> ToList(this ushort value) { return new List<ushort>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<uint> ToList(this uint value) { return new List<uint>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<ulong> ToList(this ulong value) { return new List<ulong>() { value }; }

        /// <summary>リストを作成します</summary>
        public static List<sbyte> ToList(this sbyte value) { return new List<sbyte>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<short> ToList(this short value) { return new List<short>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<int> ToList(this int value) { return new List<int>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<long> ToList(this long value) { return new List<long>() { value }; }

        /// <summary>リストを作成します</summary>
        public static List<bool> ToList(this bool value) { return new List<bool>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<char> ToList(this char value) { return new List<char>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<float> ToList(this float value) { return new List<float>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<double> ToList(this double value) { return new List<double>() { value }; }
        /// <summary>リストを作成します</summary>
        public static List<string> ToList(this string value) { return new List<string>() { value }; }

        //
        //
        //
    }
}
