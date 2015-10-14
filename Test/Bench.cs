using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;

namespace Test {

    public static class Bench {
        public static long Run(double limit, Action<long> action) {
            var now = DateTime.Now;
            var count = 0L;
            while ((DateTime.Now - now).TotalSeconds < limit) {
                action(count);
                ++count;
            }
            Console.WriteLine(action.Method.DeclaringType.ToString() + ":" + count.ToString());
            return count;
        }
    }
}
