/**
* @file
* @brief バッファを自動伸長しつつ内部バッファ参照可能なメモリーストリーム
*/

using System;
using System.IO;

namespace ThunderEgg.BrownSugar {

    /// <summary>
    /// バッファを自動伸長しつつ内部バッファ参照可能なメモリーストリーム
    /// </summary>
    public class ExpandableMemoryStream : Stream {

        /// <summary>ストリームバッファ</summary>
        byte[] Buffer_;

        /// <summary>ストリーム量</summary>
        long Length_;

        /// <summary>シーク位置</summary>
        long Position_;

        /// <summary>デフォルトのストリームバッファ量</summary>
        const int Default = 1024;

        /// <summary>拡張量の式</summary>
        Func<long, long> CalcExpandSize = _ => _ * 2;

        //
        //
        //

        /// <summary>読み込み操作可能か</summary>
        public override bool CanRead { get { return true; } }

        /// <summary>シーク可能か</summary>
        public override bool CanSeek { get { return true; } }

        /// <summary>タイムアウト可能化か</summary>
        public override bool CanTimeout { get { return false; } }

        /// <summary>書き込み操作可能か</summary>
        public override bool CanWrite { get { return true; } }


        /// <summary>コンストラクタ</summary>
        /// <param name="calc_expand_size">拡張量計算デリゲート</param>
        /// バッファはデフォルト量(1024)になる
        public ExpandableMemoryStream(Func<long, long> calc_expand_size = null) //
            : this(Default, calc_expand_size) //
        {
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="capacity">バッファ拡張しなくても済む量</param>
        /// <param name="calc_expand_size">拡張量計算デリゲート</param>
        public ExpandableMemoryStream(long capacity, //
            Func<long, long> calc_expand_size = null) //
        {
            SetBuffer(capacity);
            if (calc_expand_size != null) {
                CalcExpandSize = calc_expand_size;
            }
        }

        /// <summary>新規バッファを割り当てる</summary>
        /// <param name="capacity">バッファ拡張しなくても済む量</param>
        void SetBuffer(long capacity) {
            SetBuffer(new byte[Math.Max(capacity, 1)]);
        }

        /// <summary>新規バッファを割り当てる</summary>
        /// <param name="buffer">バッファ</param>
        void SetBuffer(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException();
            }
            if (buffer.LongLength < 1) {
                throw new ArgumentException();
            }
            Buffer_ = buffer;
            Position_ = 0;
            Length_ = 0;
        }

        /// <summary>内部バッファを返す</summary>
        public byte[] GetBuffer() {
            return Buffer_;
        }

        //
        //
        //

        /// <summary>ストリーム長を返す</summary>
        public override long Length { get { return Length_; }
        }

        /// <summary>読み書き位置</summary>
        public override long Position {
            get { return Position_; }
            set {
                if (value < 0 || value > Length) {
                    throw new ArgumentOutOfRangeException();
                }
                Position_ = value;
            }
        }

        /// <summary>シーク</summary>
        /// <param name="offset">originからのオフセット</param>
        /// <param name="origin">起点位置</param>
        public override long Seek(long offset, SeekOrigin origin) {
            long t;
            switch (origin) {
                default: throw new ArgumentException();
                case SeekOrigin.Begin: t = offset; break;
                case SeekOrigin.Current: t = Position_ + offset; break;
                case SeekOrigin.End: t = Length + offset; break;
            }
            if (t < 0) throw new IOException();
            if (t > Length) throw new ArgumentOutOfRangeException();
            Position_ = t;
            return Position_;
        }

        /// <summary>ストリーム長を設定する</summary>
        /// <exception cref="ArgumentOutOfRangeException">valueがマイナスまたはint.MaxValueを超過時</exception>
        public override void SetLength(long value) {
            if (value < 0 || value > int.MaxValue) {
                throw new ArgumentOutOfRangeException();
            }
            ExpandBuffer(value);
            var from = (int)Length_;
            Length_ = value;
            Position_ = Math.Min(Position_, Length_);
            var to = (int)Length;
            if (from < to) {
                Array.Clear(Buffer_, from, to - from);
            }
        }

        /// <summary>ストリームの内容を読み込む</summary>
        public override int Read(byte[] buffer, int offset, int count) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0 || offset > buffer.Length) {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0 || (offset + count) > buffer.Length) {
                throw new ArgumentOutOfRangeException("count");
            }
            var from = Position_;
            var to = Math.Min(Position_ + count, Length_);
            var size = (int)(to - from);
            // 最後に到達したか
            if (size <= 0) {
                return 0;
            }
            Buffer.BlockCopy(Buffer_, (int)from, buffer, offset, size);
            Position_ += size;
            return size;
        }

        /// <summary>ストリームへデータを書き込む</summary>
        public override void Write(byte[] buffer, int offset, int count) {
            // 引数の確認
            if (buffer == null) {
                throw new ArgumentNullException("buffer");
            }
            if (offset < 0 || offset > buffer.Length) {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0 || (offset + count) > buffer.Length) {
                throw new ArgumentOutOfRangeException("count");
            }
            // バッファを自動拡張し書き込む
            ExpandBuffer(Position_ + count);
            Buffer.BlockCopy(buffer, offset, Buffer_, (int)Position_, count);
            Position_ += count;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>ストリームへデータを書き込む</summary>
        public override void WriteByte(byte value) {
            ExpandBuffer(Position_ + 1);
            Buffer_[Position_++] = value;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>バッファサイズを調整する</summary>
        void ExpandBuffer(long n) {
            if (n > int.MaxValue) {
                throw new OverflowException("inner buffer");
            }
            // 十分なら何もしない
            if (Buffer_.Length > n) return;
            // バッファ拡張し内容をコピーする
            var size = Math.Min(CalcExpandSize(n), int.MaxValue);
            var expand = new byte[size];
            Buffer_.CopyTo(expand, 0);
            Buffer_ = expand;
        }

        /// <summary>一時バッファを書き出す、特に何もしない</summary>
        public override void Flush() {
        }

        /// <summary>ディスポーズ</summary>
        protected override void Dispose(bool disposing) {
            Buffer_ = null;
        }

    }
}