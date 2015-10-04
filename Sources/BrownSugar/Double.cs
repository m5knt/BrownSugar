using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;

//
//
//

namespace BrownSugar {

    public static class DoubleSugar {

        public static bool IsInfinity(this double f) {
            return Double.IsInfinity(f);
        }

        public static bool IsNaN(this Double f) {
            return Double.IsNaN(f);
        }

        public static bool IsNegativeInfinity(this Double f) {
            return Double.IsNegativeInfinity(f);
        }

        public static bool IsPositiveInfinity(this Double f) {
            return Double.IsPositiveInfinity(f);
        }

        public static long To64Bits(this double val) {
            return BitConverter.DoubleToInt64Bits(val);
        }
    }
}
