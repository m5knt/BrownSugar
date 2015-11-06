
namespace ThunderEgg.BrownSugar
{
    /// <summary>リトルエンディアン順でバッファを操作します</summary>
    public class LittleEndian : HostOrderAligned {
    }

    /// <summary>ビッグエンディアン順でバッファを操作します</summary>
    public class BigEndian : BigEndianAny {
    }

    /// <summary>ネットワークバイトオーダー順でバッファを操作します</summary>
    public class NetOrder : BigEndianAny {
    }
}
