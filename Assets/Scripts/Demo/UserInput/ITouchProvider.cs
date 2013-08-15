using System.Collections.Generic;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// Интерфейс класса, порождающего объекты типа Touch
	/// </summary>
	public interface ITouchProvider
	{
		IEnumerable<Touch> Touches { get; }

		void Update();
	}
}
