using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;

using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class TestStringExtension {

        [TestMethod]
        public void Misc() {
            Assert.AreEqual(string.Copy("123"), "123".Copy());
            Assert.AreEqual(string.IsNullOrEmpty("123"), "123".IsNullOrEmpty());
            Assert.AreEqual(string.IsNullOrEmpty(null), ((string)null).IsNullOrEmpty());
            var t = new string[] { "1", "2", "3" };
            Assert.AreEqual(string.Join(",", t), ",".Join(t));
            Assert.AreEqual(string.Join(",", t, 1, 2), ",".Join(t, 1, 2));
        }

        [TestMethod]
        public void Format() {
            Assert.AreEqual(string.Format("{0}", 0), "{0}".format("0"));
            Assert.AreEqual(string.Format("{0}{1}", 0, 1), "{0}{1}".format("0", "1"));
            Assert.AreEqual(string.Format("{0}{1}{2}", 0, 1, 2), "{0}{1}{2}".format("0", "1", "2"));
            Assert.AreEqual(string.Format("{0}{1}{2}{3}", 0, 1, 2, 3), "{0}{1}{2}{3}".format("0", "1", "2", "3"));
        }

        [TestMethod]
        public void Intern() {
            Assert.AreSame(string.Intern("ABC"), "ABC".Intern());
            Assert.AreSame(string.Intern("ABC"), "ABC".IsInterned());
            string str1 = "a";
            string str2 = str1 + "b";
            string str3 = str2 + "c";
            Assert.AreEqual(null, str3.IsInterned());
        }
    }
}
