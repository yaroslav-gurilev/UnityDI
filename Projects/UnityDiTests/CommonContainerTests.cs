using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityDI;

namespace UnityDiTests
{
	[TestClass]
	public class CommonContainerTests
	{
		[TestMethod]
		public void CreateContainer()
		{
			var container = new Container();
		}
	}
}
