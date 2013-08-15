using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDI.Demo;
using TestDI.Demo.Asteroids;
using UnityDI;

namespace UnityDiTests.DemoTests
{
	/// <summary>
	/// Юнит-тесты для класса Flight
	/// </summary>
	[TestClass]
	public class FlightTests
	{
		/// <summary>
		/// Вызов StartNewFlight() передается в AsteroidField и ShipController
		/// </summary>
		[TestMethod]
		public void StartNewFlightPropagated()
		{
			var field = new Mock<IAsteroidField>();
			var shipController = new Mock<IShipController>();

			var container = new Container();
			container.RegisterType<Flight>();
			container.RegisterInstance(field.Object);
			container.RegisterInstance(new Mock<ISpaceShip>().Object);
			container.RegisterInstance(shipController.Object);

			var flight = container.Resolve<Flight>();
			flight.StartNewFlight();
			field.Verify(f => f.StartNewFlight(),Times.Once());
			shipController.Verify(s => s.StartNewFlight(),Times.Once());
		}

		/// <summary>
		/// Вызов Update() передается в AsteroidField и ShipController
		/// </summary>
		[TestMethod]
		public void UpdatePropagated()
		{
			var field = new Mock<IAsteroidField>();
			var shipController = new Mock<IShipController>();

			var container = new Container();
			container.RegisterType<Flight>();
			container.RegisterInstance(field.Object);
			container.RegisterInstance(new Mock<ISpaceShip>().Object);
			container.RegisterInstance(shipController.Object);

			var flight = container.Resolve<Flight>();
			flight.StartNewFlight();
			flight.Update(2.0f);
			field.Verify(f => f.Update(2.0f), Times.Once());
			shipController.Verify(s => s.Update(2.0f), Times.Once());
		}

		/// <summary>
		/// Если HP корабля упали до нуля, файрится событие GameFinished
		/// </summary>
		[TestMethod]
		public void GameFinishedFired()
		{
			var field = new Mock<IAsteroidField>();
			var ship = new Mock<ISpaceShip>();

			var container = new Container();
			container.RegisterType<Flight>();
			container.RegisterInstance(field.Object);
			container.RegisterInstance(ship.Object);
			container.RegisterInstance(new Mock<IShipController>().Object);

			var flight = container.Resolve<Flight>();
			flight.StartNewFlight();
			bool gameFinishedFired = false;
			flight.GameFinished += () => { gameFinishedFired = true; };
			ship.SetupGet(s => s.Hp).Returns(0);
			ship.Raise(s => s.HpChanged += null);

			Assert.AreEqual(true, gameFinishedFired);
		}
	}
}
