using System;
using System.Collections.Generic;
using UnityEngine;
using OmiyaGames.Global;

namespace OmiyaGames.MVC
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="ModelFactory.cs" company="Omiya Games">
	/// The MIT License (MIT)
	/// 
	/// Copyright (c) 2021 Omiya Games
	/// 
	/// Permission is hereby granted, free of charge, to any person obtaining a copy
	/// of this software and associated documentation files (the "Software"), to deal
	/// in the Software without restriction, including without limitation the rights
	/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	/// copies of the Software, and to permit persons to whom the Software is
	/// furnished to do so, subject to the following conditions:
	/// 
	/// The above copyright notice and this permission notice shall be included in
	/// all copies or substantial portions of the Software.
	/// 
	/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
	/// THE SOFTWARE.
	/// </copyright>
	/// <list type="table">
	/// <listheader>
	/// <term>Revision</term>
	/// <description>Description</description>
	/// </listheader>
	/// <item>
	/// <term>
	/// <strong>Version:</strong> 1.1.0<br/>
	/// <strong>Date:</strong> 11/27/2021<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial verison.</description>
	/// </item>
	/// </list>
	/// </remarks>
	///-----------------------------------------------------------------------
	/// <summary>
	/// Use this class to get a static instance of a component.
	/// Mainly used to have a default instance.
	/// </summary>
	/// <remarks>
	/// Code from Unity's Core RenderPipeline package (<c>ComponentSingleton<TType></c>.)
	/// </remarks>
	/// <typeparam name="T">Component type.</typeparam>
	public class ModelFactory : MonoBehaviour
	{
		struct KeyPair
		{
			internal KeyPair(Type type, string key)
			{
				// Prevent null keys.
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}

				// Setup member properties
				Type = type;
				Key = key;
			}

			public Type Type
			{
				get;
			}
			public string Key
			{
				get;
			}
		}

		static ModelFactory Instance => ComponentSingleton<ModelFactory>.Instance;
		static Dictionary<KeyPair, IModel> KeyToModelMap => Instance.keyToModelMap;

		readonly Dictionary<KeyPair, IModel> keyToModelMap = new Dictionary<KeyPair, IModel>();

		/// <summary>
		/// Gets all the created models, and their associated keys.
		/// </summary>
		public static IEnumerable<IModel> AllModels => KeyToModelMap.Values;
		/// <summary>
		/// Number of models created so far.
		/// </summary>
		public static int NumberOfModels => KeyToModelMap.Count;

		/// <summary>
		/// Creates a unique <see cref="IModel"/>,
		/// optionally assoiciated with a <paramref name="key"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of <see cref="IModel"/> created.
		/// </typeparam>
		/// <param name="key">
		/// To create multiple instances of the same <see cref="IModel"/>,
		/// supply a unique key to be associated with each of them.
		/// </param>
		/// <returns>
		/// The newly constructed <see cref="IModel"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If <paramref name="key"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// If a <typeparamref name="T"/> model with the same <see cref="IModel.Key"/>
		/// as <paramref name="key"/> has already been created.
		/// </exception>
		/// <seealso cref="IModel"/>
		/// <seealso cref="Get{T}(string)"/>
		public static T Create<T>(string key = "") where T : Component, IModel
		{
			// Construct a key
			var pair = new KeyPair(typeof(T), key);

			// Check if the key already exists in the dictionary
			if (KeyToModelMap.ContainsKey(pair) == true)
			{
				// Don't let the code proceed
				throw new ArgumentException($"Model \"{typeof(T).Name}\" with key \"{key}\" has already been created.", "key");
			}

			// Create the component
			T returnComponent = Instance.gameObject.AddComponent<T>();

			// Add the component to the map before initializing
			KeyToModelMap.Add(pair, returnComponent);
			returnComponent.OnCreate(key, Instance);

			// Return the component
			return returnComponent;
		}

		/// <summary>
		/// Gets an existing <see cref="IModel"/>,
		/// created by <see cref="Create{T}(string)"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of <see cref="IModel"/> to get.
		/// </typeparam>
		/// <param name="key">
		/// Optional key associated with an <see cref="IModel"/>.
		/// </param>
		/// <returns>
		/// Associated <see cref="IModel"/> with <paramref name="key"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If <paramref name="key"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// If a <typeparamref name="T"/> model with the same <see cref="IModel.Key"/>
		/// as <paramref name="key"/> has not been created yet.
		/// </exception>
		/// <seealso cref="IModel"/>
		/// <seealso cref="Create{T}(string)"/>
		public static T Get<T>(string key = "") where T : Component, IModel
		{
			// Construct a key
			var pair = new KeyPair(typeof(T), key);

			// Check if the key exists in the dictionary
			if (KeyToModelMap.TryGetValue(pair, out IModel model) == false)
			{
				// If not, don't let the code proceed
				throw new ArgumentException($"Model \"{typeof(T).Name}\" with key \"{key}\" has not been created yet.", "key");
			}

			// Cast the model
			return (T)model;
		}

		/// <summary>
		/// Attempts to get an existing <see cref="IModel"/>,
		/// created by <see cref="Create{T}(string)"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of <see cref="IModel"/> to get.
		/// </typeparam>
		/// <param name="key">
		/// Key associated with an <see cref="IModel"/>.
		/// </param>
		/// <param name="model">
		/// Associated <see cref="IModel"/> with <paramref name="key"/>,
		/// or <c>null</c> if one isn't found.
		/// </param>
		/// <returns>
		/// <c>true</c> if a created <see cref="IModel"/> was found.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If <paramref name="key"/> is <c>null</c>.
		/// </exception>
		/// <seealso cref="IModel"/>
		/// <seealso cref="Create{T}(string)"/>
		public static bool TryGet<T>(string key, out T model) where T : Component, IModel
		{
			// Construct a key
			var pair = new KeyPair(typeof(T), key);

			// Default return model
			model = null;

			// Check if the key exists in the dictionary
			bool returnFlag = KeyToModelMap.TryGetValue(pair, out IModel returnModel);
			if(returnFlag)
			{
				// Cast the model
				model = (T)returnModel;
			}

			// Return flag
			return returnFlag;
		}

		/// <summary>
		/// Attempts to get an existing <see cref="IModel"/>,
		/// created by <see cref="Create{T}()"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of <see cref="IModel"/> to get.
		/// </typeparam>
		/// <param name="model">
		/// An <see cref="IModel"/> created prior,
		/// or <c>null</c> if one isn't found.
		/// </param>
		/// <returns>
		/// <c>true</c> if a created <see cref="IModel"/> was found.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If <paramref name="key"/> is <c>null</c>.
		/// </exception>
		/// <seealso cref="IModel"/>
		/// <seealso cref="Create{T}(string)"/>
		public static bool TryGet<T>(out T model) where T : Component, IModel => TryGet("", out model);

		/// <summary>
		/// Attempts to destroy an <see cref="IModel"/>,
		/// created by <see cref="Create{T}(string)"/>.
		/// </summary>
		/// <typeparam name="T">
		/// The type of <see cref="IModel"/> to destroy.
		/// </typeparam>
		/// <param name="key">
		/// Key associated with an <see cref="IModel"/>.
		/// </param>
		/// <returns>
		/// <c>true</c> if a created <see cref="IModel"/> was destroyed.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// If <paramref name="key"/> is <c>null</c>.
		/// </exception>
		/// <seealso cref="Create{T}(string)"/>
		public static bool Release<T>(string key = "") where T : Component, IModel
		{
			// Construct a key
			var pair = new KeyPair(typeof(T), key);

			// Check if the key exists in the dictionary
			bool returnFlag = KeyToModelMap.TryGetValue(pair, out IModel model);
			if (returnFlag)
			{
				// Destroy the model
				Helpers.Destroy((T)model);
			}

			// Return flag
			return returnFlag;
		}

		/// <summary>
		/// Destroys all the created <see cref="IModel"/>.
		/// </summary>
		public static void Reset()
		{
			ComponentSingleton<ModelFactory>.Release();
		}
	}
}
