using System.Collections.Generic;
using UnityEngine;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// В связи с тем, что Unity не поддерживает эмуляцию Input.touches в редакторе, придется делать это самому
	/// </summary>
	public class InputController
	{
		private readonly ITouchProvider _provider;

		public IEnumerable<Touch> Touches { get { return _provider.Touches;  } }

		public InputController()
		{
		#if  (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
			Debug.Log("Using native touches");
			_provider = new UnityTouchProvider();
		#else
			Debug.Log("Emulating touches from mouse");
			_provider = new MouseTouchProvider();
		#endif
		}

		/// <summary>
		/// Обновить состояние ввода
		/// </summary>
		public void Update()
		{
			_provider.Update();
		}
	}
}
