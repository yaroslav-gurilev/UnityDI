using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityDI.Providers;

namespace UnityDI
{
	/// <summary>
	/// Класс DI-контейнера
	/// </summary>
    public class Container
    {
		private readonly Dictionary<ContainerKey, IProviderWrapper> _providers = new Dictionary<ContainerKey, IProviderWrapper>();
 		
		/// <summary>
		/// Зарегистрировать тип
		/// При каждом обращении Resolve&lt;T&gt;() будет создаваться новый объект типа T
		/// </summary>
		public Container RegisterType<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new ActivatorObjectProvider<T>(), name);
		}

		/// <summary>
		/// Зарегистрировать тип под видом базового
		/// При каждом обращении Resolve&lt;TBase&gt;() будет создаваться новый объект TDerived
		/// </summary>
		public Container RegisterType<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new ActivatorObjectProvider<TDerived>(), name);
		}

		/// <summary>
		/// Зарегистрировать синглтон
		/// При каждом обращении Resolve&lt;T&gt;() будет возвращаться ссылка на один и тот же объект типа T
		/// </summary>
		public Container RegisterSingleton<T>(string name = null) where T : class, new()
		{
			return RegisterProvider(new SingletonProvider<T>(), name);
		}

		/// <summary>
		/// Зарегистрировать синглтон
		/// При каждом обращении Resolve&lt;TBase&gt;() будет возвращаться ссылка на один и тот же объект типа TDerived
		/// </summary>
		public Container RegisterSingleton<TBase, TDerived>(string name = null) where TDerived : class, TBase, new()
		{
			return RegisterProvider<TBase, TDerived>(new SingletonProvider<TDerived>(), name);
		}

		/// <summary>
		/// Зарегистрировать объект типа T
		/// При каждом обращении Resolve&lt;T&gt;() будет возвращаться ссылка на переданный объект
		/// </summary>
		public Container RegisterInstance<T>(T obj, string name = null) where T : class
		{
			return RegisterProvider(new InstanceProvider<T>(obj), name);
		}

		/// <summary>
		/// Зарегистрировать объект типа TDerived
		/// При каждом обращении Resolve&lt;TBase&gt;() будет возвращаться ссылка на переданный объект
		/// </summary>
		public Container RegisterInstance<TBase, TDerived>(TDerived obj, string name = null) where TDerived : class, TBase
		{
			return RegisterProvider<TBase, TDerived>(new InstanceProvider<TDerived>(obj), name);
		}

		/// <summary>
		/// Зарегистрировать путь в дереве сцены
		/// При каждом обращении Resolve&lt;T&gt;() будет объект типа T, найденный по пути в дереве сцены
		/// Путь может указывать на неактивный объект, однако должен начинаться с активного объекта!
		/// В пути может встречаться символ '*', что обозначает первый активный объект
		/// </summary>
		public Container RegisterSceneObject<T>(string path, string name = null) where T : class
		{
			return RegisterProvider(new ScenePathProvider<T>(path), name);
		}
		
		public Container RegisterProvider<T>(IObjectProvider<T> provider, string name = null) where T : class
		{
			var key = new ContainerKey(typeof (T), name);
			_providers[key] = new ProviderWrapper<T>(provider);
			return this;
		}

		public Container RegisterProvider<TBase, TDerived>(IObjectProvider<TDerived> provider, string name = null) where TDerived : class, TBase
		{
			var key = new ContainerKey(typeof (TBase), name);
			_providers[key] = new ProviderWrapper<TDerived>(provider);
			return this;
		}
	
		/// <summary>
		/// Получить объект нужного типа
		/// </summary>
		public T Resolve<T>(string name = null)
		{
			return (T) Resolve(typeof(T), name);
		}
		
		/// <summary>
		/// Получить объект нужного типа
		/// </summary>
		public object Resolve(Type type, string name = null)
		{
			IProviderWrapper provider;
			if (!_providers.TryGetValue(new ContainerKey(type, name), out provider))
				throw new ContainerException("Can't resolve type " + type.FullName + (name == null ? "" : " registered with name \"" + name + "\""));
			return provider.GetObject(this);
		}

		/// <summary>
		/// Заинжектить зависимости в уже существующий объект
		/// </summary>
		public void BuildUp(object obj)
		{
			Type type = obj.GetType();

			MemberInfo[] members = type.FindMembers(MemberTypes.Property,
				BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance,
				null, null);

			foreach (MemberInfo member in members)
			{
				var attrs = member.GetCustomAttributes(typeof (DependencyAttribute), true);
				if (!attrs.Any())
					continue;

				var attrib = (DependencyAttribute)attrs[0];
				var propertyInfo = (PropertyInfo)member;
				object valueObj;
				
				try
				{
					valueObj = Resolve(propertyInfo.PropertyType, attrib.Name);
				}
				catch (ContainerException ex)
				{
					throw new ContainerException("Can't resolve property \"" + propertyInfo.Name + "\" of class \"" + type.FullName + "\"", ex);
				}
				
				propertyInfo.SetValue(obj, valueObj, null);
			}

			var dependent = obj as IDependent;
			if (dependent != null)
				dependent.OnInjected();
		}
    }
}
