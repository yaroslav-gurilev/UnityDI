using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityDI;

namespace UnityDiTests
{
	[TestClass]
	public class RegisterInstanceTests
	{
		[TestMethod]
		public void RegisterInstance()
		{
			var container = new Container();
			var a = new ClassA();
			container.RegisterInstance(a);

			var b = container.Resolve<ClassA>();
			Assert.IsNotNull(a);
			Assert.AreSame(a, b);
		}

		[TestMethod]
		public void Register2Instances()
		{
			var container = new Container();
			var a = new ClassA();
			container.RegisterInstance(a, "A");
			var b = new ClassA();
			container.RegisterInstance(b, "B");

			var a1 = container.Resolve<ClassA>("A");
			var b1 = container.Resolve<ClassA>("B");
			Assert.AreSame(a, a1);
			Assert.AreSame(b, b1);
		}

		[TestMethod]
		public void BuildUpNotPerformed()
		{
			var container = new Container();
			var a = new ClassWithDependency();
			container.RegisterInstance(a);
			container.RegisterType<ClassA>();

			var b = container.Resolve<ClassWithDependency>();
			Assert.IsNull(b.Dependency);
		}
	}
}
