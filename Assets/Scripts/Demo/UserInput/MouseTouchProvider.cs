using System.Collections.Generic;
using UnityEngine;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// Эмулируем прикосновения к экрану мышью
	/// </summary>
	public class MouseTouchProvider : ITouchProvider
	{
		private readonly List<Touch> _touches = new List<Touch>();
		private Vector2 _prevMouseClick;
		private float _prevMouseClickTime;

		public IEnumerable<Touch> Touches { get { return _touches; } }

		public void Update()
		{
			_touches.Clear();

			if (!Input.GetMouseButton(0)) 
				return;
			
			var touch = new Touch
			{
				Id = 1,
				Position = new Vector2(Input.mousePosition.x, Input.mousePosition.y),
				TouchFirstFrame = Input.GetMouseButtonDown(0)
			};

			if (!touch.TouchFirstFrame)
			{
				touch.DeltaPosition = touch.Position - _prevMouseClick;
				touch.DeltaTime = Time.time - _prevMouseClickTime;
			}
			_touches.Add(touch);
			_prevMouseClick = touch.Position;
			_prevMouseClickTime = Time.time;
		}
	}
}