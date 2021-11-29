using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
		const string TabTitle = "Model Inspector";
		const string IconPath = "Packages/com.omiyagames.mvc/Editor/Icons/model.png";
		const string Tooltip = "Displays runtime model data";

		UnityEditor.Editor editor = null;
		bool isFactoryExpanded = false;
		readonly Dictionary<Object, UnityEditor.Editor> objectToEditorCache = new Dictionary<Object, UnityEditor.Editor>();
		readonly List<Object> destroyedObjects = new List<Object>();

		[MenuItem("Tools/Omiya Games/Model Inspector")]
		[MenuItem("Window/Omiya Games/Model Inspector")]
		static void Initialize()
		{
			ModelInspector window = GetWindow<ModelInspector>("Model Inspector", true);
			window.Show();
		}

		void OnEnable()
		{
			Texture2D tabIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);
			titleContent = new GUIContent(TabTitle, tabIcon, Tooltip);
			editor = UnityEditor.Editor.CreateEditor(ModelFactory.Instance.gameObject);
			objectToEditorCache.Clear();
		}

		void OnGUI()
		{
			// Only draw the following if app is playing
			if (Application.isPlaying == false)
			{
				EditorGUILayout.HelpBox("Play the game to see runtime data!", MessageType.Info);
				return;
			}

			// Draw any common actions
			DrawCommonFoldOut();

			// Update the factory gameobject to its latest settings
			SerializedObject factory = editor.serializedObject;
			factory.Update();

			// Draw the models in the factory
			DrawAllModels(factory.FindProperty("m_Component"));

			// Record any changes
			factory.ApplyModifiedProperties();
		}

		void DrawCommonFoldOut()
		{
			// FIXME: change this to a proper toolbar like all the other windows.
			// Start the fold out
			isFactoryExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(isFactoryExpanded, "Model Factory");
			if (isFactoryExpanded)
			{
				// Draw the reset button
				if (GUILayout.Button("Destroy All Models") == true)
				{
					// Call reset on click
					ModelFactory.Reset();
				}
			}
			EditorGUILayout.EndFoldoutHeaderGroup();
		}

		void DrawAllModels(SerializedProperty allComponents)
		{
			// Go through all components in the factory
			for (int i = 0; i < allComponents.arraySize; ++i)
			{
				// Grab the component
				SerializedProperty component = allComponents.GetArrayElementAtIndex(i).FindPropertyRelative("component");

				// Check if this component is a model in the ModelFactory
				if ((component != null) && (component.objectReferenceValue is IModel) && ModelFactory.Contains((IModel)component.objectReferenceValue))
				{
					// If so, check if an editor for this model already exists
					if (objectToEditorCache.TryGetValue(component.objectReferenceValue, out var modelEditor) == false)
					{
						// If not, construct one, and cache it
						modelEditor = UnityEditor.Editor.CreateEditor(component.objectReferenceValue);
						objectToEditorCache.Add(component.objectReferenceValue, modelEditor);
					}

					// Draw the component's title bar
					component.isExpanded = EditorGUILayout.InspectorTitlebar(component.isExpanded, modelEditor);
					if (component.isExpanded)
					{
						// Draw the content of the component
						// FIXME: figure out how to make the serialized fields editable
						modelEditor.DrawDefaultInspector();
					}
				}
			}

			// Check if any objects in the cache has been removed from the factory
			destroyedObjects.Clear();
			foreach (Object key in objectToEditorCache.Keys)
			{
				if (key == null)
				{
					destroyedObjects.Add(key);
				}
			}

			// Clear outdated cache
			foreach (Object key in destroyedObjects)
			{
				objectToEditorCache.Remove(key);
			}
			destroyedObjects.Clear();
		}
	}
}
