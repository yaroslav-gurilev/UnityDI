using UnityDI.Providers;

namespace UnityDI
{
	class ProviderWrapper<T> : IProviderWrapper where T:class
	{
		private readonly IObjectProvider<T> _provider;

		public ProviderWrapper(IObjectProvider<T> provider)
		{
			_provider = provider;
		}

		public object GetObject(Container container)
		{
			return _provider.GetObject(container);
		}
	}
}
