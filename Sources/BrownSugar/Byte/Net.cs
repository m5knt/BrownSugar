/*
 * @file
 * @Auther Yukio KANEDA
 */

using System;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class SugarNet {

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static ushort ToNetOrder(this ushort value) {
            return BitConverter.IsLittleEndian ?
                unchecked((ushort)( //
                value >> 8 |
                value << 8)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static uint ToNetOrder(this uint value) {
            return BitConverter.IsLittleEndian ?
                (value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static ulong ToNetOrder(this ulong value) {
            return BitConverter.IsLittleEndian ?
                (value >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                value << 56) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static short ToNetOrder(this short value) {
            return BitConverter.IsLittleEndian ?
                unchecked((short)( //
                unchecked((ushort)value) >> 8 |
                value << 8)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static int ToNetOrder(this int value) {
            return BitConverter.IsLittleEndian ?
                unchecked((int)( //
                unchecked((uint)value) >> 24 |
                (unchecked((uint)value) & 0x00ff0000U) >> 8 |
                (unchecked((uint)value) & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24)) : value;
        }

        /// <summary>ホストオーダー値からネットオーダー値にする</summary>
        public static long ToNetOrder(this long value) {
            return BitConverter.IsLittleEndian ?
                unchecked((long)( //
                unchecked((ulong)value) >> 56 |
                (unchecked((ulong)value) & 0x00ff000000000000UL) >> 40 |
                (unchecked((ulong)value) & 0x0000ff0000000000UL) >> 24 |
                (unchecked((ulong)value) & 0x000000ff00000000UL) >> 8 |
                (unchecked((ulong)value) & 0x00000000ff000000UL) << 8 |
                (unchecked((ulong)value) & 0x0000000000ff0000UL) << 24 |
                (unchecked((ulong)value) & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56)) : value;
        }

        //
        //
        //

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static ushort ToHostOrder(this ushort value) {
            return BitConverter.IsLittleEndian ? //
                unchecked((ushort)( //
                value >> 8 |
                value << 8)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static uint ToHostOrder(this uint value) {
            return BitConverter.IsLittleEndian ? //
                (value >> 24 |
                (value & 0x00ff0000U) >> 8 |
                (value & 0x0000ff00U) << 8 |
                value << 24) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static ulong ToHostOrder(this ulong value) {
            return BitConverter.IsLittleEndian ? //
                (value >> 56 |
                (value & 0x00ff000000000000UL) >> 40 |
                (value & 0x0000ff0000000000UL) >> 24 |
                (value & 0x000000ff00000000UL) >> 8 |
                (value & 0x00000000ff000000UL) << 8 |
                (value & 0x0000000000ff0000UL) << 24 |
                (value & 0x000000000000ff00UL) << 40 |
                value << 56) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static short ToHostOrder(this short value) {
            return BitConverter.IsLittleEndian ?
                unchecked((short)( //
                unchecked((ushort)value) >> 8 |
                value << 8)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static int ToHostOrder(this int value) {
            return BitConverter.IsLittleEndian ?
                unchecked((int)( //
                unchecked((uint)value) >> 24 |
                unchecked((uint)value & 0x00ff0000U) >> 8 |
                unchecked((uint)value & 0x0000ff00U) << 8 |
                unchecked((uint)value) << 24)) : value;
        }

        /// <summary>ネットオーダー値からホストオーダー値にする</summary>
        public static long ToHostOrder(this long value) {
            return BitConverter.IsLittleEndian ?
                unchecked((long)( //
                unchecked((ulong)value) >> 56 |
                unchecked((ulong)value & 0x00ff000000000000UL) >> 40 |
                unchecked((ulong)value & 0x0000ff0000000000UL) >> 24 |
                unchecked((ulong)value & 0x000000ff00000000UL) >> 8 |
                unchecked((ulong)value & 0x00000000ff000000UL) << 8 |
                unchecked((ulong)value & 0x0000000000ff0000UL) << 24 |
                unchecked((ulong)value & 0x000000000000ff00UL) << 40 |
                unchecked((ulong)value) << 56)) : value;
        }

    }
}
