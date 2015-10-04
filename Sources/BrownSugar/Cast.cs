/*
 * @file
 * @Auther Yukio KANEDA
 */

using System;

namespace BrownSugar {

    public static class CastSugar {

        public static T Cast<T>(this Object e) {
            return (T)e;
        }

        public static T UncheckedCast<T>(this Object e) {
            return unchecked((T)e);
        }

        public static T CheckedCast<T>(this Object e) {
            return unchecked((T)e);
        }
    }
}
