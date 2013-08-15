using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityDI;

namespace UnityDiTests
{
	[TestClass]
	public class RegisterTypeTests
	{
		[TestMethod]
		public void RegisterType()
		{
			var container = new Container();
			container.RegisterType<ClassA>();

			var a = container.Resolve<ClassA>();
			Assert.IsNotNull(a);
			Assert.AreEqual(typeof(ClassA), a.GetType());
		}

		[TestMethod]
		public void RegisterTypeAndResolveTwice()
		{
			var container = new Container();
			container.RegisterType<ClassA>();

			var a = container.Resolve<ClassA>();
			var b = container.Resolve<ClassA>();
			Assert.AreNotSame(a, b);
		}

		[TestMethod]
		public void CantFindType()
		{
			var container = new Container();

			try
			{
				container.Resolve<ClassA>();
				Assert.Fail("Exception expected");
			}
			catch (ContainerException) {}
		}

		/*
		 * Compile error
		[TestMethod]
		public void ProtectedConstructor()
		{
			var container = new Container();
			container.RegisterType<ProtectedContructor>();
		}*/

		[TestMethod]
		public void RegisterDerived()
		{
			var container = new Container();
			container.RegisterType<ClassA, ClassB>();

			ClassA a = container.Resolve<ClassA>();
			Assert.AreEqual(typeof(ClassB), a.GetType());
		}

		[TestMethod]
		public void RegisterAsInterface()
		{
			var container = new Container();
			container.RegisterType<IInterface, InterfaceImplementor>();

			IInterface a = container.Resolve<IInterface>();
			Assert.AreEqual(typeof(InterfaceImplementor), a.GetType());
		}

		[TestMethod]
		public void ResolveDependency()
		{
			var container = new Container();
			container.RegisterType<ClassA>().RegisterType<ClassWithDependency>();
			var d = container.Resolve<ClassWithDependency>();
			Assert.IsNotNull(d);
			Assert.IsNotNull(d.Dependency);
		}


		[TestMethod]
		public void RegisterNamedType()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A");

			var a = container.Resolve<ClassA>("A");
			Assert.IsNotNull(a);
			Assert.AreEqual(typeof(ClassA), a.GetType());
		}
		
		[TestMethod]
		public void RegisterNamedTypeAndResolveTwice()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A");

			var a = container.Resolve<ClassA>("A");
			var b = container.Resolve<ClassA>("A");
			Assert.AreNotSame(a, b);
		}

		[TestMethod]
		public void CantFindNamedType()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A");

			try
			{
				container.Resolve<ClassA>();
				Assert.Fail("Exception expected");
			}
			catch (ContainerException) { }
		}
		
		[TestMethod]
		public void WrongTypeName()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A");

			try
			{
				container.Resolve<ClassA>("B");
				Assert.Fail("Exception expected");
			}
			catch (ContainerException) { }
		}
		
		[TestMethod]
		public void NamedTypeNotRegistered()
		{
			var container = new Container();
			container.RegisterType<ClassA>();

			try
			{
				container.Resolve<ClassA>("B");
				Assert.Fail("Exception expected");
			}
			catch (ContainerException) { }
		}

		[TestMethod]
		public void RegisterDerivedNamed()
		{
			var container = new Container();
			container.RegisterType<ClassA, ClassB>("A");

			ClassA a = container.Resolve<ClassA>("A");
			Assert.AreEqual(typeof(ClassB), a.GetType());
		}

		[TestMethod]
		public void RegisterNamedAsInterface()
		{
			var container = new Container();
			container.RegisterType<IInterface, InterfaceImplementor>("A");

			IInterface a = container.Resolve<IInterface>("A");
			Assert.AreEqual(typeof(InterfaceImplementor), a.GetType());
		}
		
		[TestMethod]
		public void ResolveNamedDependency()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A").RegisterType<ClassWithNamedDependency>();
			var d = container.Resolve<ClassWithNamedDependency>();
			Assert.IsNotNull(d);
			Assert.IsNotNull(d.Dependency);
		}
		
		[TestMethod]
		public void ResolveNamedDependencyFailed()
		{
			var container = new Container();
			container.RegisterType<ClassA>().RegisterType<ClassWithNamedDependency>();
			try
			{
				container.Resolve<ClassWithNamedDependency>();
				Assert.Fail("Exception expected");
			}
			catch (ContainerException) {}
		}

		[TestMethod]
		public void Resolve2NamedDependencies()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A").RegisterType<ClassA, ClassB>("B").RegisterType<ClassWith2NamedDependencies>();
			var d = container.Resolve<ClassWith2NamedDependencies>();
			Assert.AreEqual(typeof(ClassA), d.DependencyA.GetType());
			Assert.AreEqual(typeof(ClassB), d.DependencyB.GetType());
		}
		
		[TestMethod]
		public void Resolve2Dependencies()
		{
			var container = new Container();
			container.RegisterType<ClassA>("A").RegisterType<ClassA, ClassB>().RegisterType<ClassWith2Dependencies>();
			var d = container.Resolve<ClassWith2Dependencies>();
			Assert.AreEqual(typeof(ClassA), d.DependencyA.GetType());
			Assert.AreEqual(typeof(ClassB), d.DependencyB.GetType());
		}
	}
}
