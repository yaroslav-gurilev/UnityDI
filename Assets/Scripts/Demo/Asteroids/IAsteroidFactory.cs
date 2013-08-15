namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Фабрика астероидов
	/// </summary>
	public interface IAsteroidFactory
	{
		/// <summary>
		/// Создать новый астероид
		/// </summary>
		/// <returns></returns>
		IAsteroid CreateAsteroid();
	}
}
