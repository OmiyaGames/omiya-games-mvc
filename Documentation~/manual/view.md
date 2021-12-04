# Views

Views in the Model-View-Controller code pattern are scripts responsible for rendering the data in [models](https://omiyagames.github.io/omiya-games-mvc/manual/model.html) in a visually-pleasing manner.  Furthermore, views also calls functions defined in [controller](https://omiyagames.github.io/omiya-games-mvc/manual/controller.html) to whenever a user interacts with the visuals views render.  In this MVC package, views are "soft" enforced: no interfaces or abstract classes exists to enforce this part of the pattern.  Nonetheless, with the help of [`ModelFactory`](https://omiyagames.github.io/omiya-games-mvc/manual/model.html#model-factory), it should be an easy pattern to enforce!

## Recommended Convention

Views are typically `MonoBehaviour` attached to a scene's GameObject with at least `void Start()` method to grab an existing model.  Naturally, the data of this model is read to setup the visuals view scripts are responsible for.  Whenever an event, such as a button click or collision, occurs, views are responsible for calling delegate methods stored in the model.  Obviously, since views needs to manipulate Unity components, they almost always have at least one serialized reference to a scene object:

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
		// Let the controller define the behavior of this submit button
		if(model != null)
		{
			model.ChangeText?.Invoke(this, input.text);
		}
	}
}
```

Views may also listen to a model's delegate (like events,) and update visual changes when said delegate gets called:

```cs
public class CustomView : MonoBehaviour
{
	CustomModel model;

	[SerializeField]
	Text loadingPercent;

	void Start()
	{
		// Retrieve the CustomModel
		model = ModelFactory.Get<CustomModel>();
		model.OnLoading += UpdateLabel;

		// Update label
		loadingPercent.text = "0%";
	}

	void OnDestroy()
	{
		// IMPORTANT: stop listening to the OnLoading event
		if(model != null)
		{
			model.OnLoading -= UpdateLabel;
		}
	}

	void UpdateLabel()
	{
		loadingPercent.text = model.loaded.ToString("0%");
	}
}
```

## Differences From Controllers

The fact that both views and controllers can change a model's delegates makes the differences between views and controllers rather blurry.  While it will be up to the developers of the project to establish their own boundaries on what views and controllers can and can't do, this documenter recommends the following rule:

- Controllers should apply behavior to delegates that changes the data in the model.
- Views should *not* change the model's data, unless through a delegate that a controller has already defined.
- Views should, instead, apply behavior to delegates that only affects the visuals of the game.
