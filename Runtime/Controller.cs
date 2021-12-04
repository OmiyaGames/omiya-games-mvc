using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmiyaGames.MVC
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="Controller.cs" company="Omiya Games">
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
	/// Helper methods for controllers of Model-View-Controller.
	/// </summary>
	public static class Controller
	{
		/// <summary>
		/// A definition for event delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		public delegate void EventBase(object source);
		/// <summary>
		/// A definition for event delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// Classes extending <seealso cref="System.EventArgs"/> recommended.
		/// </typeparam>
		public delegate void EventBase<T>(object source, T arg);
		/// <summary>
		/// A definition for event delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Arguments' type.
		/// Classes extending <seealso cref="System.EventArgs"/> recommended.
		/// </typeparam>
		public delegate void EventBaseMulti<T>(object source, params T[] args);
	}
}
