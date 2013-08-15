using System.Linq;
using UnityDI.Finders;
using UnityEngine;

namespace UnityDI.Providers
{
	public class ScenePathProvider<T> : IObjectProvider<T> where T:class
	{
		private readonly string _path;

		public ScenePathProvider(string path)
		{
			_path = path;
		}

		public T GetObject(Container container)
		{
			var gameObject = new MaskFinder().Find(_path);
			if (gameObject == null)
				throw new ContainerException("Can't find game object \"" + _path + "\"");
		
			if (typeof (T) == typeof (GameObject))
			{
				return (T)(object)gameObject;
			}
			
			if (typeof (T) == typeof (Transform))
			{
				return (T)(object)gameObject.transform;
			}

			var components = gameObject.GetComponents<Component>();
			T component = components.OfType<T>().FirstOrDefault();
			if (component != null)
				return component;

			throw new ContainerException("Can't find component \"" + typeof(T).FullName + "\" of game object \"" + _path + "\"");
		}
	}
}
