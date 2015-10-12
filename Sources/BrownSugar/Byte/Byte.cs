/**
 * @file
 * @brief バイト・ビットに関係するシンタックスシュガーを纏めています
 */

using System;
using System.Runtime.InteropServices;

/*
 *
 */

namespace ThunderEgg.BrownSugar {

    public static class SugarByte {

        /// <summary>倍精度値をビットイメージとして収納します</summary>
        public static long DoubleToInt64Bits(this double value) {
            return BitConverter.DoubleToInt64Bits(value);
        }

        /// <summary>倍精度値をビットイメージから復元します</summary>
        public static double Int64BitsToDouble(this long value) {
            return BitConverter.Int64BitsToDouble(value);
        }
    }

    //
    //
    //

    public static class HostOrder {

        /// <summary>
        /// 型のサイズを返す
        /// <see cref="https://msdn.microsoft.com/ja-jp/library/system.runtime.interopservices.layoutkind(v=vs.90).aspx">バイナリレイアウトの説明</see>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ByteSize(this object obj) {
            return Marshal.SizeOf(obj);
        }

        /// <summary>
        /// ホストオーダーでオブジェクトをバッファ書き込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファのインデックス</param>
        /// <param name="source">オブジェクト</param>
        public static void Assign(byte[] buffer, int index, object marshal) {
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.StructureToPtr(marshal, new IntPtr(fix + index), false);
                }
            }
        }

        /// <summary>
        /// ホストオーダーでオブジェクト読み込み
        /// </summary>
        /// <param name="buffer">バッファ</param>
        /// <param name="index">バッファのインデックス</param>
        /// <param name="to">書き込み先</param>
        public static void CopyTo(byte[] buffer, int index, object marshal) {
            unsafe
            {
                fixed (byte* fix = buffer)
                {
                    Marshal.PtrToStructure(new IntPtr(fix + index), marshal);
                }
            }
        }
    }
}
