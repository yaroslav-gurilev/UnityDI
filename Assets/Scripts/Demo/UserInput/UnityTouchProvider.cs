using System.Collections.Generic;
using UnityEngine;

namespace TestDI.Demo.UserInput
{
	public class UnityTouchProvider : ITouchProvider
	{
		private readonly List<Touch> _touches = new List<Touch>();

		public IEnumerable<Touch> Touches { get { return _touches; } }
		
		public void Update()
		{
			_touches.Clear();
			foreach (var touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
					continue;

				_touches.Add(new Touch
				{
					Id = touch.fingerId,
					Position = touch.position,
					DeltaPosition = touch.deltaPosition,
					DeltaTime = touch.deltaTime,
					TouchFirstFrame = (touch.phase == TouchPhase.Began),
				});
			}
		}
	}
}