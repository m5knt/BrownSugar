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
    public class Test {

	    [TestMethod]
        public void T() {
            var b = new ExpandableMemoryStream(1);
            using (var a = new StreamWriter(b)) {
                a.Write("あ");
                a.Flush();
            }
        }
    }
}
