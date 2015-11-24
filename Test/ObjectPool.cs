using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;

namespace Test {

    [TestClass]
    public class ObjectPool_ {

        [TestMethod]
        public void ObjectPool() {
            var pool = new ObjectPool<byte[]>(
                () => {
                    return new byte[1500];
                }, 100);
            using (var n = pool.Allocate()) {
                var t = n.Value;
            }
        }
    }
}

