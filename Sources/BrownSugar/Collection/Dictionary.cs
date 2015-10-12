using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ThunderEgg.BrownSugar {

    public static partial class SugarCollection {
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
    }
}
