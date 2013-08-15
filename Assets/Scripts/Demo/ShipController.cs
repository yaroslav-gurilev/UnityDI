using System.Linq;
using TestDI.Demo.UI;
using TestDI.Demo.UserInput;
using UnityDI;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Класс, передающий кораблю сигналы управления
	/// </summary>
	public class ShipController : IShipController, IDependent
	{
		[Dependency]
		public ISpaceShip SpaceShip { private get; set; }

		[Dependency]
		public IInput3D Input3D { private get; set; }

		[Dependency]
		public IHud Hud { private get; set; }

		[Dependency]
		public IFxPlayer FxPlayer { private get; set; }
		

		public void OnInjected()
		{
			SpaceShip.HpChanged += OnHpChanged;
		}

		public void StartNewFlight()
		{
			SpaceShip.Hp = 3;
			SpaceShip.SetVisible(true);

			Hud.Score = 0;
			Hud.SetVisible(true);
		}
		
		public void Update(float deltaTime)
		{
			Input3D.Update();
			var touch = Input3D.Touches.FirstOrDefault();
			if (touch != null)
			{
				MoveTo(touch.Hit.point);
			}

			Hud.Score += (int)(100.0f * deltaTime);
		}

		/// <summary>
		/// Двигаться в указанную точку
		/// </summary>
		private void MoveTo(Vector3 pos)
		{
			Vector3 v = pos - SpaceShip.Pos;
			Vector3 dir = v.normalized * SpaceShip.Speed * Time.deltaTime;
			if (dir.sqrMagnitude > v.sqrMagnitude)
			{
				SpaceShip.Pos = pos;
			}
			else
			{
				SpaceShip.Pos += dir;
			}
		}

		private void OnHpChanged()
		{
			Hud.Hp = SpaceShip.Hp;
			if (SpaceShip.Hp <= 0)
			{
				SpaceShip.SetVisible(false);
				Hud.SetVisible(false);

				FxPlayer.PlayBigBlast(SpaceShip.Pos);
			}
		}
	}
}
