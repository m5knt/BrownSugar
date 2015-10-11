/**
 * @file
 * @brief バイト・ビットに関係するシンタックスシュガーを纏めています
 */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class SwapSugar {

        // /// <summary>バイトオーダーを反転させた値を返す</summary>
        // public static byte SwapByteOrder(this byte value) { return value; }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static ushort SwapByteOrder(this ushort value) {
            return unchecked((ushort)( //
                (value & 0xff00) >> 8 |
                (value & 0x00ff) << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static uint SwapByteOrder(this uint value) {
            return ( //
                (value & 0xff000000U) >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                (value & 0x000000ffU) << 24);
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static ulong SwapByteOrder(this ulong value) {
            return ( //
                (value & 0xff00000000000000UL) >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                (value & 0x00000000000000ffUL) << 56);
        }

        // /// <summary>バイトオーダーを反転させた値を返す</summary>
        // public static sbyte SwapByteOrder(this sbyte value) { return value; }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static short SwapByteOrder(this short value) {
            return unchecked((short)( //
                (value & 0xff00) >> 8 |
                (value & 0x00ff) << 8));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static int SwapByteOrder(this int value) {
            return unchecked((int)( //
                (unchecked((uint)value) & (uint)0xff000000U) >> 24 |
                (unchecked((uint)value) & (uint)0x00ff0000U) >> 8 |
                (unchecked((uint)value) & (uint)0x0000ff00U) << 8 |
                (unchecked((uint)value) & (uint)0x000000ffU) << 24));
        }

        /// <summary>バイトオーダーを反転させた値を返す</summary>
        public static long SwapByteOrder(this long value) {
            return unchecked((long)( //
                (unchecked((ulong)value) & 0xff00000000000000UL) >> 56 |
                (unchecked((ulong)value) & 0x00ff000000000000UL) >> 40 |
                (unchecked((ulong)value) & 0x0000ff0000000000UL) >> 24 |
                (unchecked((ulong)value) & 0x000000ff00000000UL) >> 8 |
                (unchecked((ulong)value) & 0x00000000ff000000UL) << 8 |
                (unchecked((ulong)value) & 0x0000000000ff0000UL) << 24 |
                (unchecked((ulong)value) & 0x000000000000ff00UL) << 40 |
                (unchecked((ulong)value) & 0x00000000000000ffUL) << 56));
        }
    }
}
