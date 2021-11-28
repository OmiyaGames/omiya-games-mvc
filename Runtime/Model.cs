using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmiyaGames.MVC
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="Model.cs" company="Omiya Games">
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
	/// <strong>Date:</strong> 11/28/2021<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial verison.</description>
	/// </item>
	/// </list>
	/// </remarks>
	///-----------------------------------------------------------------------
	/// <summary>
	/// A based implementation of <seealso cref="IModel"/>, which can be created
	/// by <seealso cref="ModelFactory"/>.
	/// </summary>
	public abstract class Model : MonoBehaviour, IModel
	{
		/// <inheritdoc/>
		public string Key
		{
			get;
			private set;
		}

		/// <inheritdoc/>
		public void OnCreate(string key, ModelFactory source)
		{
			if (key == null)
			{
				throw new System.ArgumentNullException("key");
			}
			else if (source == null)
			{
				throw new System.ArgumentNullException("source");
			}

			// Setup member variables
			Key = key;
			OnCreate(source);
		}

		/// <summary>
		/// Called by <seealso cref="ModelFactory.Create{T}(string)"/>.
		/// </summary>
		/// <param name="source">The factory creating this model.</param>
		/// <remarks>
		/// If not overridden, this method does nothing.
		/// Property <see cref="Key"/> is already set by the point this method
		/// is called.
		/// </remarks>
		protected virtual void OnCreate(ModelFactory source) { }
	}
}
