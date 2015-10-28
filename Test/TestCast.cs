using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar.Byte;

namespace Test {

    [TestClass]
    public class TestCast {

        public sealed class Hoge { }
        public class Fuga {
            public Hoge hoge = new Hoge();
            public static implicit operator Hoge(Fuga f) {
                return f.hoge;
            }
        }
        public class Moga : Fuga {
        }

        [TestMethod]
        public void TestCastUnsigned() {
            var hoge = new Hoge();
            var fuga = new Fuga();
            var moga = new Moga();
            //fuga = (Fuga)moga;//.Cast<Fuga>();
            //hoge = fuga;
            //.Cast<Hoge>();


            var b = (byte)0x80;
            var s = (ushort)0x8000;
            var i = (uint)0x80000000;
            var l = (ulong)0x8000000000000000;

            //Assert.AreEqual(ub.ToUInt8(), unchecked(ub));
            Assert.AreEqual(b.ToUInt16(), unchecked((ushort)b));
            Assert.AreEqual(b.ToUInt32(), unchecked((uint)b));
            Assert.AreEqual(b.ToUInt64(), unchecked((ulong)b));
            Assert.AreEqual(b.CastInt8(), unchecked((sbyte)b));
            Assert.AreEqual(b.ToInt16(), unchecked((short)b));
            Assert.AreEqual(b.ToInt32(), unchecked((int)b));
            Assert.AreEqual(b.ToInt64(), unchecked((long)b));

            Assert.AreEqual(s.CastUInt8(), unchecked((byte)s));
            //Assert.AreEqual(us.ToUInt16(), unchecked((ushort)us));
            Assert.AreEqual(s.ToUInt32(), unchecked((uint)s));
            Assert.AreEqual(s.ToUInt64(), unchecked((ulong)s));
            Assert.AreEqual(s.CastInt8(), unchecked((sbyte)s));
            Assert.AreEqual(s.CastInt16(), unchecked((short)s));
            Assert.AreEqual(s.ToInt32(), unchecked((int)s));
            Assert.AreEqual(s.ToInt64(), unchecked((long)s));

            Assert.AreEqual(i.CastUInt8(), unchecked((byte)i));
            Assert.AreEqual(i.CastUInt16(), unchecked((ushort)i));
            //Assert.AreEqual(ui.CastUInt32(), unchecked((uint)ui));
            Assert.AreEqual(i.ToUInt64(), unchecked((ulong)i));
            Assert.AreEqual(i.CastInt8(), unchecked((sbyte)i));
            Assert.AreEqual(i.CastInt16(), unchecked((short)i));
            Assert.AreEqual(i.CastInt32(), unchecked((int)i));
            Assert.AreEqual(i.ToInt64(), unchecked((long)i));

            Assert.AreEqual(l.CastUInt8(), unchecked((byte)l));
            Assert.AreEqual(l.CastUInt16(), unchecked((ushort)l));
            Assert.AreEqual(l.CastUInt32(), unchecked((uint)l));
            //Assert.AreEqual(ul.ToUInt64(), unchecked((ulong)ul));
            Assert.AreEqual(l.CastInt8(), unchecked((sbyte)l));
            Assert.AreEqual(l.CastInt16(), unchecked((short)l));
            Assert.AreEqual(l.CastInt32(), unchecked((int)l));
            Assert.AreEqual(l.CastInt64(), unchecked((long)l));
        }

        [TestMethod]
        public void TestCastSigned() {
            var b = unchecked((sbyte)0x80);
            var s = unchecked((short)0x8000);
            var i = unchecked((int)0x80000000);
            var l = unchecked((long)0x8000000000000000);

            Assert.AreEqual(b.CastUInt8(), unchecked((byte)b));
            Assert.AreEqual(b.CastUInt16(), unchecked((ushort)b));
            Assert.AreEqual(b.CastUInt32(), unchecked((uint)b));
            Assert.AreEqual(b.CastUInt64(), unchecked((ulong)b));
            //Assert.AreEqual(b.ToInt8(), unchecked((sbyte)b));
            Assert.AreEqual(b.ToInt16(), unchecked((short)b));
            Assert.AreEqual(b.ToInt32(), unchecked((int)b));
            Assert.AreEqual(b.ToInt64(), unchecked((long)b));

            Assert.AreEqual(s.CastUInt8(), unchecked((byte)s));
            Assert.AreEqual(s.CastUInt16(), unchecked((ushort)s));
            Assert.AreEqual(s.CastUInt32(), unchecked((uint)s));
            Assert.AreEqual(s.CastUInt64(), unchecked((ulong)s));
            Assert.AreEqual(s.CastInt8(), unchecked((sbyte)s));
            //Assert.AreEqual(s.ToInt16(), unchecked((short)s));
            Assert.AreEqual(s.ToInt32(), unchecked((int)s));
            Assert.AreEqual(s.ToInt64(), unchecked((long)s));

            Assert.AreEqual(i.CastUInt8(), unchecked((byte)i));
            Assert.AreEqual(i.CastUInt16(), unchecked((ushort)i));
            Assert.AreEqual(i.CastUInt32(), unchecked((uint)i));
            Assert.AreEqual(i.CastUInt64(), unchecked((ulong)i));
            Assert.AreEqual(i.CastInt8(), unchecked((sbyte)i));
            Assert.AreEqual(i.CastInt16(), unchecked((short)i));
            //Assert.AreEqual(i.ToInt32(), unchecked((int)i));
            Assert.AreEqual(i.ToInt64(), unchecked((long)i));

            Assert.AreEqual(l.CastUInt8(), unchecked((byte)l));
            Assert.AreEqual(l.CastUInt16(), unchecked((ushort)l));
            Assert.AreEqual(l.CastUInt32(), unchecked((uint)l));
            Assert.AreEqual(l.CastUInt64(), unchecked((ulong)l));
            Assert.AreEqual(l.CastInt8(), unchecked((sbyte)l));
            Assert.AreEqual(l.CastInt16(), unchecked((short)l));
            Assert.AreEqual(l.CastInt32(), unchecked((int)l));
            //Assert.AreEqual(l.ToInt64(), unchecked((long)l));
        }
    }
}
