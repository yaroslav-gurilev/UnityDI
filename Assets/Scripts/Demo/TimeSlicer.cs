namespace TestDI.Demo
{
	/// <summary>
	/// Таймер
	/// </summary>
	public class TimeSlicer : ITimeSlicer
	{
		private float _time;

		public float Timeout { get; set; }

		public bool Update(float deltaTime)
		{
			_time += deltaTime;
			if (_time > Timeout)
			{
				_time -= Timeout;
				return true;
			}

			return false;
		}
	}
}
