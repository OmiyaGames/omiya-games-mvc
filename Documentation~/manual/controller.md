# Controller

Controllers in the Model-View-Controller code pattern are scripts responsible for defining actions that manipulate the data held in [models](https://omiyagames.github.io/omiya-games-mvc/manual/model.html).  The [view](https://omiyagames.github.io/omiya-games-mvc/manual/view.html) scripts later uses these manipulated data as reference to update its display.  In this MVC package, controllers are "soft" enforced: no interfaces or abstract classes exists to enforce this part of the pattern.  Nonetheless, with the help of [`ModelFactory`](https://omiyagames.github.io/omiya-games-mvc/manual/model.html#model-factory), it should be an easy pattern to enforce!

## Recommended Convention

Controllers are typically `MonoBehaviour` attached to a scene's GameObject with at least `void Awake()` method defined to create model, and setup a model.  Critically, controller's main purpose is to assign methods to the delegates of a model.

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

Controllers may also hold serialized references to both in-scene and in-project assets.  Typically, this is used to assign the initial variables of a model, but there may be other uses as well where this might be important.

```cs
public class CustomController : MonoBehaviour
{
	CustomModel model;

	[SerializeField]
	Sprite deadIcon;
	[SerializeField]
	ParticleSystem explosionPrefab;

	void Awake()
	{
		// Setup initial data of the model
		model = ModelFactory.Create<CustomModel>();
		model.deadIcon = deadIcon;
		model.explosion = explosionPrefab;
	}
}
```

## Helper Delegates

The `Controller` static class contains a few useful delegate templates defined:

```cs
public static class Controller
{
	public delegate void EventBase(object source);
	public delegate void EventBase<T>(object source, T arg);
	public delegate void EventBaseMulti<T>(object source, params T[] args);
}
```

These delegates can be used for listening to events.  The `source` is the object calling the function, and `arg(s)` is the supplied argument (recommended: have `arg's` type be a class that extends `System.EventArgs`.)

## Differences From Views

The fact that both controllers and views can change a model's delegates makes the differences between views and controllers rather blurry.  While it will be up to the developers of the project to establish their own boundaries on what views and controllers can and can't do, this documenter recommends the following rule:

- Controllers should apply behavior to delegates that changes the data in the model.
- Views should *not* change the model's data, unless through a delegate that a controller has already defined.
- Views should, instead, apply behavior to delegates that only affects the visuals of the game.
