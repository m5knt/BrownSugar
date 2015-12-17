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
        public void ByteAndSeekTest() {
            var buf = new ExpandableMemoryStream(1);
            buf.SetLength(0);
            buf.WriteByte(1);
            buf.WriteByte(2);
            buf.WriteByte(3);
            buf.WriteByte(4);
            buf.Flush();
            buf.Seek(0, SeekOrigin.Begin);
            Assert.AreEqual(1, buf.ReadByte());
            Assert.AreEqual(2, buf.ReadByte());
            buf.Seek(-1, SeekOrigin.Current);
            Assert.AreEqual(2, buf.ReadByte());
            Assert.AreEqual(3, buf.ReadByte());
            Assert.AreEqual(4, buf.ReadByte());
            Assert.AreEqual(-1, buf.ReadByte());
            buf.Seek(-1, SeekOrigin.End);
            Assert.AreEqual(4, buf.ReadByte());
        }

        [TestMethod]
        public void StreamReaderWriterTest() {
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

