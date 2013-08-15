using System;

namespace UnityDI.Providers
{
	public class SingletonProvider<T> : IObjectProvider<T> where T : class, new()
	{
		private bool _inited;
		private T _instance;
		
		public T GetObject(Container container)
		{
			if (!_inited)
			{
				_instance  = Activator.CreateInstance<T>();
				_inited = true;
				container.BuildUp(_instance);
			}
			return _instance;
		}
	}
}
