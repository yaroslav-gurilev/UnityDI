using UnityEngine;

namespace UnityDI.Finders
{
	/// <summary>
	/// Класс, реализующий дефолтный алгоритм поиска
	/// </summary>
	public class DefaultFinder : IGameObjectFinder
	{
		public GameObject Find(string path)
		{
			var gameObject = GameObject.Find(path);
			if (gameObject == null)
				throw new ContainerException("Can't find game object \"" + path + "\"");
			return gameObject;
		}
	}
}
