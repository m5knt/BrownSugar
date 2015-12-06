using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    public partial class ByteOrderTest : Values {

        // メンバーの並びは定義順,詰め系の並び,文字列はUnicode(utf16)
        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        unsafe class SwapStringType {
            // 文字列は Win32 の TCHAR に相当するが文字型を決定する指定は
            // StructLayout の CharSet になる
            // 文字列はヌル終端サイズはヌル文字を含む
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string str = "あいうえおかきくけ\0";
            public char c16 = '@';
        }

        [TestMethod]
        public unsafe void SwapString() {
            var src = new SwapStringType();
            var srcbin = new byte[Marshal.SizeOf(src)];
            ByteOrder.MarshalAssign(srcbin, 0, src);
            var extbin = (byte[])srcbin.Clone();
            ByteOrder.Swap(extbin, 0, typeof(SwapStringType));
            var ext = ByteOrder.MarshalTo<SwapStringType>(extbin, 0);
            for(var i = 0; i < ext.str.Length; ++i) {
                var t = ext.str[i];
                Assert.AreEqual(src.str[i], ByteOrder.Swap(t));
            }
            Assert.AreEqual(src.c16, ByteOrder.Swap(ext.c16));
        }
    }
}
