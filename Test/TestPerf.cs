using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public partial class Perf {

        [TestMethod]
        public unsafe void TestPerf() {
            var Boost = 100;
            var limit = 10;
            var buffer = new byte[8];
            var count = 0L;
            count = Bench.Run(limit, (n) => {
                for (var j = 0; j < Boost; ++j) {
                    buffer[0] = (byte)n;
                    buffer[1] = (byte)(n >> 8);
                    buffer[2] = (byte)(n >> 16);
                    buffer[3] = (byte)(n >> 24);
                    buffer[4] = (byte)(n >> 32);
                    buffer[5] = (byte)(n >> 40);
                    buffer[6] = (byte)(n >> 48);
                    buffer[7] = (byte)(n >> 56);
                }
            });
            Console.WriteLine("type1 " + count / Boost);
            count = Bench.Run(limit, (n) => {
                for (var j = 0; j < Boost; ++j) {
                    var t = (uint)n;
                    buffer[0] = (byte)t;
                    buffer[1] = (byte)(t >> 8);
                    buffer[2] = (byte)(t >> 16);
                    buffer[3] = (byte)(t >> 24);
                    t = (uint)(n >> 32);
                    buffer[4] = (byte)t;
                    buffer[5] = (byte)(t >> 8);
                    buffer[6] = (byte)(t >> 16);
                    buffer[7] = (byte)(t >> 24);
                }
            });
            Console.WriteLine("type2 " + count / Boost);
        }
    }
}

