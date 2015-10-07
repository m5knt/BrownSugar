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
			return double.IsInfinity(f);
        }

        public static bool IsNaN(this double f) {
			return double.IsNaN(f);
        }

		public static bool IsNegativeInfinity(this double f) {
			return double.IsNegativeInfinity(f);
        }

		public static bool IsPositiveInfinity(this double f) {
			return double.IsPositiveInfinity(f);
        }
    }
}
