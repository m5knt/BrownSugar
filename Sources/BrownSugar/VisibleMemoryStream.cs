/**
* @file
* @brief 内部バッファ参照可能なメモリーストリーム
*/

using System;
using System.IO;

namespace ThunderEgg.BrownSugar {

    /// <summary>内部バッファ参照可能なメモリーストリーム</summary>
    public class VisibleMemoryStream : Stream {

        byte[] VisibleBuffer;
        long Pos;
        long Len;

        const int Default = 4096;
        Func<long, long> Expander = _ => _ * 2;

        /// <summary>コンストラクタ</summary>
        public VisibleMemoryStream(Func<long, long> expander = null) //
            : this(Default, expander) //
        {
        }

        /// <summary>コンストラクタ</summary>
        public VisibleMemoryStream(long capacity, Func<long, long> expander = null) {
            capacity = Math.Max(capacity, 1);
            VisibleBuffer = new byte[capacity];
            Pos = 0;
            Len = 0;
            if (expander != null) {
                Expander = expander;
            }
        }

        /// <summary>読み込み操作可能か</summary>
        public override bool CanRead { get { return false; } }
        /// <summary>シーク可能か</summary>
        public override bool CanSeek { get { return true; } }
        /// <summary>タイムアウト可能化か</summary>
        public override bool CanTimeout { get { return false; } }
        /// <summary>書き込み操作可能か</summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>ストリーム長</summary>
        public override long Length {
            get { return Len; }
        }

        /// <summary>読み書き位置</summary>
        public override long Position {
            get { return Pos; }
            set {
                if (value < 0 || value >= VisibleBuffer.Length) {
                    throw new Exception();
                }
                Pos = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            throw new NotSupportedException();
        }

        /// <summary>シーク指定</summary>
        public override long Seek(long offset, SeekOrigin origin) {
            long t;
            switch (origin) {
                default:
                case SeekOrigin.Begin: t = offset; break;
                case SeekOrigin.Current: t = Pos + offset; break;
                case SeekOrigin.End: t = Len + offset; break;
            }
            if (t < 0 || t >= Len) {
                throw new Exception();
            }
            Pos = t;
            return Pos;
        }

        /// <summary>ストリーム長を設定する</summary>
        public override void SetLength(long value) {
            AutoReallocBuffer(value);
            Pos = value;
            Len = value;
        }

        /// <summary>ストリームへデータを書き込む</summary>
        public override void Write(byte[] buffer, int offset, int count) {
            AutoReallocBuffer(Pos + count);
            System.Buffer.BlockCopy(buffer, offset, VisibleBuffer, (int)Pos, count);
            Pos += count;
            Len = Math.Max(Len, Pos);
        }

        /// <summary>ストリームへデータを書き込む</summary>
        public override void WriteByte(byte value) {
            AutoReallocBuffer(Pos);
            VisibleBuffer[Pos++] = value;
            Len = Math.Max(Len, Pos);
        }

        /// <summary>バッファサイズを調整する</summary>
        void AutoReallocBuffer(long n) {
            var len = VisibleBuffer.LongLength;
            if (len > n) {
                return;
            }
            while (len < n) len = Expander(len);
            var expand = new byte[len];
            VisibleBuffer.CopyTo(expand, 0);
            VisibleBuffer = expand;
        }

        /// <summary>
        /// 一時バッファを書き出す・バッファリングはしていないので何もしない
        /// </summary>
        public override void Flush() {        
        }

        /// <summary>ディスポーズ</summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            // バッファを開放
            VisibleBuffer = null;
        }

        /// <summary>内部バッファを返す</summary>
        public byte[] GetBuffer() {
            return VisibleBuffer;
        }
    }
}