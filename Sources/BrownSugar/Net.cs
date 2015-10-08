/*
 * @file
 * @Auther Yukio KANEDA
 */

using System;
using System.Net;
using System.Threading;

/*
 *
 */

namespace ThunderEgg.BrownSugar.Net {
    using Byte;

    public static class NetOrder {

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static short ToNetOrder(this short value) {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static int ToNetOrder(this int value) {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static long ToNetOrder(this long value) {
            return IPAddress.HostToNetworkOrder(value);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static ushort ToNetOrder(this ushort value) {
            return unchecked((ushort)(IPAddress.HostToNetworkOrder(unchecked((short)value))));
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static uint ToNetOrder(this uint value) {
            return unchecked((uint)(IPAddress.HostToNetworkOrder(unchecked((int)value))));
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="value">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static ulong ToNetOrder(this ulong value) {
            return unchecked((ulong)(IPAddress.HostToNetworkOrder(unchecked((long)value))));
        }

        /*
         *
         */

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static short ToHostOrder(this short value) {
            return IPAddress.NetworkToHostOrder(value);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static int ToHostOrder(this int value) {
            return IPAddress.NetworkToHostOrder(value);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static long ToHostOrder(this long value) {
            return IPAddress.NetworkToHostOrder(value);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static ushort ToHostOrder(this ushort value) {
            return unchecked((ushort)(IPAddress.NetworkToHostOrder(unchecked((short)value))));
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static uint ToHostOrder(this uint value) {
            return unchecked((uint)(IPAddress.NetworkToHostOrder(unchecked((int)value))));
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="value">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static ulong ToHostOrder(this ulong value) {
            return unchecked((ulong)(IPAddress.NetworkToHostOrder(unchecked((long)value))));
        }

        /*
         *
         */

        [Obsolete("Recommend bytes[N]=unchecked((byte)val);")]
        public static void Assign(byte[] bytes, int index, sbyte value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, short value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, int value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, long value) {
            BigEndian.Assign(bytes, index, value);
        }

        [Obsolete("Recommend bytes[N]=val;")]
        public static void Assign(byte[] bytes, int index, byte value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, ushort value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, uint value) {
            BigEndian.Assign(bytes, index, value);
        }

        public static void Assign(byte[] bytes, int index, ulong value) {
            BigEndian.Assign(bytes, index, value);
        }
        //
        //
        //

        public static short ToInt16(byte[] bytes, int index) {
            return BigEndian.ToInt16(bytes, index);
        }

        public static int ToInt32(byte[] bytes, int index) {
            return BigEndian.ToInt32(bytes, index);
        }
        public static long ToInt64(byte[] bytes, int index) {
            return BigEndian.ToInt64(bytes, index);
        }

        public static ushort ToUInt16(byte[] bytes, int index) {
            return BigEndian.ToUInt16(bytes, index);
        }

        public static ulong ToUInt32(byte[] bytes, int index) {
            return BigEndian.ToUInt32(bytes, index);
        }

        public static ulong ToUInt64(byte[] bytes, int index) {
            return BigEndian.ToUInt64(bytes, index);
        }

    }
}
