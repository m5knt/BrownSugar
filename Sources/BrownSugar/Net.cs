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

    public static class ByteOrder {

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

        public static short ToHostInt16(byte[] net, int index) {
            return BitConverter.ToInt16(net, index).ToHostOrder();
        }

        public static int ToHostInt32(byte[] net, int index) {
            return BitConverter.ToInt32(net, index).ToHostOrder();
        }

        public static long ToHostInt64(byte[] net, int index) {
            return BitConverter.ToInt64(net, index).ToHostOrder();
        }

        public static ushort ToHostUInt16(byte[] net, int index) {
            return BitConverter.ToUInt16(net, index).ToHostOrder();
        }

        public static uint ToHostUInt32(byte[] net, int index) {
            return BitConverter.ToUInt32(net, index).ToHostOrder();
        }

        public static ulong ToHostUInt64(byte[] net, int index) {
            return BitConverter.ToUInt64(net, index).ToHostOrder();
        }

        public static double ToHostDouble(byte[] net, int index) {
            var n = BitConverter.ToInt64(net, index).ToHostOrder();
            return BitConverter.Int64BitsToDouble(n);
        }

        /*
				 *
				 */

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this short value, byte* p) {
            var q = p + sizeof(short);
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value);
            return q;
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this int value, byte* p) {
            var q = p + sizeof(int);
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value);
            return q;
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this long value, byte* p) {
            var q = p + sizeof(long);
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value); value >>= 8;
            *--p = unchecked((byte)value);
            return q;
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this ushort value, byte* p) {
            return CopyToNetOrder(unchecked((short)value), p);
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this uint value, byte* p) {
            return CopyToNetOrder(unchecked((int)value), p);
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this ulong value, byte* p) {
            return CopyToNetOrder(unchecked((long)value), p);
        }

        /// <summary>
        /// 値をビッグエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="p">書き出し位置</param>
        /// <returns>次の書き出し位置</returns>
        public unsafe static byte* CopyToNetOrder(this double value, byte* p) {
            var n = BitConverter.DoubleToInt64Bits(value);
            return CopyToNetOrder(n, p);
        }

        /*
         *
         */

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this short value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this int value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this long value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this ushort value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this uint value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this ulong value, byte[] buffer, int index) {
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(value, fix); }
        }

        /// <summary>
        /// 値をネットワークエンディアン順で書き出す
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="buffer">書き出しバッファ</param>
        /// <param name="index">書き出しバッファ位置</param>
        public static void CopyToNetOrder(this double value, byte[] buffer, int index) {
            var n = BitConverter.DoubleToInt64Bits(value);
            unsafe { fixed (byte* fix = &buffer[index]) CopyToNetOrder(n, fix); }
        }
    }
}
