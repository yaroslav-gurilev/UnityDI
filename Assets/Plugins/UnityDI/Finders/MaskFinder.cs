using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityDI.Finders
{
	/// <summary>
	/// Класс, реализующий поиск игровых объектов в дереве сцены по пути
	/// Путь должен начинаться с указания активного объекта!
	/// В пути может встречаться символ '*', что обозначает первый активный объект
	/// </summary>
	public class MaskFinder : IGameObjectFinder
	{
		private const string PathSeparator = "/";
		private const string AnyActiveMask = "*";
		private GameObject[] _gameObjects;

		public GameObject Find(string path)
		{
			bool rootSearch;
			var tokens = SplitPath(path, out rootSearch);
			return FindGameObject(tokens, rootSearch);
		}

		private IEnumerable<GameObject> GameObjects
		{
			get { return UnityEngine.Object.FindObjectsOfType(typeof(GameObject)).Cast<GameObject>(); }
		}

		private string[] SplitPath(string path, out bool searchFromRoot)
		{
			string[] tokens;
			if (path.StartsWith(PathSeparator))
			{
				searchFromRoot = true;
				tokens = path.Substring(1).Split(new[] { PathSeparator }, StringSplitOptions.None);
			}
			else
			{
				searchFromRoot = false;
				tokens = path.Split(new[] { PathSeparator }, StringSplitOptions.None);
			}
			if (tokens.Length == 0)
				throw new ContainerException("Invalid path: " + path);

			if (tokens.Any(string.IsNullOrEmpty))
				throw new ContainerException("Invalid path: " + path);

			if (tokens[0] == AnyActiveMask)
				throw new ContainerException("Path can't start with symbol \"" + AnyActiveMask + "\"!");
			return tokens;
		}
		
		private GameObject FindGameObject(string[] names, bool rootSearch)
		{
			GameObject start = FindStartNode(names[0], rootSearch);
			
			var transform = start.transform;
			Transform childTransform;
			for (int i = 1; i < names.Length; ++i)
			{
				if (names[i] == AnyActiveMask)
				{
					childTransform = GetFirstActiveChild(transform);
				}
				else
				{
					childTransform = transform.Find(names[i]);
				}
				if (childTransform == null)
					throw new ContainerException("Can't find game object \"" + GetFullPath(transform) + PathSeparator + names[i] + "\"");
				transform = childTransform;
			}
			return transform.gameObject;
		}

		/// <summary>
		/// Найти первый активный дочерний элемент
		/// </summary>
		private Transform GetFirstActiveChild(Transform transform)
		{
			return transform.Cast<Transform>().FirstOrDefault(child => child.gameObject.activeInHierarchy);
		}

		private GameObject FindStartNode(string name, bool rootSearch)
		{
			return rootSearch ? FindRootNode(name) : FindNode(name);
		}

		private GameObject FindNode(string name)
		{
			var node = GameObjects.FirstOrDefault(go => go.name == name);
			if (node == null)
				throw new ContainerException("Can't find game object \"" + name + "\"");
			return node;
		}

		private GameObject FindRootNode(string name)
		{
			var gameObjects = GameObjects.ToArray();
			var node = gameObjects.FirstOrDefault(go => go.transform.parent == null && go.name == name);
			if (node == null)
				throw new ContainerException("Can't find game object \"" + PathSeparator + name + "\"");
			return node;
		}

		private string GetFullPath(Transform transform)
		{
			var builder = new StringBuilder();
			if (transform.parent != null)
				builder.Append(GetFullPath(transform.parent));
			
			builder.Append(PathSeparator).Append(transform.name);
			return builder.ToString();
		}
	}
}
