﻿/**
 * @file
 * @brief ホストオーダー順のバッファ操作
 */

using System;
using System.Runtime.InteropServices;

//
//
//

namespace ThunderEgg.BrownSugar {

    /// <summary>バッファをホストオーダー順で操作</summary>
    public class HostOrderAligned : NoOrder {

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
        }

        //
        //
        //

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt16 ToUInt16(byte* buffer) {
            return *(ushort*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int16 ToInt16(byte* buffer) {
            return *(short*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte* buffer) {
            return *(char*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt16 ToUInt16(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(UInt16*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int16 ToInt16(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(Int16*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(char*)&p[index];
        }

        //
        //
        //

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt32 ToUInt32(byte* buffer) {
            return *(UInt32*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int32 ToInt32(byte* buffer) {
            return *(Int32*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Single ToSingle(byte* buffer) {
            return *(Single*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt32 ToUInt32(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(UInt32*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int32 ToInt32(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(Int32*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Single ToSingle(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(Single*)&p[index];
        }

        //
        //
        //

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ulong ToUInt64(byte* buffer) {
            return *(ulong*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe long ToInt64(byte* buffer) {
            return *(long*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte* buffer) {
            return *(double*)buffer;
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt64 ToUInt64(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(UInt64*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int64 ToInt64(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(Int64*)&p[index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* p = buffer) return *(double*)&p[index];
        }

        //
        //
        //

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, UInt16 value) {
            *(UInt16*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, Int16 value) {
            *(Int16*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            *(char*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, UInt16 value) {
            fixed (byte* fix = buffer) *(UInt16*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int16 value) {
            fixed (byte* fix = buffer) *(Int16*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, char value) {
            fixed (byte* fix = buffer) *(char*)&fix[index] = value;
        }

        //
        //
        //

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, UInt32 value) {
            *(UInt32*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, Int32 value) {
            *(Int32*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, Single value) {
            *(Single*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, UInt32 value) {
            fixed (byte* fix = buffer) *(UInt32*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int32 value) {
            fixed (byte* fix = buffer) *(Int32*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Single value) {
            fixed (byte* fix = buffer) *(Single*)&fix[index] = value;
        }

        //
        //
        //

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, UInt64 value) {
            *(UInt64*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, Int64 value) {
            *(Int64*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, Double value) {
            *(Double*)buffer = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, UInt64 value) {
            fixed (byte* fix = buffer) *(UInt64*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int64 value) {
            fixed (byte* fix = buffer) *(Int64*)&fix[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Double value) {
            fixed (byte* fix = buffer) *(Double*)&fix[index] = value;
        }

    }

    //
    //
    //

    public partial class HostOrder {

        /// <summary>オブジェクトをバイナリ化しバッファへ書き込む</summary>
        public static void Assign<T>(byte[] buffer, int index, T obj) {
            int length = Marshal.SizeOf(typeof(T));
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = &buffer[index])
                {
                    Marshal.StructureToPtr(obj, new IntPtr(fix), false);
                }
            }
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        public static byte[] GetBytes<T>(T obj) {
            int length = Marshal.SizeOf(typeof(T));
            var buffer = new byte[length];
            Assign(buffer, 0, obj);
            return buffer;
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        public static void CopyTo<T>(byte[] buffer, int index, T obj)
            where T : class //
        {
            var type = typeof(T);
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = &buffer[index])
                {
                    Marshal.PtrToStructure(new IntPtr(fix), obj);
                }
            }
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        public static T To<T>(byte[] buffer, int index) {
            var type = typeof(T);
            int length = Marshal.SizeOf(type);
            if (index < 0 || (index + length) > buffer.Length) {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (byte* fix = &buffer[index])
                {
                    return (T)Marshal.PtrToStructure(new IntPtr(fix), type);
                }
            }
        }
    }

}