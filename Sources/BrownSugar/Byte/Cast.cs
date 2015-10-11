﻿/**
 * @file
 * @brief キャスト回り
 */

namespace ThunderEgg.BrownSugar {

    public static class CastSugar {

        // 同じ型なので不要
        // public static byte CastUInt8(this byte value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this ushort value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this uint value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this ulong value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this sbyte value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this short value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this int value) { return unchecked((byte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static byte CastUInt8(this long value) { return unchecked((byte)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static ushort ToUInt16(this byte value) { return unchecked(/*(ushort)*/value); }
        // 同じ型なので不要
        // public static ushort CastUInt16(this ushort value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this uint value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this ulong value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this sbyte value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this short value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this int value) { return unchecked((ushort)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ushort CastUInt16(this long value) { return unchecked((ushort)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static uint ToUInt32(this byte value) { return unchecked(/*(uint)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static uint ToUInt32(this ushort value) { return unchecked(/*(uint)*/value); }
        // 同じ型なので不要
        // public static uint CastUInt32(this uint value) { return unchecked((uint)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static uint CastUInt32(this ulong value) { return unchecked((uint)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static uint CastUInt32(this sbyte value) { return unchecked((uint)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static uint CastUInt32(this short value) { return unchecked((uint)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static uint CastUInt32(this int value) { return unchecked((uint)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static uint CastUInt32(this long value) { return unchecked((uint)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static ulong ToUInt64(this byte value) { return unchecked(/*(ulong)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static ulong ToUInt64(this ushort value) { return unchecked(/*(ulong)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static ulong ToUInt64(this uint value) { return unchecked(/*(ulong)*/value); }
        // 同じ型なので不要
        // public static ulong CastUInt64(this ulong value) { return unchecked((ulong)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ulong CastUInt64(this sbyte value) { return unchecked((ulong)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ulong CastUInt64(this short value) { return unchecked((ulong)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ulong CastUInt64(this int value) { return unchecked((ulong)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static ulong CastUInt64(this long value) { return unchecked((ulong)value); }

    }

    //
    //
    //

    public static partial class Sugar {

        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this byte value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this ushort value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this uint value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this ulong value) { return unchecked((sbyte)value); }
        // 同じ型なので不要
        // public static sbyte CastInt8(this sbyte value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this short value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this int value) { return unchecked((sbyte)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static sbyte CastInt8(this long value) { return unchecked((sbyte)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static short ToInt16(this byte value) { return unchecked(/*(short)*/value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static short CastInt16(this ushort value) { return unchecked((short)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static short CastInt16(this uint value) { return unchecked((short)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static short CastInt16(this ulong value) { return unchecked((short)value); }
        /// <summary>型昇格した値を返します</summary>
        public static short ToInt16(this sbyte value) { return unchecked(/*(short)*/value); }
        // 同じ型なので不要
        // public static short CastInt16(this short value) { return unchecked((short)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static short CastInt16(this int value) { return unchecked((short)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static short CastInt16(this long value) { return unchecked((short)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static int ToInt32(this byte value) { return unchecked(/*(int)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static int ToInt32(this ushort value) { return unchecked(/*(int)*/value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static int CastInt32(this uint value) { return unchecked((int)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static int CastInt32(this ulong value) { return unchecked((int)value); }
        /// <summary>型昇格した値を返します</summary>
        public static int ToInt32(this sbyte value) { return unchecked(/*(int)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static int ToInt32(this short value) { return unchecked(/*(int)*/value); }
        // 同じ型なので不要
        // public static int CastInt32(this int value) { return unchecked((int)value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static int CastInt32(this long value) { return unchecked((int)value); }

        //
        //
        //

        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this byte value) { return unchecked(/*(long)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this ushort value) { return unchecked(/*(long)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this uint value) { return unchecked(/*(long)*/value); }
        /// <summary>アンセーフキャストをした値を返します</summary>
        public static long CastInt64(this ulong value) { return unchecked((long)value); }
        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this sbyte value) { return unchecked(/*(long)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this short value) { return unchecked(/*(long)*/value); }
        /// <summary>型昇格した値を返します</summary>
        public static long ToInt64(this int value) { return unchecked(/*(long)*/value); }
        // 同じ型なので不要
        //public static long CastInt64(this long value) { return unchecked((long)value); }
    }
}
