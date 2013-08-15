using System.Collections.Generic;

namespace TestDI.Demo.UserInput
{
	public interface IInput3D
	{
		IEnumerable<Touch3D> Touches { get; } 

		void Update();
	}
}
