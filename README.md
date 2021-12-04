# [Omiya Games](https://omiyagames.com) - MVC

[![MVC Package documentation](https://github.com/OmiyaGames/omiya-games-mvc/workflows/Host%20DocFX%20Documentation/badge.svg)](https://omiyagames.github.io/omiya-games-mvc/) [![Ko-fi Badge](https://img.shields.io/badge/donate-ko--fi-29abe0.svg?logo=ko-fi)](https://ko-fi.com/I3I51KS8F) [![License Badge](https://img.shields.io/github/license/OmiyaGames/omiya-games-mvc)](/LICENSE.md)

The **Model-View-Controller (MVC)** framework is a common way of organizing code for GUI applications.  This package implements a number of helper scripts to help enforce this framework for a Unity project.  Currently, this package is in development stages, and may change over time.

This MVC implementation runs with the philosophy that [**Models**](https://omiyagames.github.io/omiya-games-mvc/manual/model.html) contains data, delegates, and `[ContextMenu]` methods for implementing quick cheats.  [**Controllers**](https://omiyagames.github.io/omiya-games-mvc/manual/controller.html), meanwhile, creates and sets up models with initial data, and assigning functions to delegates that manipulates the model's data.  Finally, [**Views**](https://omiyagames.github.io/omiya-games-mvc/manual/view.html) grabs instances of models to update visuals (e.g. UI) in-game based off of model's data, call the model's delegate, and listen to them like events.

With this organization, it's becomes possible to display in-game data in realtime through the use of the Model Inspector:

[![Model Inspector Preview](https://omiyagames.github.io/omiya-games-mvc/resources/modelInspectorPreview.png)](https://omiyagames.github.io/omiya-games-mvc/manual/model.html#model-inspector)

## Install

There are two common methods for installing this package.

### Through [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

Unity's own Package Manager supports [importing packages through a URL to a Git repo](https://docs.unity3d.com/Manual/upm-ui-giturl.html):

1. First, on this repository page, click the "Clone or download" button, and copy over this repository's HTTPS URL.  
2. Then click on the + button on the upper-left-hand corner of the Package Manager, select "Add package from git URL..." on the context menu, then paste this repo's URL!

While easy and straightforward, this method has a few major downside: it does not support dependency resolution and package upgrading when a new version is released.  To add support for that, the following method is recommended:

### Through [OpenUPM](https://openupm.com/)

*Note: this package hasn't been uploaded to OpenUPM yet.  Instructions below are for future references.*

Installing via [OpenUPM's command line tool](https://openupm.com/) is recommended because it supports dependency resolution, upgrading, and downgrading this package.  Given this package is just an example, thought, it hadn't been added into OpenUPM yet.  So the rest of these instructions are hypothetical...for now...

If you haven't already [installed OpenUPM](https://openupm.com/docs/getting-started.html#installing-openupm-cli), you can do so through Node.js's `npm` (obviously have Node.js installed in your system first):
```
npm install -g openupm-cli
```
Then, to install this package, just run the following command at the root of your Unity project:
```
openupm add com.omiyagames.mvc
```

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

## Resources

- [Documentation](https://omiyagames.github.io/omiya-games-mvc/)
- [Change Log](/CHANGELOG.md)

## LICENSE

Overall package is licensed under [MIT](/LICENSE.md), unless otherwise noted in the [3rd party licenses](/THIRD%20PARTY%20NOTICES.md) file and/or source code.

Copyright (c) 2021 Omiya Games
