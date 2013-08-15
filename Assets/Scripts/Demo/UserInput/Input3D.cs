using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// Класс, проецирующий прикосновения к экрану на игровой мир
	/// </summary>
	public class Input3D : IInput3D
	{
		private List<Touch3D> _touches = new List<Touch3D>();
		private readonly InputController _input = new InputController();

		public IEnumerable<Touch3D> Touches { get { return _touches; } } 

		public void Update()
		{
			_input.Update();

			var newTouches = new List<Touch3D>();
			foreach (var touch2D in _input.Touches)
			{
				var ray = Camera.main.ScreenPointToRay(new Vector3(touch2D.Position.x, touch2D.Position.y));

				RaycastHit hitSurface;
				int surfaceLayerMask = 1 << LayerMask.NameToLayer("Surface");
				if (!Physics.Raycast(ray, out hitSurface, 100, surfaceLayerMask))
					continue;
				var touch3D = new Touch3D
				{
					Touch2D = touch2D,
					Hit = hitSurface,
					Ray = ray
				};
				var prevTouch = _touches.FirstOrDefault(t => t.Touch2D.Id == touch2D.Id);
				if (prevTouch != null)
				{
					touch3D.PrevHit = prevTouch.Hit;
					touch3D.PrevRay = prevTouch.Ray;
				}
				else
				{
					touch3D.TouchFirstFrame = true;
				}
				newTouches.Add(touch3D);
			}
			_touches = newTouches;
		}
	}
}
