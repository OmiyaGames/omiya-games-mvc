# [Omiya Games](https://omiyagames.com) - MVC

[![Ko-fi Badge](https://img.shields.io/badge/donate-ko--fi-29abe0.svg?logo=ko-fi)](https://ko-fi.com/I3I51KS8F) [![License Badge](https://img.shields.io/github/license/OmiyaGames/omiya-games-mvc)](https://github.com/OmiyaGames/omiya-games-mvc/blob/master/LICENSE.md)

The **Model-View-Controller (MVC)** framework is a common way of organizing code for GUI applications.  This package implements a number of helper scripts to help enforce this framework for a Unity project.  Currently, this package is in development stages, and may change over time.

This MVC implementation runs with the philosophy that [**Models**](https://omiyagames.github.io/omiya-games-mvc/manual/model.html) contains data, delegates, and `[ContextMenu]` methods for implementing quick cheats.  [**Controllers**](https://omiyagames.github.io/omiya-games-mvc/manual/controller.html), meanwhile, creates and sets up models with initial data, and assigning functions to delegates that manipulates the model's data.  Finally, [**Views**](https://omiyagames.github.io/omiya-games-mvc/manual/view.html) grabs instances of models to update visuals (e.g. UI) in-game based off of model's data, call the model's delegate, and listen to them like events.

With this organization, it's becomes possible to display in-game data in realtime through the use of the Model Inspector:

[![Model Inspector Preview](https://omiyagames.github.io/omiya-games-mvc/resources/modelInspectorPreview.png)](https://omiyagames.github.io/omiya-games-mvc/manual/model.html#model-inspector)

## About the Manual

Each part of the MVC framework are described in more thorough details in the links below:

- [Model](https://omiyagames.github.io/omiya-games-mvc/manual/model.html)
- [View](https://omiyagames.github.io/omiya-games-mvc/manual/view.html)
- [Controllers](https://omiyagames.github.io/omiya-games-mvc/manual/controller.html)

## Sample Code

Here's an example of reading a text input entry from a UI:

### [Model](https://omiyagames.github.io/omiya-games-mvc/manual/model.html)

```cs
using OmiyaGames.MVC;
using UnityEngine;

public class CustomModel : Model
{
	// Serialized member variable
	public string text = "Testing!";

	// Delegate for the controller to define
	public Controller.EventBase<string> ChangeText;

	// Context Menu method, usually for implementing cheats
	[ContextMenu("Log Text")]
	public void LogText()
	{
		Debug.Log(text);
	}
}
```

### [View](https://omiyagames.github.io/omiya-games-mvc/manual/view.html)

```cs
using OmiyaGames.MVC;
using UnityEngine;
using UnityEngine.UI;

public class CustomView : MonoBehaviour
{
	CustomModel model;

	[SerializeField]
	TextInput input;

	void Start()
	{
		// Retrieve the CustomModel
		// Note: if ModelFactory.Create<CustomModel>() hasn't been called yet,
		// this line *will* throw an exception!
		model = ModelFactory.Get<CustomModel>();

		// Update text input value
		input.text = model.text;
	}

	// Called by the submit button
	public void OnSubmitClicked()
	{
		// Call ChangeText if it's defined
		model.ChangeText?.Invoke(this, input.text);
	}
}
```

### [Controller](https://omiyagames.github.io/omiya-games-mvc/manual/controller.html)

```cs
using OmiyaGames.MVC;
using UnityEngine;

public class CustomController : MonoBehaviour
{
	CustomModel model;

	[SerializeField]
	string firstText = "First!";

	// Using Awake() so model is created before Start()
	void Awake()
	{
		// Create the CustomModel
		model = ModelFactory.Create<CustomModel>();

		// Setup initial data of the model
		model.text = firstText;
		model.ChangeText = (source, newText) => model.text = newText;
	}

	void OnDestroy()
	{
		// (Optional) Destroy the CustomModel
		ModelFactory.Release<CustomModel>();
		model = null;
	}
}
```

## LICENSE

Overall package is licensed under [MIT](https://github.com/OmiyaGames/omiya-games-mvc/blob/master/LICENSE.md), unless otherwise noted in the [3rd party licenses](https://github.com/OmiyaGames/omiya-games-mvc/blob/master/THIRD%20PARTY%20NOTICES.md) file and/or source code.
