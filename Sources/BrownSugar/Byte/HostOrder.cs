/**
 * @file
 * @brief ホストオーダー順のバッファ操作
 */

namespace ThunderEgg.BrownSugar {

    /// <summary>アライメント済みのバッファをホストオーダー順で操作</summary>
    public class HostOrderAligned : NoByteOrder {

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ushort ToUInt16(byte* b) {
            return *(ushort*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe short ToInt16(byte* b) {
            return *(short*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte* b) {
            return *(char*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ushort ToUInt16(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(ushort*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe short ToInt16(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(short*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe char ToChar(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(char*)b;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe uint ToUInt32(byte* b) {
            return *(uint*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe int ToInt32(byte* b) {
            return *(int*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe float ToSingle(byte* b) {
            return *(float*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe uint ToUInt32(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(uint*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe int ToInt32(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(int*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe float ToSingle(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(float*)b;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ulong ToUInt64(byte* b) {
            return *(ulong*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe long ToInt64(byte* b) {
            return *(long*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte* b) {
            return *(double*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe ulong ToUInt64(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(ulong*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe long ToInt64(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(long*)b;
        }

        /// <summary>アライメント済みバッファ位置の値をホストオーダー順で取得</summary>
        public static unsafe double ToDouble(byte[] buffer, int index) {
            fixed (byte* b = &buffer[index])
            return *(double*)b;
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, ushort value) {
            *(ushort*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, short value) {
            *(short*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, char value) {
            *(char*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, ushort value) {
            fixed (byte* b = &buffer[index])
            *(ushort*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, short value) {
            fixed (byte* b = &buffer[index])
            *(short*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, char value) {
            fixed (byte* b = &buffer[index])
            *(char*)b = value
        }

        //
        //
        //

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, uint value) {
            *(uint*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, int value) {
            *(int*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte* b, float value) {
            *(float*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, uint value) {
            fixed (byte* b = &buffer[index])
            *(uint*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, int value) {
            fixed (byte* b = &buffer[index])
            *(int*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, float value) {
            fixed (byte* b = &buffer[index])
            *(float*)b = value;
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
            fixed (byte* b = &buffer[index])
            *(ulong*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, long value) {
            fixed (byte* b = &buffer[index])
            *(long*)b = value;
        }

        /// <summary>アライメント済みバッファ位置に値をホストオーダー順で書く</summary>
        public static unsafe void Assign(byte[] buffer, int index, double value) {
            fixed (byte* b = &buffer[index])
            *(double*)b = value;
        }

    }
}
