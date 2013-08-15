using System;

namespace TestDI.Demo.UI
{
	/// <summary>
	/// Интерфейс главного меню
	/// </summary>
	public interface IMainMenu
	{
		/// <summary>
		/// Сообщение о начале новой игры
		/// </summary>
		event Action StartNewGame;
		
		/// <summary>
		/// Очки, набранные игроком
		/// </summary>
		int Score {get; set;}
		
		/// <summary>
		/// Показать/спрятать меню
		/// </summary>
		void SetVisible(bool visible);
	}
}

