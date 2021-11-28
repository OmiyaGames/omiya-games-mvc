using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace OmiyaGames.MVC.Editor
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="ModelInspector.cs" company="Omiya Games">
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
	/// <strong>Version:</strong> 0.1.0-preview.1<br/>
	/// <strong>Date:</strong> 11/28/2021<br/>
	/// <strong>Author:</strong> Taro Omiya
	/// </term>
	/// <description>Initial version.</description>
	/// </item>
	/// </list>
	/// </remarks>
	///-----------------------------------------------------------------------
	/// <summary>
	/// Creates a window that always displays <seealso cref="ModelInspector"/>.
	/// </summary>
	public class ModelInspector : EditorWindow
	{
		[MenuItem("Tools/Omiya Games/Model Inspector")]
		[MenuItem("Window/Omiya Games/Model Inspector")]
		static void Initialize()
		{
			ModelInspector window = GetWindow<ModelInspector>(title: "Model Inspector");
			window.Show();
		}

		void OnGUI()
		{
			if (GUILayout.Button("Reset") == true)
			{
				ModelFactory.Reset();
			}

			// TODO: consider using editor visual tree?
			foreach (var model in ModelFactory.AllModels)
			{
				// FIXME: better performance, better visuals, please.
				var test = UnityEditor.Editor.CreateEditor((Component)model);
				test.DrawDefaultInspector();
			}
		}
	}
}
