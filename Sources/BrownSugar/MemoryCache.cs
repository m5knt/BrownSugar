/**
 * @file
 * @brief メモリーキャッシュ関係
 */

using System;
using System.Collections.Generic;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>メモリーキャッシュ関係</summary>
    public class MemoryQuiche<Key, Value> {

        Dictionary<Key, WeakReference> Weaks;

        public MemoryQuiche() {
            Weaks = new Dictionary<Key, WeakReference>();
        }

        public bool TryGetValue(Key key, ref Value value) {
            WeakReference r;
            if (Weaks.TryGetValue(key, out r)) {
                var n = r.Target;
                if (n != null && r.IsAlive) {
                    value = (Value)n;
                    return true;
                }
                Weaks.Remove(key);
                return false;
            }
            return false;
        }

#if false
        public bool LifeExtension(K key) {
        }

        public void Reload(K key) {

        }
#endif
    }
}

//
//
//

