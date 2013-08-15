namespace TestDI.Demo.UI
{
	/// <summary>
	/// Внутриигровой интерфейс
	/// </summary>
	public interface IHud
	{
		int Hp { get; set; }
		
		int Score { get; set; }
		
		/// <summary>
		/// Показать/спрятать меню
		/// </summary>
		void SetVisible(bool visible);
	}
}

