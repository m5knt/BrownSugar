/**
 * @file
 * @brief ホストオーダー順のバッファ操作
 */

using System;
using System.Runtime.InteropServices;

//
//
//

namespace ThunderEgg.BrownSugar {

    /// <summary>アライメント済みのバッファをホストオーダー順で操作</summary>
    public class HostOrderAligned : OneByte {

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ushort ToUInt16(byte* buffer) {
            return *(ushort*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe short ToInt16(byte* buffer) {
            return *(short*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte* buffer) {
            return *(char*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ushort ToUInt16(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(ushort*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe short ToInt16(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(short*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(char*)p;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe uint ToUInt32(byte* buffer) {
            return *(uint*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe int ToInt32(byte* buffer) {
            return *(int*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe float ToSingle(byte* buffer) {
            return *(float*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe uint ToUInt32(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(uint*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe int ToInt32(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(int*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(float*)p;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ulong ToUInt64(byte* buffer) {
            return *(ulong*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe long ToInt64(byte* buffer) {
            return *(long*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte* buffer) {
            return *(double*)buffer;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(ulong*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(long*)p;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* p = &buffer[index]) return *(double*)p;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, ushort value) {
            *(ushort*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, short value) {
            *(short*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, char value) {
            *(char*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, ushort value) {
            fixed (byte* p = &buffer[index]) *(ushort*)p = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, short value) {
            fixed (byte* p = &buffer[index]) *(short*)p = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, char value) {
            fixed (byte* p = &buffer[index]) *(char*)p = value;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, uint value) {
            *(uint*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, int value) {
            *(int*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* buffer, float value) {
            *(float*)buffer = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, uint value) {
            fixed (byte* b = &buffer[index]) *(uint*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, int value) {
            fixed (byte* b = &buffer[index]) *(int*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, float value) {
            fixed (byte* b = &buffer[index]) *(float*)b = value;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, ulong value) {
            *(ulong*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, long value) {
            *(long*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, double value) {
            *(double*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, ulong value) {
            fixed (byte* p = &buffer[index]) *(ulong*)p = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, long value) {
            fixed (byte* p = &buffer[index]) *(long*)p = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, double value) {
            fixed (byte* b = &buffer[index]) *(double*)b = value;
        }
    }

    //
    //
    //

    public class HostOrder {

        public static int LittleEndian = 0; // 0 リトル

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
        public static void To<T>(byte[] buffer, int index, T obj)
            where T : class {
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
