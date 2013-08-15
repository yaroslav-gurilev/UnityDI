using UnityEngine;

namespace TestDI.Demo.Asteroids
{
	/// <summary>
	/// Фабрика астероидов
	/// </summary>
	public class AsteroidFactory : MonoBehaviour, IAsteroidFactory
	{
		private GameObject _spawnNode;
		public GameObject Prefab;


		public void Start()
		{
			_spawnNode = GameObject.Find("AsteroidRespawn");
		}

		/// <summary>
		/// Создать новый астероид
		/// </summary>
		public IAsteroid CreateAsteroid()
		{
			var asteroid = (GameObject) GameObject.Instantiate(Prefab);
			asteroid.transform.position = GetAsteroidPos();
			asteroid.transform.parent = gameObject.transform;
			return (IAsteroid) asteroid.GetComponent<Asteroid>();
		}

		private Vector3 GetAsteroidPos()
		{
			var box = (BoxCollider) _spawnNode.collider;
			var center = _spawnNode.transform.TransformPoint(box.center);
			float x = Random.Range(center.x - box.size.x / 2, center.x + box.size.x / 2);
			float y = Random.Range(center.y - box.size.y / 2, center.y + box.size.y / 2);
			return new Vector3(x, y, 0.0f);
		}
	}
}