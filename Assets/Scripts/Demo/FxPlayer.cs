using System.Collections.Generic;
using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Проигрыватель эффектов
	/// </summary>
	public class FxPlayer : MonoBehaviour, IFxPlayer
	{
		public GameObject SmallBlastPrefab;

		public GameObject BigBlastPrefab;

		private readonly List<ParticleSystem> _particles = new List<ParticleSystem>();
		private int _frameCounter;

		/// <summary>
		/// Проиграть малый взрыв в указанной точке
		/// </summary>
		public void PlaySmallBlast(Vector3 pos)
		{
			CreateBlast(pos, SmallBlastPrefab);
		}

		/// <summary>
		/// Проиграть большой взрыв в указанной точке
		/// </summary>
		public void PlayBigBlast(Vector3 pos)
		{
			CreateBlast(pos, BigBlastPrefab);
		}

		private void CreateBlast(Vector3 pos, GameObject blastPrefab)
		{
			var obj = (GameObject) GameObject.Instantiate(blastPrefab, pos, Quaternion.identity);
			obj.transform.parent = gameObject.transform;
			var particleSystem = obj.GetComponent<ParticleSystem>();
			_particles.Add(particleSystem);
		}

		public void Update()
		{
			++_frameCounter;
			if (_frameCounter % 1000 == 0)
			{
				RemoveStoppedParticles();
			}
		}

		private void RemoveStoppedParticles()
		{
			for (int i = 0; i < _particles.Count;)
			{
				var ps = _particles[i];
				if (ps.isPlaying)
				{
					++i;
					continue;
				}

				_particles.RemoveAt(i);
				GameObject.Destroy(ps.gameObject);
			}
		}
	}
}
