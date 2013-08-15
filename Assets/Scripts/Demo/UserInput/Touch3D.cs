using UnityEngine;

namespace TestDI.Demo.UserInput
{
	/// <summary>
	/// Проекция прикосновения на игровое поле
	/// </summary>
	public class Touch3D
	{
		public Touch Touch2D { get; set; }
 
		public RaycastHit Hit { get; set; }

		public Ray Ray { get; set; }
		
		public RaycastHit PrevHit { get; set; }

		public Ray PrevRay { get; set; }

		public bool TouchFirstFrame { get; set; }
	}
}
