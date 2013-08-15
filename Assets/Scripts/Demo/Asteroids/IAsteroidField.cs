namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Класс, управляющий жизнью астероидов
	/// </summary>
	public interface IAsteroidField
	{
		void StartNewFlight();

		void Update(float deltaTime);
	}
}
