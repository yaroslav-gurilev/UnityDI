using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityDI;

namespace UnityDiTests
{
	[TestClass]
	public class RegisterSingletonTests
	{
		[TestMethod]
		public void RegisterSingleton()
		{
			var container = new Container();
			container.RegisterSingleton<ClassA>();

			var a = container.Resolve<ClassA>();
			Assert.IsNotNull(a);
			Assert.AreEqual(typeof(ClassA), a.GetType());
		}

		[TestMethod]
		public void RegisterInterface()
		{
			var container = new Container();
			container.RegisterSingleton<IInterface, InterfaceImplementor>();

			var a = container.Resolve<IInterface>();
			Assert.IsNotNull(a);
			Assert.AreEqual(typeof(InterfaceImplementor), a.GetType());
		}

		[TestMethod]
		public void RegisterAndResolveTwice()
		{
			var container = new Container();
			container.RegisterSingleton<ClassA>();

			var a = container.Resolve<ClassA>();
			var b = container.Resolve<ClassA>();
			Assert.AreSame(a, b);
		}

		[TestMethod]
		public void RegisterNamedAndResolveTwice()
		{
			var container = new Container();
			container.RegisterSingleton<ClassA>("A");

			var a = container.Resolve<ClassA>("A");
			var b = container.Resolve<ClassA>("A");
			Assert.AreSame(a, b);
		}
		
		[TestMethod]
		public void Register2Named()
		{
			var container = new Container();
			container.RegisterSingleton<ClassA>("A");
			container.RegisterSingleton<ClassA>("B");

			var a = container.Resolve<ClassA>("A");
			var b = container.Resolve<ClassA>("B");
			Assert.AreNotSame(a, b);
		}

		[TestMethod]
		public void TestBuildUp()
		{
			var container = new Container();
			container.RegisterType<ClassA>();
			container.RegisterSingleton<ClassWithDependency>();

			var a = container.Resolve<ClassWithDependency>();
			Assert.IsNotNull(a.Dependency);
			var b = container.Resolve<ClassWithDependency>();
			Assert.AreSame(a.Dependency, b.Dependency);
		}
	}
}
