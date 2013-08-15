using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityDI;

namespace UnityDiTests
{
	[TestClass]
	public class DependentTests
	{
		[TestMethod]
		public void TypeInjectedCalledOnce()
		{
			var container = new Container();
			container.RegisterType<Dependent>();

			var a = container.Resolve<Dependent>();
			Assert.AreEqual(1, a.OnInjectedCalledCount);
		}
		
		[TestMethod]
		public void SingletonInjectedCalledOnce()
		{
			var container = new Container();
			container.RegisterSingleton<Dependent>();

			var a = container.Resolve<Dependent>();
			Assert.AreEqual(1, a.OnInjectedCalledCount);
		}
		
		[TestMethod]
		public void InstanceInjectedNotCalled()
		{
			var container = new Container();
			var a = new Dependent();
			container.RegisterInstance(a);

			var b = container.Resolve<Dependent>();
			Assert.AreEqual(0, a.OnInjectedCalledCount);
			Assert.AreEqual(0, b.OnInjectedCalledCount);
		}
	}
}
