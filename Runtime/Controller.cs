using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
		#region Actions
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		public delegate void Action(object source);
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// </typeparam>
		public delegate void Action<in T>(object source, T arg);
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Arguments' type.
		/// </typeparam>
		public delegate void ActionMulti<in T>(object source, params T[] args);
		#endregion


		#region Funcs
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		public delegate T Func<out T>(object source);
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// Return type.
		/// </typeparam>
		public delegate TResult Func<in TArg, out TResult>(object source, TArg arg);
		/// <summary>
		/// A definition for delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Arguments' type.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// Return type.
		/// </typeparam>
		public delegate TResult FuncMulti<in TArg, out TResult>(object source, params TArg[] args);
		#endregion

		#region Events
		/// <summary>
		/// A definition for event delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		public delegate void Event(object source);
		/// <summary>
		/// A definition for event delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// </typeparam>
		public delegate void Event<in T>(object source, T arg) where T : System.EventArgs;
		#endregion

		#region Coroutines
		/// <summary>
		/// A definition for coroutine delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <returns>A coroutine.</returns>
		public delegate IEnumerator Coroutine(object source);
		/// <summary>
		/// A definition for coroutine delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// </typeparam>
		/// <returns>A coroutine.</returns>
		public delegate IEnumerator Coroutine<T>(object source, T arg);
		/// <summary>
		/// A definition for coroutine delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Arguments' type.
		/// </typeparam>
		/// <returns>A coroutine.</returns>
		public delegate IEnumerator CoroutineMulti<T>(object source, params T[] args);
		#endregion

		#region Async
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <returns>A <seealso cref="Task">.</returns>
		public delegate Task Async(object source);
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Return type.
		/// </typeparam>
		/// <returns>A <seealso cref="Task{T}">.</returns>
		public delegate Task<T> Async<T>(object source);
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Argument's type.
		/// </typeparam>
		/// <returns>A <seealso cref="Task">.</returns>
		public delegate Task AsyncArgs<in T>(object source, T arg);
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="arg">Supplied event arguments.</param>
		/// <typeparam name="TArgs">
		/// Argument's type.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// Return type.
		/// </typeparam>
		/// <returns>A <seealso cref="Task{TResult}">.</returns>
		public delegate Task<TResult> AsyncArgs<in TArgs, TResult>(object source, TArgs arg);
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="T">
		/// Arguments' type.
		/// </typeparam>
		/// <returns>A <seealso cref="Task">.</returns>
		public delegate Task AsyncArgsMulti<in T>(object source, params T[] args);
		/// <summary>
		/// A definition for async delegates in a <see cref="Model"/>.
		/// </summary>
		/// <param name="source">The caller of this event.</param>
		/// <param name="args">Supplied event arguments.</param>
		/// <typeparam name="TArgs">
		/// Arguments' type.
		/// </typeparam>
		/// <typeparam name="TResult">
		/// Return type.
		/// </typeparam>
		/// <returns>A <seealso cref="Task{TResult}">.</returns>
		public delegate Task<TResult> AsyncArgsMulti<in TArgs, TResult>(object source, params TArgs[] args);
		#endregion
	}
}
