using System;
using TestDI.Demo;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Корабль
	/// </summary>
	public class SpaceShip : MonoBehaviour, ISpaceShip
	{
		[SerializeField] 
		private float _speed = 1.0f;

		[SerializeField] 
		private int _hp = 3;

		public int Hp
		{
			get { return _hp; }
			set
			{
				int prevHp = _hp;
				_hp = value;
				if (_hp != prevHp)
				{
					var evt = HpChanged;
					if (evt != null)
						evt();
				}
			}
		}

		public float Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		public Vector3 Pos
		{
			get { return transform.position; }
			set { transform.position = value; }
		}

		public event Action HpChanged;

		/// <summary>
		/// Показать/спрятать модельку корабля
		/// </summary>
		public void SetVisible(bool visible)
		{
			gameObject.SetActive(visible);
		}
	}
}