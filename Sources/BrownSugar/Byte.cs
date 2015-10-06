using System;
using System.Runtime.InteropServices;

namespace ThunderEgg.BrownSugar {

    public static class ByteSugar {
        /// <summary>
        /// 型のサイズを返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int ByteSize<T>(this T t) {
            return Marshal.SizeOf(t);
        }

        public static long DoubleToInt64Bits(this double value) {
            return BitConverter.DoubleToInt64Bits(value);
        }

        public static double Int64BitsToDouble(this long value) {
            return BitConverter.Int64BitsToDouble(value);
        }
    }

    public static class ByteOrder {

        public static void AssignLittle(byte[] bytes, int index, sbyte val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
        }

        public static void AssignLittle(byte[] bytes, int index, short val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
        }

        public static void AssignLittle(byte[] bytes, int index, int val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
            bytes[index + 2] = unchecked((byte)(val >> 16));
            bytes[index + 3] = unchecked((byte)(val >> 24));
        }

        public static void AssignLittle(byte[] bytes, int index, long val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
            bytes[index + 2] = unchecked((byte)(val >> 16));
            bytes[index + 3] = unchecked((byte)(val >> 24));
            bytes[index + 4] = unchecked((byte)(val >> 32));
            bytes[index + 5] = unchecked((byte)(val >> 40));
            bytes[index + 6] = unchecked((byte)(val >> 48));
            bytes[index + 7] = unchecked((byte)(val >> 56));
        }

        public static void AssignLittle(byte[] bytes, int index, byte val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
        }

        public static void AssignLittle(byte[] bytes, int index, ushort val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
        }

        public static void AssignLittle(byte[] bytes, int index, uint val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
            bytes[index + 2] = unchecked((byte)(val >> 16));
            bytes[index + 3] = unchecked((byte)(val >> 24));
        }

        public static void AssignLittle(byte[] bytes, int index, ulong val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
            bytes[index + 1] = unchecked((byte)(val >> 8));
            bytes[index + 2] = unchecked((byte)(val >> 16));
            bytes[index + 3] = unchecked((byte)(val >> 24));
            bytes[index + 4] = unchecked((byte)(val >> 32));
            bytes[index + 5] = unchecked((byte)(val >> 40));
            bytes[index + 6] = unchecked((byte)(val >> 48));
            bytes[index + 7] = unchecked((byte)(val >> 56));
        }

        //
        //
        //

        public static void AssignBig(byte[] bytes, int index, sbyte val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, short val) {
            bytes[index + 0] = unchecked((byte)(val >> 8));
            bytes[index + 1] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, int val) {
            bytes[index + 0] = unchecked((byte)(val >> 24));
            bytes[index + 1] = unchecked((byte)(val >> 16));
            bytes[index + 2] = unchecked((byte)(val >> 8));
            bytes[index + 3] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, long val) {
            bytes[index + 0] = unchecked((byte)(val >> 56));
            bytes[index + 1] = unchecked((byte)(val >> 48));
            bytes[index + 2] = unchecked((byte)(val >> 40));
            bytes[index + 3] = unchecked((byte)(val >> 32));
            bytes[index + 4] = unchecked((byte)(val >> 24));
            bytes[index + 5] = unchecked((byte)(val >> 16));
            bytes[index + 6] = unchecked((byte)(val >> 8));
            bytes[index + 7] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, byte val) {
            bytes[index + 0] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, ushort val) {
            bytes[index + 0] = unchecked((byte)(val >> 8));
            bytes[index + 1] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, uint val) {
            bytes[index + 0] = unchecked((byte)(val >> 24));
            bytes[index + 1] = unchecked((byte)(val >> 16));
            bytes[index + 2] = unchecked((byte)(val >> 8));
            bytes[index + 3] = unchecked((byte)(val >> 0));
        }

        public static void AssignBig(byte[] bytes, int index, ulong val) {
            bytes[index + 0] = unchecked((byte)(val >> 56));
            bytes[index + 1] = unchecked((byte)(val >> 48));
            bytes[index + 2] = unchecked((byte)(val >> 40));
            bytes[index + 3] = unchecked((byte)(val >> 32));
            bytes[index + 4] = unchecked((byte)(val >> 24));
            bytes[index + 5] = unchecked((byte)(val >> 16));
            bytes[index + 6] = unchecked((byte)(val >> 8));
            bytes[index + 7] = unchecked((byte)(val >> 0));
        }

        //
        //
        //

        public static short ToInt16Big(byte[] bytes, int index) {
            var val = unchecked((short)(
                (ushort)bytes[index + 0] << 8 |
                (ushort)bytes[index + 1] << 0 |
                0));
            return val;
        }

        public static int ToInt32Big(byte[] bytes, int index) {
            var val = unchecked((int)(
                (uint)bytes[index + 0] << 24 |
                (uint)bytes[index + 1] << 16 |
                (uint)bytes[index + 2] << 8 |
                (uint)bytes[index + 3] << 0 |
                0));
            return val;
        }

        public static long ToInt64Big(byte[] bytes, int index) {
            var val = unchecked((long)(
                (ulong)bytes[index + 0] << 56 |
                (ulong)bytes[index + 1] << 48 |
                (ulong)bytes[index + 2] << 40 |
                (ulong)bytes[index + 3] << 32 |
                (ulong)bytes[index + 4] << 24 |
                (ulong)bytes[index + 5] << 16 |
                (ulong)bytes[index + 6] << 8 |
                (ulong)bytes[index + 7] << 0 |
                0));
            return val;
        }


        public static ushort ToUInt16Big(byte[] bytes, int index) {
            var val = unchecked((ushort)(
                (ushort)bytes[index + 0] << 8 |
                (ushort)bytes[index + 1] << 0 |
                0));
            return val;
        }

        public static ulong ToUInt32Big(byte[] bytes, int index) {
            var val = unchecked((uint)(
                (uint)bytes[index + 0] << 24 |
                (uint)bytes[index + 1] << 16 |
                (uint)bytes[index + 2] << 8 |
                (uint)bytes[index + 3] << 0 |
                0));
            return val;
        }

        public static ulong ToUInt64Big(byte[] bytes, int index) {
            var val = unchecked((ulong)(
                (ulong)bytes[index + 0] << 56 |
                (ulong)bytes[index + 1] << 48 |
                (ulong)bytes[index + 2] << 40 |
                (ulong)bytes[index + 3] << 32 |
                (ulong)bytes[index + 4] << 24 |
                (ulong)bytes[index + 5] << 16 |
                (ulong)bytes[index + 6] << 8 |
                (ulong)bytes[index + 7] << 0 |
                0));
            return val;
        }

        /*
         *
         */

        public static short ToInt16Little(byte[] bytes, int index) {
            var val = unchecked((short)(
                (ushort)bytes[index + 0] << 0 |
                (ushort)bytes[index + 1] << 8 |
                0));
            return val;
        }

        public static int ToInt32Little(byte[] bytes, int index) {
            var val = unchecked((int)(
                (uint)bytes[index + 0] << 0 |
                (uint)bytes[index + 1] << 8 |
                (uint)bytes[index + 2] << 16 |
                (uint)bytes[index + 3] << 24 |
                0));
            return val;
        }

        public static long ToInt64Little(byte[] bytes, int index) {
            var val = unchecked((long)(
                (ulong)bytes[index + 0] << 0 |
                (ulong)bytes[index + 1] << 8 |
                (ulong)bytes[index + 2] << 16 |
                (ulong)bytes[index + 3] << 24 |
                (ulong)bytes[index + 4] << 32 |
                (ulong)bytes[index + 5] << 40 |
                (ulong)bytes[index + 6] << 48 |
                (ulong)bytes[index + 7] << 56 |
                0));
            return val;
        }


        public static ushort ToUInt16Little(byte[] bytes, int index) {
            var val = unchecked((ushort)(
                (ushort)bytes[index + 0] << 0 |
                (ushort)bytes[index + 1] << 8 |
                0));
            return val;
        }

        public static ulong ToUInt32Little(byte[] bytes, int index) {
            var val = unchecked((uint)(
                (uint)bytes[index + 0] << 0 |
                (uint)bytes[index + 1] << 8 |
                (uint)bytes[index + 2] << 16 |
                (uint)bytes[index + 3] << 24 |
                0));
            return val;
        }

        public static ulong ToUInt64Little(byte[] bytes, int index) {
            var val = unchecked((ulong)(
                (ulong)bytes[index + 0] << 0 |
                (ulong)bytes[index + 1] << 8 |
                (ulong)bytes[index + 2] << 16 |
                (ulong)bytes[index + 3] << 24 |
                (ulong)bytes[index + 4] << 32 |
                (ulong)bytes[index + 5] << 40 |
                (ulong)bytes[index + 6] << 48 |
                (ulong)bytes[index + 7] << 56 |
                0));
            return val;
        }

        /*
         *
         */

    }
}
