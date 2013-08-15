using System;

namespace TestDI.Demo
{
	/// <summary>
	/// Интерфейс режима полета
	/// </summary>
	public interface IFlight
	{
		event Action GameFinished;

		void StartNewFlight();

		void Update(float deltaTime);
	}
}
