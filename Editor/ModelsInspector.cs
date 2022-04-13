using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OmiyaGames.MVC.Editor
{
	///-----------------------------------------------------------------------
	/// <remarks>
	/// <copyright file="ModelsInspector.cs" company="Omiya Games">
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
	/// Creates a window that displays the content of all models generated
	/// by <seealso cref="ModelFactory"/>
	/// </summary>
	public class ModelsInspector : EditorWindow
	{
		const string TabTitle = "Models Inspector";
		const string IconPath = "Packages/com.omiyagames.mvc/Editor/Icons/model.png";
		const string Tooltip = "Displays runtime data";

		static readonly Vector2 MinSize =  new Vector2(300f, 300f);
		UnityEditor.Editor editor = null;
		bool allowLazyModelLoading = true;
		Vector2 scrollPosition;
		readonly Dictionary<Object, UnityEditor.Editor> objectToEditorCache = new Dictionary<Object, UnityEditor.Editor>();
		readonly List<Object> destroyedObjects = new List<Object>();

		[MenuItem("Tools/Omiya Games/Models Inspector")]
		[MenuItem("Window/Omiya Games/Models Inspector")]
		static void Initialize()
		{
			ModelsInspector window = GetWindow<ModelsInspector>(TabTitle, true);
			window.minSize = MinSize;
			window.Show();
		}

		#region Unity Events
		void OnEnable()
		{
			Texture2D tabIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);
			titleContent = new GUIContent(TabTitle, tabIcon, Tooltip);

			editor = null;
			objectToEditorCache.Clear();
		}

		void OnGUI()
		{
			// Only draw the following if app is playing
			if (Application.isPlaying == false)
			{
				// If not, draw placeholder message
				DrawDisabledToolbar();
				EditorGUILayout.HelpBox("Play the game to see runtime data!", MessageType.Info);
				return;
			}

			// Check if the editor needs to be generated for the ModelFactory
			if (editor == null)
			{
				// Make sure a ModelFactory exists
				if (ModelFactory.Instance == null)
				{
					// If not, draw placeholder message
					DrawDisabledToolbar();
					EditorGUILayout.HelpBox("Unable to retrieve an instance of the Model Factory", MessageType.Warning);
					return;
				}

				// Generate an editor to the gameobject
				editor = UnityEditor.Editor.CreateEditor(ModelFactory.Instance.gameObject);
			}

			// Draw the toolbar
			DrawToolbar();
			ModelFactory.Instance.IsGetLazy = allowLazyModelLoading;

			// Update the factory gameobject to its latest settings
			SerializedObject factory = editor.serializedObject;
			factory.Update();

			// Start scrolling area
			using (var scope = new EditorGUILayout.ScrollViewScope(scrollPosition, GUIStyle.none, GUI.skin.verticalScrollbar))
			{
				// Draw the models in the factory
				DrawAllModels(factory.FindProperty("m_Component"));

				// Update scroll position
				scrollPosition = scope.scrollPosition;
			}

			// Record any changes
			factory.ApplyModifiedProperties();
		}
		#endregion

		void DrawDisabledToolbar()
		{
			// Draw the toolbar
			using (var scope = new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
			{
				// Draw a toggle for lazy-loading models
				DrawLazyLoadModelsToggle();

				// Set whether controls in the toolbar should be enabled or not
				bool wasEnabled = GUI.enabled;
				GUI.enabled = false;

				// Draw the reset button
				DrawClearButton();

				// Reset enabled
				GUI.enabled = wasEnabled;

				// Pad the rest of the toolbar with spaces
				GUILayout.FlexibleSpace();
			}
		}

		void DrawToolbar()
		{
			// Draw the toolbar
			using (var scope = new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
			{
				// Draw a toggle for lazy-loading models
				DrawLazyLoadModelsToggle();

				// Draw the reset button
				if (DrawClearButton() == true)
				{
					// Reset everything
					ModelFactory.Reset();
					objectToEditorCache.Clear();

					// Create a fresh new editor from a fresh new ModelFactory
					editor = UnityEditor.Editor.CreateEditor(ModelFactory.Instance.gameObject);
				}

				// Pad the rest of the toolbar with spaces
				GUILayout.FlexibleSpace();
			}
		}

		bool DrawClearButton() => GUILayout.Button("Clear", EditorStyles.toolbarButton, GUILayout.Width(50f));
		void DrawLazyLoadModelsToggle() => allowLazyModelLoading = EditorGUILayout.ToggleLeft("Always Generate Models", allowLazyModelLoading, GUILayout.Width(160f));

		void DrawAllModels(SerializedProperty allComponents)
		{
			// Go through all components in the factory
			for (int i = 0; i < allComponents.arraySize; ++i)
			{
				// Grab the component
				SerializedProperty component = allComponents.GetArrayElementAtIndex(i).FindPropertyRelative("component");

				// Check if this component is a model in the ModelFactory
				Object referencedObject = component?.objectReferenceValue;
				if ((referencedObject is IModel) && ModelFactory.Contains((IModel)referencedObject))
				{
					// If so, check if an editor for this model already exists
					if (objectToEditorCache.TryGetValue(referencedObject, out var modelEditor) == false)
					{
						// If not, construct one, and cache it
						modelEditor = UnityEditor.Editor.CreateEditor(referencedObject);
						objectToEditorCache.Add(referencedObject, modelEditor);
					}

					// Draw the component's title bar
					component.isExpanded = EditorGUILayout.InspectorTitlebar(component.isExpanded, modelEditor);
					if (component.isExpanded)
					{
						// Draw the content of the component
						modelEditor.DrawDefaultInspector();
						EditorGUILayout.Space();
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
