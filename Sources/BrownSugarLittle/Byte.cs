
namespace ThunderEgg.BrownSugar
{
    /// <summary>リトルエンディアンでバッファを操作します</summary>
    public class LittleEndian : HostOrderAligned {
    }

    /// <summary>ビッグエンディアンでバッファを操作します</summary>
    public class BigEndian : BigEndianAny {
    }

    /// <summary>ネットワークバイトオーダーでバッファを操作します</summary>
    public class NetOrder : BigEndianAny {
    }
}
