using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Runtime.InteropServices;
using ThunderEgg.BrownSugar;
using ThunderEgg.BrownSugar.Extentions;
using System.Threading.Tasks;

namespace Test {
    [TestClass]
    public class Test {

		void action() {  }
		void action(object n) { }
//		int func() { return 0; }
//		int func(int n) { return 0; }
		int func() { return 0; }
		int func(object n) { return 0; }

	    [TestMethod]
        public void T() {
			Task.Factory.StartNew(action); // OK StartNew(Action action);
			Task.Factory.StartNew(action, 0); // だめ StartNew(Action action, TaskCreationOptions creationOptions);
			Task.Factory.StartNew<int>(func); // StartNew<TResult>(Func<TResult> function);
			Task.Factory.StartNew<int>(func, 0); // だめ StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions);
#if false
			//
			Task.Factory.StartNew(() => { }); // OK StartNew(Action action);
			Task.Factory.StartNew(() => 0); // OK StartNew<TResult>(Func<TResult> function);
			Task.Factory.StartNew(_ => { }, 0); // OK StartNew(Action<object> action, object state);
			Task.Factory.StartNew(_ => 0, 0); // OK StartNew<TResult>(Func<object, TResult> function, object state);
#endif
		}
    }
}
