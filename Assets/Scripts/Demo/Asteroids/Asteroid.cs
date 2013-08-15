using System;
using UnityEngine;

namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Астероид
	/// </summary>
	public class Asteroid : MonoBehaviour, IAsteroid
	{
		private bool _isVisiblePrev;

		[SerializeField] private float _speed = 1.0f;

		public float Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		public event Action<IAsteroid, Vector3> CollisionPerformed;
		public event Action<IAsteroid> BecomeInvisible;

		public void Update()
		{
			Move();
			CheckInvisible();
		}

		public void SelfDestroy()
		{
			GameObject.Destroy(gameObject);
		}

		public void OnCollisionEnter(Collision collision)
		{
			Debug.Log("collision with " + collision.contacts[0].otherCollider.name);
			var evt = CollisionPerformed;
			if (evt != null)
			{
				evt(this, collision.contacts[0].point);
			}
		}

		private void CheckInvisible()
		{
			bool visible = GetComponent<MeshRenderer>().isVisible;
			if (_isVisiblePrev && !visible)
			{
				var evt = BecomeInvisible;
				if (evt != null)
					evt(this);
			}
			_isVisiblePrev = visible;
		}

		private void Move()
		{
			var pos = transform.position;
			pos.y -= Speed * Time.deltaTime;
			transform.position = pos;
		}
	}
}