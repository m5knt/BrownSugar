using System;

namespace Test {

    public static class Bench {
        public static long Run(double limit, Action<long> action) {
            var now = DateTime.Now;
            var count = 0L;
            while ((DateTime.Now - now).TotalSeconds < limit) {
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                action(count);
                count += 10;
            }
            return count;
        }
    }
}
