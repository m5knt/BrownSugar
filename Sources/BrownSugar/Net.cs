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

namespace BrownSugar.Net {

    public static class NetSugar {

        public static short ToNetOrder(this short val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        public static int ToNetOrder(this int val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        public static long ToNetOrder(this long val) {
            return IPAddress.HostToNetworkOrder(val);
        }

        public static ushort ToNetOrder(this ushort val) {
            return IPAddress.HostToNetworkOrder(val.UncheckedCast<short>()).
                UncheckedCast<ushort>();
        }

        public static uint ToNetOrder(this uint val) {
            return IPAddress.HostToNetworkOrder(val.UncheckedCast<int>()).
                UncheckedCast<uint>();
        }

        public static ulong ToNetOrder(this ulong val) {
            return IPAddress.HostToNetworkOrder(val.UncheckedCast<long>()).
                UncheckedCast<ulong>();
        }

        public static short ToHostOrder(this short val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        public static int ToHostOrder(this int val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        public static long ToHostOrder(this long val) {
            return IPAddress.NetworkToHostOrder(val);
        }

        public static ushort ToHostOrder(this ushort val) {
            return IPAddress.NetworkToHostOrder(val.UncheckedCast<short>()).
                UncheckedCast<ushort>();
        }

        public static uint ToHostOrder(this uint val) {
            return IPAddress.NetworkToHostOrder(val.UncheckedCast<int>()).
                UncheckedCast<uint>();
        }

        public static ulong ToHostOrder(this ulong val) {
            return IPAddress.NetworkToHostOrder(val.UncheckedCast<long>()).
                UncheckedCast<ulong>();
        }

        /*
         *
         */

        public static short ToInt16(this byte[] net, int index) {
            return BitConverter.ToInt16(net, index).ToHostOrder();
        }

        public static int ToInt32(this byte[] net, int index) {
            return BitConverter.ToInt32(net, index).ToHostOrder();
        }

        public static long ToInt64(this byte[] net, int index) {
            return BitConverter.ToInt64(net, index).ToHostOrder();
        }

        public static ushort ToUInt16(this byte[] net, int index) {
            return BitConverter.ToUInt16(net, index).ToHostOrder();
        }

        public static uint ToUInt32(this byte[] net, int index) {
            return BitConverter.ToUInt32(net, index).ToHostOrder();
        }

        public static ulong ToUInt64(this byte[] net, int index) {
            return BitConverter.ToUInt64(net, index).ToHostOrder();
        }

        public class ToFloatTemp {
            public byte[] Bytes = new byte[4];
        };

        static ThreadLocal<ToFloatTemp> ThreadLocalToFloatTemp = 
            new ThreadLocal<ToFloatTemp>();

        public static float ToFloat(this byte[] net, int index) {
            if (BitConverter.IsLittleEndian) {
                var tmp = ThreadLocalToFloatTemp.Value.Bytes;
                for (var i = 0; i < 4; ++i) {
                    tmp[4 - i] = net[index + i];
                }
                return BitConverter.ToSingle(tmp, 0);
            } else {
                return BitConverter.ToSingle(net, index);
            }
        }

        public static double ToDouble(this byte[] net, int index) {
            return BitConverter.ToInt64(net, index).ToHostOrder().From64BitsToDouble();
        }

        /*
         *
         */

        public static void CopyTo(this short val, byte[] net, int index) {
            var n = val.ToNetOrder();
            var count = sizeof(short);
            for(var i = 0; i < count; ++i) { 
                net[i] = n.UncheckedCast<byte>();
                n >>= 8;
            }
        }

        public static void CopyTo(this int val, byte[] net, int index) {
            var n = val.ToNetOrder();
            var count = sizeof(int);
            for (var i = 0; i < count; ++i) {
                net[i] = n.UncheckedCast<byte>();
                n >>= 8;
            }
        }

        public static void CopyTo(this long val, byte[] net, int index) {
            var n = val.ToNetOrder();
            var count = sizeof(long);
            for (var i = 0; i < count; ++i) {
                net[i] = n.UncheckedCast<byte>();
                n >>= 8;
            }
        }

        public static void CopyTo(this ushort val, byte[] net, int index) {
            val.UncheckedCast<short>().CopyTo(net, index);
        }

        public static void CopyTo(this uint val, byte[] net, int index) {
            val.UncheckedCast<int>().CopyTo(net, index);
        }

        public static void CopyTo(this ulong val, byte[] net, int index) {
            val.UncheckedCast<long>().CopyTo(net, index);
        }

        public static void CopyTo(this double val, byte[] net, int index) {
            val.To64Bits().CopyTo(net, index);
        }

    }

}
