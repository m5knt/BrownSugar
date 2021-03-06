﻿/**
* @file
* @brief バッファを自動伸長しつつ内部バッファ参照可能なメモリーストリーム
*/

using System;
using System.IO;

namespace ThunderEgg.BrownSugar {

    /// <summary>
    /// バッファを自動伸長しつつ内部バッファ参照可能なメモリーストリーム
    /// System.IO.MemoryStream はバッファを与えると自動伸長動作しない
    /// visible でインスタンスを作成した場合 GetBuffer() が利用できない
    /// </summary>
    public class ExpandableMemoryStream : Stream {

        /// <summary>伸長しなくて済むバッファ量を設定する</summary>
        public long Capacity {
            get {
                CheckDisposed();
                return Buffer_ == null ? 0 : Buffer_.Length;
            }
            set {
                CheckDisposed();
                ExpandBuffer(value);
            }
        }

        /// <summary>ストリーム長を返す</summary>
        public override long Length {
            get { return Length_; }
        }

        /// <summary>ストリーム量</summary>
        long Length_;

        /// <summary>読み書き位置</summary>
        public override long Position {
            get { return Position_; }
            set {
                CheckDisposed();
                if (value < 0 || value > Length) {
                    throw new ArgumentOutOfRangeException();
                }
                Position_ = value;
            }
        }

        /// <summary>シーク位置</summary>
        long Position_;

        /// <summary>ストリームバッファ</summary>
        byte[] Buffer_;

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

        //
        //
        //

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

        /// <summary>ディスポーズ</summary>
        protected override void Dispose(bool disposing) {
            Buffer_ = null;
        }

        /// <summary>ディスポーズ済み判定</summary>
        void CheckDisposed() {
            if (Buffer_ == null) throw new ObjectDisposedException("");
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
            CheckDisposed();
            return Buffer_;
        }

        //
        //
        //

        /// <summary>シーク</summary>
        /// <param name="offset">originからのオフセット</param>
        /// <param name="origin">起点位置</param>
        /// <returns>新しいシーク位置を返す</returns>
        /// <exception cref="IOException">シーク位置がストリームより前の時</exception>
        /// <exception cref="ArgumentOutOfRangeException">offsetがint.MaxValueより大きい時</exception>
        /// <exception cref="ArgumentException">originが無効</exception>
        public override long Seek(long offset, SeekOrigin origin) {
            CheckDisposed();
            if (offset > int.MaxValue) throw new ArgumentOutOfRangeException("offset, too big");
            long t;
            switch (origin) {
                default: throw new ArgumentException("illigal orgin");
                case SeekOrigin.Begin: t = offset; break;
                case SeekOrigin.Current: t = Position_ + offset; break;
                case SeekOrigin.End: t = Length + offset; break;
            }
            if (t < 0) throw new IOException("illigal seek position");
            ExpandBuffer(t);
            Length_ = Math.Max(Length_, Position_);
            Position_ = t;
            return Position_;
        }

        /// <summary>ストリーム長を設定する</summary>
        /// <exception cref="ArgumentOutOfRangeException">valueがマイナスまたはint.MaxValueを超過時</exception>
        public override void SetLength(long value) {
			SetLength(value, true);
        }

		/// <summary>ストリーム長を設定する 拡張時のフィル制御可能版</summary>
		/// <exception cref="ArgumentOutOfRangeException">valueがマイナスまたはint.MaxValueを超過時</exception>
		public void SetLength(long value, bool fill) {
			CheckDisposed();
			if (value < 0 || value > int.MaxValue) {
				throw new ArgumentOutOfRangeException();
			}
			ExpandBuffer(value);
			var from = (int)Length_;
			Length_ = value;
			Position_ = Math.Min(Position_, Length_);
			var to = (int)Length;
			// 拡張部分をフィルする
			if (fill && from < to) {
				Array.Clear(Buffer_, from, to - from);
			}
		}

        /// <summary>ストリームから読み込む</summary>
        /// <returns>読み込めた量</returns>
        /// <exception cref="ArgumentNullException">bufferがヌルの時</exception>
        /// <exception cref="ArgumentOutOfRangeException">offsetかcountがマイナスの時</exception>
        /// <exception cref="ArgumentException">count数が大きい時</exception>
        public override int Read(byte[] buffer, int offset, int count) {
            CheckDisposed();
            // 引数確認
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || count < 0) throw new ArgumentOutOfRangeException("offset or count");
            if ((count + offset) > buffer.Length) throw new ArgumentException("count, too big");
            // 範囲の確認
            var from = Position_;
            var to = Math.Min(Position_ + count, Length_);
            var size = (int)(to - from);
            // 最後に到達したら 0
            if (size <= 0) return 0;
            // ストリームの内容をバッファへ読み込む
            Buffer.BlockCopy(Buffer_, (int)from, buffer, offset, size);
            Position_ += size;
            return size;
        }

        /// <summary>ストリームから読み込む</summary>
        /// <returns>バイトデータ/終端時は-1</returns>
        public override int ReadByte() {
            CheckDisposed();
            if (Position_ == Length_) return -1;
            return Buffer_[Position_++];
        }

        /// <summary>ストリームへ書き込む</summary>
        /// <exception cref="ArgumentNullException">bufferがヌルの時</exception>
        /// <exception cref="ArgumentOutOfRangeException">offsetかcountがマイナス</exception>
        /// <exception cref="ArgumentException">count数を大きい時</exception>
        public override void Write(byte[] buffer, int offset, int count) {
            CheckDisposed();
            // 引数の確認
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (offset < 0 || count < 0) throw new ArgumentOutOfRangeException("offset or count");
            if ((count + offset) > buffer.Length) throw new ArgumentException("count, too big");
            // 内部バッファを自動拡張し書き込む
            ExpandBuffer(Position_ + count);
            Buffer.BlockCopy(buffer, offset, Buffer_, (int)Position_, count);
            Position_ += count;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>ストリームへ書き込む</summary>
        public override void WriteByte(byte value) {
            CheckDisposed();
            ExpandBuffer(Position_ + 1);
            Buffer_[Position_++] = value;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>バッファサイズを調整する</summary>
        void ExpandBuffer(long n) {
            // 大きすぎか確認する
            if (n > int.MaxValue) {
                throw new OverflowException("stream, too big");
            }
            // 十分なら何もしない
            if (Buffer_ != null & Buffer_.Length > n) return;
            // バッファ拡張し内容をコピーする
            var size = Math.Min(CalcExpandSize(n), int.MaxValue);
            var expand = new byte[size];
            if (Buffer_ != null) {
                Buffer_.CopyTo(expand, 0);
            }
            Buffer_ = expand;
        }

        /// <summary>一時バッファの内容をストリームへ書き出す、特に何もしない</summary>
        public override void Flush() {
        }
    }
}