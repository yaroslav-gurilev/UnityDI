using TestDI.Demo.Asteroids;
using TestDI.Demo.UI;
using TestDI.Demo.UserInput;
using UnityDI;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Класс, запускающий игру
	/// </summary>
	public class GameStarter : MonoBehaviour
	{
		private Container _container;
		private GameMain _gameMain;

		public void Start()
		{
			SetupContainer();

			_gameMain = _container.Resolve<GameMain>();
		}

		public void Update()
		{
			_gameMain.Update(Time.deltaTime);
		}

		private void SetupContainer()
		{
			_container = new Container();
			_container.RegisterType<GameMain>();
			_container.RegisterType<TimeSlicer>();
			_container.RegisterType<IInput3D, Input3D>();
			_container.RegisterType<IShipController, ShipController>();
			_container.RegisterType<IAsteroidField, AsteroidField>();
			_container.RegisterSingleton<IFlight, Flight>();
			_container.RegisterSceneObject<IAsteroidFactory>("AsteroidRespawn");
			_container.RegisterSceneObject<ISpaceShip>("Ship");
			_container.RegisterSceneObject<IFxPlayer>("Effects");
			_container.RegisterSceneObject<IMainMenu>("Interface");
			_container.RegisterSceneObject<IHud>("Interface");
		}
	}
}
