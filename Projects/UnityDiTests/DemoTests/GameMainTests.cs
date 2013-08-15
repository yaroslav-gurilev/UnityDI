using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDI.Demo;
using TestDI.Demo.UI;
using UnityDI;

namespace UnityDiTests.DemoTests
{
	/// <summary>
	/// Примеры тестов для класса GameMain
	/// </summary>
	[TestClass]
	public class GameMainTests
	{
		/// <summary>
		/// В начале игры главное меню должно быть видно
		/// </summary>
		[TestMethod]
		public void MainMenuVisible()
		{
			var mainMenu = new Mock<IMainMenu>();

			var container = new Container();
			container.RegisterType<GameMain>();
			container.RegisterInstance(mainMenu.Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(new Mock<IFlight>().Object);

			container.Resolve<GameMain>();
			mainMenu.Verify(m => m.SetVisible(true), Times.Once());
		}

		/// <summary>
		/// В начале игры состояние полета не должно обновляться
		/// </summary>
		[TestMethod]
		public void FlightNotUpdated()
		{
			var flight = new Mock<IFlight>();

			var container = new Container();
			container.RegisterType<GameMain>();
			container.RegisterInstance(new Mock<IMainMenu>().Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(flight.Object);

			var gameMain = container.Resolve<GameMain>();
			gameMain.Update(1.0f);
			flight.Verify(f => f.StartNewFlight(), Times.Never());
			flight.Verify(f => f.Update(It.IsAny<float>()), Times.Never());
		}

		/// <summary>
		/// Игра начинается по событию из главного меню
		/// </summary>
		[TestMethod]
		public void FlightStartedFromMainMenu()
		{
			var mainMenu = new Mock<IMainMenu>();
			var flight = new Mock<IFlight>();

			var container = new Container();
			container.RegisterType<GameMain>();
			container.RegisterInstance(mainMenu.Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(flight.Object);

			var gameMain = container.Resolve<GameMain>();
			mainMenu.Raise(m => m.StartNewGame += null);

			flight.Verify(f => f.StartNewFlight(), Times.Once());
			gameMain.Update(1.0f);
			flight.Verify(f => f.Update(1.0f), Times.Once());
		}

		/// <summary>
		/// После запуска игры главное меню не видно
		/// </summary>
		[TestMethod]
		public void MainMenuInvisibleAfterFlightStarted()
		{
			var mainMenu = new Mock<IMainMenu>();

			var container = new Container();
			container.RegisterType<GameMain>();
			container.RegisterInstance(mainMenu.Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(new Mock<IFlight>().Object);

			var gameMain = container.Resolve<GameMain>();
			mainMenu.Raise(m => m.StartNewGame += null);
			mainMenu.Verify(m => m.SetVisible(false), Times.Once());
		}

		/// <summary>
		/// Главное меню опять становится видимым после окончания полета
		/// </summary>
		[TestMethod]
		public void MainMenuVisibleWhenFlightFinished()
		{
			var mainMenu = new Mock<IMainMenu>();
			var flight = new Mock<IFlight>();

			var container = new Container();
			container.RegisterType<GameMain>();
			container.RegisterInstance(mainMenu.Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(flight.Object);

			var gameMain = container.Resolve<GameMain>();
			mainMenu.Raise(m => m.StartNewGame += null);
			flight.Raise(f => f.GameFinished += null);
			mainMenu.Verify(m => m.SetVisible(true), Times.Exactly(2));
		}
	}
}
