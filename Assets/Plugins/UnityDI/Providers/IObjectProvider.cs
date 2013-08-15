namespace UnityDI.Providers
{
	public interface IObjectProvider<T> where T : class
	{
		T GetObject(Container container);
	}
}
