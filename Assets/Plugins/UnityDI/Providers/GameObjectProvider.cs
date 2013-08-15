using UnityEngine;

namespace UnityDI.Providers
{
	public class GameObjectProvider<T> : IObjectProvider<T> where T : UnityEngine.Object
	{
		public T GetObject(Container container)
		{
			var obj = GameObject.FindObjectOfType(typeof (T));
			if (obj == null)
				throw new ContainerException("Can't find component \"" + typeof(T).FullName + "\"!");
			return (T)obj;
		}
	}
}
