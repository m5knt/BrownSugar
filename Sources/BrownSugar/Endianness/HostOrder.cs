/**
 * @file
 * @brief ホストオーダー順のバッファ操作関係
 */

using System;

//
//
//

namespace ThunderEgg.BrownSugar {

    /// <summary>ホストエンディアン順のバッファ操作関係</summary>
    public partial class HostOrder : NoOrder {
    }
}

//
//
//

namespace ThunderEgg.BrownSugar {

    /// <summary>エンディアンを知っている型</summary>
    using EndianHolder = BitConverter;

    public partial class HostOrder { 

        /// <summary>オブジェクトをバイナリ化しバッファへ書き込む</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int Assign<T>(byte[] buffer, int index, T obj) //
        {
            return ByteOrder.Assign<T>(buffer, index, obj, //
                EndianHolder.IsLittleEndian);
        }

        /// <summary>オブジェクトをバイナリ化しバッファを返す</summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] GetBytes<T>(T obj) {
            return ByteOrder.GetBytes<T>(obj, EndianHolder.IsLittleEndian);
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static int CopyTo<T>(byte[] buffer, int index, T obj) //
            where T : class //
        {
            return ByteOrder.CopyTo<T>(buffer, index, obj, //
                EndianHolder.IsLittleEndian);
        }

        /// <summary>バッファからオブジェクトを復元する</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T To<T>(byte[] buffer, int index) {
            return ByteOrder.To<T>(buffer, index, EndianHolder.IsLittleEndian);
        }

    }
}
