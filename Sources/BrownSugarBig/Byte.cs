
namespace ThunderEgg.BrownSugar
{
    /// <summary>リトルエンディアンでバッファを操作します</summary>
    public class LittleEndian : LittleEndianAny {
    }

    /// <summary>ビッグエンディアンでバッファを操作します</summary>
    public class BigEndian : BigEndianAny {
    }

    /// <summary>ネットワークバイトオーダーでバッファを操作します</summary>
    public class NetOrder : BigEndianAny {
    }

    /// <summary>ホストオーダーでバッファを操作します</summary>
    public class HostOrder : BigEndian {
    }
}
