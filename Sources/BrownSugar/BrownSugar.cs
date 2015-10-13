/*
 * @file
 * @Auther Yukio KANEDA
 */

using System;
using System.Net;

/*
 *
 */

/// <summary>
/// シュガーを纏めています
/// </summary>
namespace ThunderEgg.BrownSugar {

    public static class Always {
        /// <summary>常に真</summary>
        public static bool True { get { return true; } }
        /// <summary>常に偽</summary>
        public static bool False { get { return false; } }
    }

    //
    //
    //

    public class BrownSugar {

        public long Bench(int limit, Action<long> action) {
            var now = DateTime.Now;
            var count = 0L;
            while ((DateTime.Now - now).TotalSeconds < limit) {
                action(count);
                ++count;
            }
            Console.WriteLine(action.Method.DeclaringType.ToString() + ":" + count.ToString());
            return count;
        }

        public static void Main() {
            Console.WriteLine(sizeof(bool));
            var self = new BrownSugar();
            var buffer = new byte[256];
            var array = self.Bench(10, (i) => {
                NetOrder.Assign(buffer, 0, i.CastUInt32());
            });
            unsafe {
                fixed (byte* p = buffer) {
                    var pp = p;
                    var ptr = self.Bench(10, (i) => {
                        NetOrder.Assign(pp, i.CastUInt32());
                    });
                }
            }
        }
    }
}

