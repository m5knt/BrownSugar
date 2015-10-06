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

    public static class NetOrder {

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static short ToNetOrder(this short val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static int ToNetOrder(this int val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static long ToNetOrder(this long val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static ushort ToNetOrder(this ushort val) {
            return unchecked((ushort)(IPAddress.HostToNetworkOrder(unchecked((short)val))));
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static uint ToNetOrder(this uint val) {
            return unchecked((uint)(IPAddress.HostToNetworkOrder(unchecked((int)val))));
        }

        /// <summary>
        /// ホストオーダー値からネットオーダー値にする
        /// </summary>
        /// <param name="val">ホストオーダー値</param>
        /// <returns>ネットオーダー値</returns>
        public static ulong ToNetOrder(this ulong val) {
            return unchecked((ulong)(IPAddress.HostToNetworkOrder(unchecked((long)val))));
        }

        /*
         *
         */

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static short ToHostOrder(this short val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static int ToHostOrder(this int val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static long ToHostOrder(this long val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static ushort ToHostOrder(this ushort val) {
            return unchecked((ushort)(IPAddress.NetworkToHostOrder(unchecked((short)val))));
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static uint ToHostOrder(this uint val) {
            return unchecked((uint)(IPAddress.NetworkToHostOrder(unchecked((int)val))));
        }

        /// <summary>
        /// ネットオーダー値からホストオーダー値にする
        /// </summary>
        /// <param name="val">ネットオーダー値</param>
        /// <returns>ホストオーダー値</returns>
        public static ulong ToHostOrder(this ulong val) {
            return unchecked((ulong)(IPAddress.NetworkToHostOrder(unchecked((long)val))));
        }

        /*
         *
         */

        public static void Assign(byte[] bytes, int index, sbyte val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, short val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, int val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, long val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, byte val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, ushort val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, uint val) {
            ByteOrder.AssignBig(bytes, index, val);
        }

        public static void Assign(byte[] bytes, int index, ulong val) {
            ByteOrder.AssignBig(bytes, index, val);
        }
        //
        //
        //

        public static short ToInt16(byte[] bytes, int index) {
            return ByteOrder.ToInt16Big(bytes, index);
        }

        public static int ToInt32(byte[] bytes, int index) {
            return ByteOrder.ToInt32Big(bytes, index);
        }
        public static long ToInt64(byte[] bytes, int index) {
            return ByteOrder.ToInt64Big(bytes, index);
        }

        public static ushort ToUInt16(byte[] bytes, int index) {
            return ByteOrder.ToUInt16Big(bytes, index);
        }

        public static ulong ToUInt32(byte[] bytes, int index) {
            return ByteOrder.ToUInt32Big(bytes, index);
        }

        public static ulong ToUInt64(byte[] bytes, int index) {
            return ByteOrder.ToUInt64Big(bytes, index);
        }

    }
}
