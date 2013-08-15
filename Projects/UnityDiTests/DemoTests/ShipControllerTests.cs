using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestDI.Demo;
using TestDI.Demo.UI;
using TestDI.Demo.UserInput;
using UnityDI;

namespace UnityDiTests.DemoTests
{
	/// <summary>
	/// Тесты для класса ShipController
	/// </summary>
	[TestClass]
	public class ShipControllerTests
	{
		/// <summary>
		/// Значения сбрасываются при начале новой игры
		/// </summary>
		[TestMethod]
		public void ResetValuesOnStart()
		{
			var spaceShip = new Mock<ISpaceShip>();
			var hud = new Mock<IHud>();

			var container = new Container();
			container.RegisterType<ShipController>();
			container.RegisterInstance(new Mock<IInput3D>().Object);
			container.RegisterInstance(spaceShip.Object);
			container.RegisterInstance(hud.Object);
			container.RegisterInstance(new Mock<IFxPlayer>().Object);

			var controller = container.Resolve<ShipController>();
			controller.StartNewFlight();
			spaceShip.VerifySet(s => s.Hp = 3);
			hud.VerifySet(h => h.Score = 0);
		}

		/// <summary>
		/// Опрашивается состояние ввода
		/// </summary>
		[TestMethod]
		public void InputUpdated()
		{
			var input = new Mock<IInput3D>();

			var container = new Container();
			container.RegisterType<ShipController>();
			container.RegisterInstance(input.Object);
			container.RegisterInstance(new Mock<ISpaceShip>().Object);
			container.RegisterInstance(new Mock<IHud>().Object);
			container.RegisterInstance(new Mock<IFxPlayer>().Object);

			var controller = container.Resolve<ShipController>();
			controller.StartNewFlight();
			controller.Update(2.0f);
			input.Verify(i => i.Update(), Times.Once());
		}

		/// <summary>
		/// За секунду полета начисляется 100 очков
		/// </summary>
		[TestMethod]
		public void GainScore()
		{
			var hud = new Mock<IHud>();

			var container = new Container();
			container.RegisterType<ShipController>();
			container.RegisterInstance(new Mock<IInput3D>().Object);
			container.RegisterInstance(new Mock<ISpaceShip>().Object);
			container.RegisterInstance(hud.Object);
			container.RegisterInstance(new Mock<IFxPlayer>().Object);

			var controller = container.Resolve<ShipController>();
			controller.StartNewFlight();
			controller.Update(2.0f);

			hud.VerifySet(h => h.Score = 200);
		}
	}
}
