# SOInputSystem
Simple Rebindable Input System for Unity using ScriptableObjects. This wraps around the normal UnityEngine.Input ("old") input system.

## Setup
Clone the repository to an Assets/Bloops/SOInputSystem/ folder.

## Use.
- Create an InputBindings scriptable object (right click in your project window and choose Bloops/Input System/Input Bindings).
- Setup your inputs in this component. You can use "Add Default Bindings" context menu (the three vertical dots menu) to get started. This will add WASD and Jump, and vertical/horizontal axis.
- Optionally add a "DefaultInput" monobehavior to the scene, if you want. This will give you a static reference to DefaultInput.Bindings that you can use like you would use Input. 

To use the input system in a script, just make a reference to an InputBindings object (ie: '[SerializeField] private InputBindings input' or just use DefaultInput.Bindings). 
On this, you can call GetKey, GetKeyUp, GetKeyDown, and GetAxis, passing in the appropriate string name.

## Rebinding use.
The short version is to start the Rebind coroutine, which will listen for the next key press. See the example script.

## Issues
- Player prefs saving not completed yet.
- Multiple Keys for a single binding is not done yet (ie: arrows and WASD).
