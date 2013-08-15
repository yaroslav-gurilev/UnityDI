using UnityEngine;

namespace TestDI.Demo.UI
{
	/// <summary>
	/// Реализация внутриигровой интерфейс
	/// </summary>
	public class Hud : MonoBehaviour, IHud
	{
		private bool _inited;
		private bool _visible;
		private float _hudHeight;
		private Rect _hudRect;
		private Rect _leftRect;
		private Rect _rightRect;
		private GUIStyle _labelStyle;
		
		public int Hp { get; set; }
		
		public int Score { get; set; }
		
		public void OnGUI()
		{
			if (!_inited)
			{
				Init();
				_inited = true;
			}
			
			if (!_visible)
				return;
			
			GUI.BeginGroup (_hudRect);
			GUI.Box(_hudRect, "");
			GUI.Label(_leftRect, string.Format("Health: {0}", Hp), _labelStyle);
			GUI.Label(_rightRect, string.Format("Score: {0}", Score), _labelStyle);
			GUI.EndGroup ();
		}
		
		/// <summary>
		/// Показать/спрятать меню
		/// </summary>
		public void SetVisible(bool visible)
		{
			_visible = visible;
		}
		
		private void Init()
		{
			_hudHeight = Screen.height * 0.1f;
			_hudRect = new Rect(0, 0, Screen.width, _hudHeight);
			_leftRect = new Rect(0, 0, Screen.width * 0.5f, _hudHeight);  
			_rightRect = new Rect(Screen.width * 0.5f, 0, Screen.width * 0.5f, _hudHeight);
			
			_labelStyle = new GUIStyle(GUI.skin.label);
			_labelStyle.fontSize = 20;
			_labelStyle.alignment = TextAnchor.MiddleCenter;
		}
	}
}

