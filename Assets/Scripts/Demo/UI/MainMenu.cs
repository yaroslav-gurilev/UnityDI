using UnityEngine;
using System;

namespace TestDI.Demo.UI
{
	/// <summary>
	/// Главное меню
	/// </summary>
	public class MainMenu : MonoBehaviour, IMainMenu
	{
		private bool _inited;
		private bool _visible;
		private int _menuWidth; 
		private int _menuHeight;
		private GUIStyle _buttonStyle;
		private GUIStyle _boxStyle;
		private GUIStyle _labelStyle;
	
		private Rect _menuRect;
		private Rect _buttonRect;
		private Rect _labelRect;
		
		/// <summary>
		/// Очки, набранные игроком
		/// </summary>
		public int Score {get; set;}
		
		/// <summary>
		/// Сообщение о начале новой игры
		/// </summary>
		public event Action StartNewGame;
		
		public void OnGUI()
		{
			if (!_inited)
			{
				Init();
				_inited = true;
			}
			
			if (!_visible)
				return;
			
			GUI.BeginGroup (new Rect ((Screen.width - _menuWidth)/2, (Screen.height - _menuHeight)/2, _menuWidth, _menuHeight));
			GUI.Box (_menuRect, "Space Flight", _boxStyle);
			GUI.Label(_labelRect, string.Format("You score: {0}", Score), _labelStyle);
			if (GUI.Button (_buttonRect, "Start new game", _buttonStyle))
			{
				Debug.Log ("Start new game");
				var evt = StartNewGame;
				if (evt != null)
					evt();
			}
			
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
			_menuWidth = Screen.width/2; 
			_menuHeight = Screen.height/3;
			
			_menuRect = new Rect (0,0, _menuWidth, _menuHeight);
			
			_buttonRect = new Rect (_menuWidth * 0.1f, _menuHeight * 0.65f, _menuWidth * 0.8f, _menuHeight * 0.2f);
				
			_labelRect = new Rect (_menuWidth * 0.1f, _menuHeight * 0.4f, _menuWidth * 0.8f, _menuHeight * 0.2f);
			
			_buttonStyle = new GUIStyle(GUI.skin.button);
			_buttonStyle.active.textColor = Color.red;
			_buttonStyle.fontSize = 30;
			_buttonStyle.alignment = TextAnchor.MiddleCenter;
			
			_boxStyle = new GUIStyle(GUI.skin.box);
			_boxStyle.normal.textColor = Color.red;
			_boxStyle.fontSize = 60;
			_boxStyle.fontStyle = FontStyle.Bold;
			_boxStyle.alignment = TextAnchor.UpperCenter;
				
			_labelStyle = new GUIStyle(GUI.skin.label);
			_labelStyle.fontSize = 20;
			_labelStyle.alignment = TextAnchor.MiddleCenter;
		}
	}
}

