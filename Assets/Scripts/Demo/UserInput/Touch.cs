using UnityEngine;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// Структура, содержащая данные о прикосновениях к экрану
	/// </summary>
	public class Touch
	{
		public int Id { get; set; }
		/// <summary>
		/// Точка прикосновения к экрану
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// Смещение относительно предыдущего прикосновения к экрану
		/// </summary>
		public Vector2 DeltaPosition { get; set; }

		/// <summary>
		/// Количество времени которое прошло с предыдущего прикосновения
		/// </summary>
		public float DeltaTime { get; set; }

		/// <summary>
		/// возникло ли прикосновение на этом кадре или уже было
		/// </summary>
		public bool TouchFirstFrame { get; set; }
	}
}
