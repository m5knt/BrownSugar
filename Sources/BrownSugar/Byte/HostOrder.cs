/**
 * @file
 * @brief ホストオーダー順のバッファ操作
 */

using System;
using System.Runtime.InteropServices;

//
//
//

namespace ThunderEgg.BrownSugar.Byte {

    /// <summary>バッファをホストオーダー順で操作</summary>
    public class HostOrderAligned {

        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe byte ToUInt8(byte* b) {
            return *b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe sbyte ToInt8(byte* b) {
            return *(sbyte*)b;
        }
        /// <summary>ポインタ位置の値を取得/遅いです</summary>
        public static unsafe bool ToBoolean(byte* b) {
            return *(bool*)b;
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static byte ToUInt8(byte[] buffer, int index) {
            return buffer[index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(byte[] buffer, int index) {
            return unchecked((sbyte)buffer[index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(byte[] buffer, int index) {
            return buffer[index] != 0;
        }

        /// <summary>byte/遅いです</summary>
        public static byte ToUInt8(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index];
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static sbyte ToInt8(ArraySegment<byte> buffer, int index) {
            return unchecked((sbyte)buffer.Array[buffer.Offset + index]);
        }

        /// <summary>バッファ内の値を取得/遅いです</summary>
        public static bool ToBoolean(ArraySegment<byte> buffer, int index) {
            return buffer.Array[buffer.Offset + index] != 0;
        }

        //
        //
        //

        /// <summary>ポインタ位置に値を書/遅いですく</summary>
        public static unsafe void Assign(byte* buffer, byte value) {
            *buffer = value;
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, sbyte value) {
            *buffer = unchecked((byte)value);
        }

        /// <summary>ポインタ位置に値を書く/遅いです</summary>
        public static unsafe void Assign(byte* buffer, bool value) {
            *buffer = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, byte value) {
            buffer[index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, sbyte value) {
            buffer[index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(byte[] buffer, int index, bool value) {
            buffer[index] = value ? (byte)1 : (byte)0;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, byte value) {
            buffer.Array[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, sbyte value) {
            buffer.Array[buffer.Offset + index] = unchecked((byte)value);
        }

        /// <summary>バッファ位置に値を書く/遅いです</summary>
        public static void Assign(ArraySegment<byte> buffer, int index, bool value) {
            buffer.Array[buffer.Offset + index] = value ? (byte)1 : (byte)0;
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

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt16 ToUInt16(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(UInt16*)&p[buffer.Offset + index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int16 ToInt16(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(Int16*)&p[buffer.Offset + index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Char ToChar(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(Char*)&p[buffer.Offset + index];
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

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt32 ToUInt32(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(UInt32*)&p[buffer.Offset + index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int32 ToInt32(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(Int32*)&p[buffer.Offset + index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Single ToSingle(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(Single*)&p[buffer.Offset + index];
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

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe UInt64 ToUInt64(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(UInt64*)&p[buffer.Offset + index];
        }

        /// <summary>バッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe Int64 ToInt64(ArraySegment<byte> buffer, int index) {
            fixed (byte* p = buffer.Array) return *(Int64*)&p[buffer.Offset + index];
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
            fixed (byte* p = buffer) *(UInt16*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int16 value) {
            fixed (byte* p = buffer) *(Int16*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, char value) {
            fixed (byte* p = buffer) *(char*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, UInt16 value) {
            fixed (byte* p = buffer.Array) *(UInt16*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, Int16 value) {
            fixed (byte* p = buffer.Array) *(Int16*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, char value) {
            fixed (byte* p = buffer.Array) *(char*)&p[buffer.Offset + index] = value;
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
            fixed (byte* p = buffer) *(UInt32*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int32 value) {
            fixed (byte* p = buffer) *(Int32*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Single value) {
            fixed (byte* p = buffer) *(Single*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, UInt32 value) {
            fixed (byte* p = buffer.Array) *(UInt32*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, Int32 value) {
            fixed (byte* p = buffer.Array) *(Int32*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, Single value) {
            fixed (byte* p = buffer.Array) *(Single*)&p[buffer.Offset + index] = value;
        }

        //
        //
        //

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, UInt64 value) {
            *(UInt64*)b = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, Int64 value) {
            *(Int64*)b = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, Double value) {
            *(Double*)b = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, UInt64 value) {
            fixed (byte* p = buffer) *(UInt64*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Int64 value) {
            fixed (byte* p = buffer) *(Int64*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, Double value) {
            fixed (byte* p = buffer) *(Double*)&p[index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, UInt64 value) {
            fixed (byte* p = buffer.Array) *(UInt64*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, Int64 value) {
            fixed (byte* p = buffer.Array) *(Int64*)&p[buffer.Offset + index] = value;
        }

        /// <summary>バッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(ArraySegment<byte> buffer, int index, Double value) {
            fixed (byte* p = buffer.Array) *(Double*)&p[buffer.Offset + index] = value;
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
