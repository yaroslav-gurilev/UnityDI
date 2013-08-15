using System;
using UnityDI;
using UnityEngine;

namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Менеджер астероидов
	/// </summary>
	public class AsteroidField : IAsteroidField, IDependent
	{
		private float _flightTime;

		[Dependency]
		public IFxPlayer FxPlayer { private get; set; }
		[Dependency]
		public TimeSlicer TimeSlicer { private get; set; }
		[Dependency]
		public IAsteroidFactory AsteroidFactory { private get; set; }
		[Dependency]
		public ISpaceShip SpaceShip { private get; set; }
		
		public void OnInjected()
		{
			TimeSlicer.Timeout = 1.0f;
		}

		public void StartNewFlight()
		{
			_flightTime = 0.0f;
		}

		public void Update(float deltaTime)
		{
			_flightTime += deltaTime;

			if (TimeSlicer.Update(deltaTime))
			{
				Add(AsteroidFactory.CreateAsteroid());
			}

			TimeSlicer.Timeout = 1.0f / Math.Max(1.0f, _flightTime / 15.0f);
		}

		private void Add(IAsteroid asteroid)
		{
			asteroid.BecomeInvisible += OnAsteroidInvisible;
			asteroid.CollisionPerformed += OnCollisionDetected;

			asteroid.Speed = Math.Max(1.0f, _flightTime / 3.0f);
		}

		private void OnCollisionDetected(IAsteroid asteroid, Vector3 pos)
		{
			FxPlayer.PlaySmallBlast(pos);
			RemoveAsteroid(asteroid);
			SpaceShip.Hp--;
		}

		private void OnAsteroidInvisible(IAsteroid asteroid)
		{
			RemoveAsteroid(asteroid);
		}

		private void RemoveAsteroid(IAsteroid asteroid)
		{
			asteroid.BecomeInvisible -= OnAsteroidInvisible;
			asteroid.CollisionPerformed -= OnCollisionDetected;
			asteroid.SelfDestroy();
		}
	}
}
