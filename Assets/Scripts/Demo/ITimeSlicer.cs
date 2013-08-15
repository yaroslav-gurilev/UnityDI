namespace TestDI.Demo
{
	/// <summary>
	/// Интерфейс таймера
	/// </summary>
	public interface ITimeSlicer
	{
		float Timeout { get; set; }

		bool Update(float deltaTime);
	}
}
