using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OmiyaGames.MVC
{
	public static class View
	{
		struct BindElements<T>
		{
			public ITrackable<T> tracking;
			public ITrackable<T>.ChangeEvent action;

		}

		static readonly Dictionary<ILayoutElement, BindElements<string>> bindedActions = new Dictionary<ILayoutElement, BindElements<string>>();

		/// <summary>
		/// Binds a label so that when changes are made to an
		/// <seealso cref="ITrackable{T}"/>, the label's text
		/// will be updated as well.
		/// </summary>
		/// <param name="labelToUpdate"></param>
		/// <param name="listenTo"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static void Bind(TextMeshProUGUI labelToUpdate, ITrackable<string> listenTo)
		{
			BindArgCheck(labelToUpdate, listenTo);

			ITrackable<string>.ChangeEvent action = (_, newValue) =>
			{
				labelToUpdate.text = newValue;
			};
			bindedActions.Add(labelToUpdate, new BindElements<string>()
			{
				tracking = listenTo,
				action = action
			});
			listenTo.OnAfterValueChanged += action;
		}

		/// <summary>
		/// Removed any <seealso cref="ITrackable{T}"/> bindings to a label.
		/// </summary>
		/// <param name="labelToUpdate"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static bool Unbind(TextMeshProUGUI labelToUpdate)
		{
			if (labelToUpdate == null)
			{
				throw new ArgumentNullException(nameof(labelToUpdate));
			}

			bool returnFlag = bindedActions.Remove(labelToUpdate, out BindElements<string> elements);
			if(returnFlag == true)
			{
				elements.tracking.OnAfterValueChanged -= elements.action;
			}
			return returnFlag;
		}

		/// <summary>
		/// Binds a label so that when changes are made to an
		/// <seealso cref="ITrackable{T}"/>, the label's text
		/// will be updated as well.
		/// </summary>
		/// <param name="labelToUpdate"></param>
		/// <param name="listenTo"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static void Bind(Text labelToUpdate, ITrackable<string> listenTo)
		{
			BindArgCheck(labelToUpdate, listenTo);

			ITrackable<string>.ChangeEvent action = (_, newValue) =>
			{
				labelToUpdate.text = newValue;
			};
			bindedActions.Add(labelToUpdate, new BindElements<string>()
			{
				tracking = listenTo,
				action = action
			});
			listenTo.OnAfterValueChanged += action;
		}

		/// <summary>
		/// Removed any <seealso cref="ITrackable{T}"/> bindings to a label.
		/// </summary>
		/// <param name="labelToUpdate"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static bool Unbind(Text labelToUpdate)
		{
			if (labelToUpdate == null)
			{
				throw new ArgumentNullException(nameof(labelToUpdate));
			}

			bool returnFlag = bindedActions.Remove(labelToUpdate, out BindElements<string> elements);
			if (returnFlag == true)
			{
				elements.tracking.OnAfterValueChanged -= elements.action;
			}
			return returnFlag;
		}

		static void BindArgCheck(ILayoutElement labelToUpdate, ITrackable<string> listenTo)
		{
			if (listenTo == null)
			{
				throw new ArgumentNullException(nameof(listenTo));
			}
			else if (labelToUpdate == null)
			{
				throw new ArgumentNullException(nameof(labelToUpdate));
			}
			else if (bindedActions.ContainsKey(labelToUpdate))
			{
				throw new ArgumentException("Already binded to this label.", nameof(labelToUpdate));
			}
		}

	}
}
