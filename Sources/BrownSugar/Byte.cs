using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;

namespace BrownSugar {

    public static class ByteSugar {

#if false
        // [StructLayout(LayoutKind.Sequential)]
        public static int ByteSize<T>(this T t) {
            return Marshal.SizeOf(t);
        }
#endif

        //
        //
        //

        public static Int16 ToInt16(this byte[] bytes, int index) {
            return BitConverter.ToInt16(bytes, index);
        }

        public static Int32 ToInt32(this byte[] bytes, int index) {
            return BitConverter.ToInt32(bytes, index);
        }

        public static UInt64 ToInt64(this byte[] bytes, int index) {
            return BitConverter.ToUInt64(bytes, index);
        }

        //
        //
        //

        public static UInt16 ToUInt16(this byte[] bytes, int index) {
            return BitConverter.ToUInt16(bytes, index);
        }

        public static UInt32 ToUInt32(this byte[] bytes, int index) {
            return BitConverter.ToUInt32(bytes, index);
        }

        public static UInt64 ToUInt64(this byte[] bytes, int index) {
            return BitConverter.ToUInt64(bytes, index);
        }

        //
        //
        //

        public static float ToFloat(this byte[] value, int index) {
            return BitConverter.ToSingle(value, index);
        }

        public static double ToDouble(this byte[] value, int index) {
            return BitConverter.ToDouble(value, index);
        }

        //
        //
        //

    }
}
