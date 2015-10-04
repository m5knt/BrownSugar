using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BrownSugar {

    public static class DictionarySugar {

        public static void Update<TDict, TKey, TValue>(
            this TDict dict, TKey k, TValue v)
            where TDict : IDictionary<TKey, TValue> //
        {
            if (dict.ContainsKey(k)) {
                dict[k] = v;
            }
            else {
                dict.Add(k, v);
            }
        }
    }
}
