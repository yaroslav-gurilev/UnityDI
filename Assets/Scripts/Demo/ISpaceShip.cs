using System;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Интерфейс космического корабля
	/// </summary>
	public interface ISpaceShip
	{
		int Hp { get; set; }
		
		float Speed { get; set; }
		
		Vector3 Pos { get; set; }

		event Action HpChanged;

		/// <summary>
		/// Показать/спрятать модельку корабля
		/// </summary>
		void SetVisible(bool visible);
	}
}
