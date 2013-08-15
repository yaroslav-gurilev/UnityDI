using UnityDI;
using TestDI.Demo.UI;

namespace TestDI.Demo
{
	/// <summary>
	/// Главный класс игры
	/// </summary>
	public class GameMain : IDependent
	{
		private bool _flightInProgress;

		[Dependency]
		public IFlight Flight { private get; set; }

		[Dependency]
		public IMainMenu MainMenu { private get; set; }

		[Dependency]
		public IHud Hud { private get; set; }

		public void OnInjected()
		{
			MainMenu.StartNewGame += OnStartNewGame;
			Flight.GameFinished += OnGameFinished;
			MainMenu.SetVisible(true);
		}

		public void Update(float deltaTime)
		{
			if (_flightInProgress)
				Flight.Update(deltaTime);
		}

		private void OnStartNewGame()
		{
			MainMenu.SetVisible(false);
			Flight.StartNewFlight();
			_flightInProgress = true;
		}

		private void OnGameFinished()
		{
			_flightInProgress = false;
			MainMenu.Score = Hud.Score;
			MainMenu.SetVisible(true);
		}
	}
}