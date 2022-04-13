# Change Log:

## 0.2.0-exp

- **New Feature**: Adding [`Views`](/Runtime/View.cs), a static class with helper functions to bind a label to a `ITrackable<string>` in a model.  Said label will auto-update if the variable changes.
- **Enhancments**: Adding a ton of new helper delegates in [`Controller`](/Runtime/Controller.cs).
- **Enhancments**: Updating [`ModelFactory`](/Runtime/ModelFactory.cs) to key off of `objects` instead of `strings`.
- **Enhancments**: Renaming `ModelInspector` into [`ModelsInspector`](/Editor/ModelsInspector.cs).  Adding a lazy load checkbox which always creates a requested model.

## 0.1.0-exp

- Initial release:
    - Adding `IModel`, `Model`, `ModelFactory`, and `Controller`.
- Adding initial documentation on all.
