using UnityDI;

namespace UnityDiTests
{
	public class ClassA
	{
	}

	public class ClassB : ClassA
	{
	}

	public class ProtectedContructor
	{
		protected ProtectedContructor() {}
	}

	public interface IInterface
	{
	}

	public class InterfaceImplementor : IInterface
	{
	}

	public class ClassWithDependency
	{
		[Dependency]
		public ClassA Dependency { get; set; }
	}
	
	public class ClassWithNamedDependency
	{
		[Dependency("A")]
		public ClassA Dependency { get; set; }
	}
	
	public class ClassWith2NamedDependencies
	{
		[Dependency("A")]
		public ClassA DependencyA { get; set; }
		
		[Dependency("B")]
		public ClassA DependencyB { get; set; }
	}

	public class ClassWith2Dependencies
	{
		[Dependency("A")]
		public ClassA DependencyA { get; set; }

		[Dependency]
		public ClassA DependencyB { get; set; }
	}

	public class Dependent : IDependent
	{
		public int OnInjectedCalledCount { get; private set; }

		public void OnInjected()
		{
			++OnInjectedCalledCount;
		}
	}
}
