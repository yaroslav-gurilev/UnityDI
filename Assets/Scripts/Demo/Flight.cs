using System;
using TestDI.Demo.Asteroids;
using UnityDI;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Класс, контроллирующий полет
	/// </summary>
	public class Flight : IFlight, IDependent
	{
		[Dependency]
		public IAsteroidField AsteroidField { private get; set; }
		
		[Dependency]
		public ISpaceShip SpaceShip { private get; set; }

		[Dependency]
		public IShipController ShipController { private get; set; }

		public event Action GameFinished;

		public void OnInjected()
		{
			SpaceShip.HpChanged += OnHpChanged;
		}

		public void Update(float deltaTime)
		{
			ShipController.Update(deltaTime);
			AsteroidField.Update(deltaTime);
		}
		
		public void StartNewFlight()
		{
			AsteroidField.StartNewFlight();
			ShipController.StartNewFlight();
		}

		private void OnHpChanged()
		{
			if (SpaceShip.Hp <= 0)
			{
				var evt = GameFinished;
				if (evt != null)
				{
					evt();
				}
			}
		}
	}
}
