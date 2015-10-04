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

    public static class LongSugar {

        public static double From64BitsToDouble(this long val) {
            return BitConverter.Int64BitsToDouble(val);
        }
    }
}
