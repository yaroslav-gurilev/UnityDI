using System;
using UnityEngine;

namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Интерфейс астероида
	/// </summary>
	public interface IAsteroid
	{
		event Action<IAsteroid, Vector3> CollisionPerformed;
		event Action<IAsteroid> BecomeInvisible;
		
		float Speed { get; set; }

		void SelfDestroy();
	}
}
