using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;
using System.Threading.Tasks;
using System.IO;

namespace Test {

    [TestClass]
    public class ExpandableMemoryTest {

        [TestMethod]
        public void Test() {
            var buf = new ExpandableMemoryStream(1);
            var wr = new StreamWriter(buf);
            wr.Write("あ");
            wr.WriteLine("あい");
            wr.Write("う");
            wr.WriteLine("うえ");
            wr.Flush();
            buf.Position = 0;
            var rd = new StreamReader(buf);
            while (!rd.EndOfStream) {
                string s;
                s = rd.ReadLine();
                Assert.AreEqual("ああい", s);
                s = rd.ReadLine();
                Assert.AreEqual("ううえ", s);
            }
        }
    }
}

