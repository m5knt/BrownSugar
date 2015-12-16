/**
* @file
* @brief 内部バッファ参照可能なメモリーストリーム
*/

using System;
using System.IO;

namespace ThunderEgg.BrownSugar {

    /// <summary>
    /// バッファを自動伸長しつつ内部バッファ参照可能なメモリーストリーム
    /// </summary>
    public class ExpandableMemoryStream : Stream {

		/// <summary>ストリームバッファ</summary>
        byte[] Buffer;

		/// <summary>ストリーム量</summary>
		long Length_;

		/// <summary>シーク位置</summary>
		long Position_;

		/// <summary>デフォルトのストリームバッファ量</summary>
		const int Default = 4096;

		/// <summary>拡張量の式</summary>
        Func<long, long> Expander = _ => _ * 2;

		//
		//
		//


        /// <summary>コンストラクタ</summary>
		/// <param name="exander">拡張量計算デリゲート</param>
		/// バッファはデフォルト量(4096)になる
		public ExpandableMemoryStream(Func<long, long> expander = null) //
            : this(Default, expander) //
        {
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="capacity">バッファ拡張しなくても済む量</param>
        /// <param name="exander">拡張量計算デリゲート</param>
        public ExpandableMemoryStream(long capacity, Func<long, long> expander = null) {
	        SetBuffer(capacity);
            if (expander != null) {
                Expander = expander;
            }
        }

		/// <summary>新規バッファを割り当てる</summary>
		/// <param name="capacity">バッファ拡張しなくても済む量</param>
		public void SetBuffer(long capacity) {
			SetBuffer(new byte[Math.Max(capacity, 1)]);
		}

		/// <summary>新規バッファを割り当てる</summary>
		/// <param name="buffer">バッファ</param>
		public void SetBuffer(byte[] buffer) {
			if (buffer == null) {
				throw new ArgumentNullException();
			}
			if (buffer.LongLength < 1) {
				throw new ArgumentException();
			}
			Buffer = buffer;
			Position_ = 0;
			Length_ = 0;
		}

		/// <summary>内部バッファを返す</summary>
		public byte[] GetBuffer() {
			return Buffer;
		}

		/// <summary>読み込み操作可能か</summary>
        public override bool CanRead { get { return true; } }

        /// <summary>シーク可能か</summary>
        public override bool CanSeek { get { return true; } }

        /// <summary>タイムアウト可能化か</summary>
        public override bool CanTimeout { get { return false; } }

        /// <summary>書き込み操作可能か</summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>ストリーム長</summary>
        public override long Length { get { return Length_; } }

        /// <summary>読み書き位置</summary>
        public override long Position {
            get { return Position_; }
            set {
                if (value < 0 || value >= Buffer.Length) {
                    throw new Exception();
                }
                Position_ = value;
            }
        }

        /// <summary>シーク指定</summary>
        public override long Seek(long offset, SeekOrigin origin) {
            long t;
            switch (origin) {
                default:
                case SeekOrigin.Begin: t = offset; break;
                case SeekOrigin.Current: t = Position_ + offset; break;
                case SeekOrigin.End: t = Length_ + offset; break;
            }
            if (t < 0 || t >= Length_) {
                throw new Exception();
            }
            Position_ = t;
            return Position_;
        }

        /// <summary>ストリーム長を設定する</summary>
        public override void SetLength(long value) {
            AutoReallocBuffer(value);
            Position_ = value;
            Length_ = value;
        }

		/// <summary>ストリームの内容を読み込む</summary>
		public override int Read(byte[] buffer, int offset, int count) {
			var from = Position_;
			var to = Math.Min(Position_ + count, Length_);
			var len = (int) (to - from);
			if (len <= 0) {
				return 0;
			}
			System.Buffer.BlockCopy(Buffer, (int)from, buffer, offset, len);
			Position_ = to;
			return len;
		}

		/// <summary>ストリームへデータを書き込む</summary>
        public override void Write(byte[] buffer, int offset, int count) {
            AutoReallocBuffer(Position_ + count);
            System.Buffer.BlockCopy(buffer, offset, Buffer, (int)Position_, count);
            Position_ += count;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>ストリームへデータを書き込む</summary>
        public override void WriteByte(byte value) {
            AutoReallocBuffer(Position_ + 1);
            Buffer[Position_++] = value;
            Length_ = Math.Max(Length_, Position_);
        }

        /// <summary>バッファサイズを調整する</summary>
        void AutoReallocBuffer(long n) {
            var len = Buffer.LongLength;
            if (len > n) {
                return;
            }
            while (len < n) len = Expander(len);
            var expand = new byte[len];
            Buffer.CopyTo(expand, 0);
            Buffer = expand;
        }

        /// <summary>一時バッファを書き出す、特に何もしない</summary>
        public override void Flush() {        
        }

		/// <summary>ディスポーズ</summary>
        protected override void Dispose(bool disposing) {
			Buffer = null;
		}

    }
}