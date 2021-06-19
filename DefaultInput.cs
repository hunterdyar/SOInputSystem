using UnityEngine;

namespace Bloops.SOInputSystem
{
	public class DefaultInput : MonoBehaviour
	{
		public static InputBindings Bindings
		{
			get
			{
				//allows the bindings to be accessed during Awake.
				if (_defaultBindings == null)
				{
					Debug.LogWarning("Default Input accessed without bindings set. Either add a DefaultInput component to the scene or avoid polling DefaultInput.Bindings in Awake.");
					var defaultInput = GameObject.FindObjectOfType<DefaultInput>();
					if (defaultInput != null)
					{
						_defaultBindings = defaultInput.defaultBindings;
					}
				}
				return _defaultBindings;
			}
		}
		
		private static InputBindings _defaultBindings; 

		[SerializeField] private InputBindings defaultBindings;
		[SerializeField] private bool loadFromPlayerPrefsOnAwake = true;
		void Awake()
		{
			_defaultBindings = defaultBindings;
			if (loadFromPlayerPrefsOnAwake)
			{
				_defaultBindings.LoadFromPlayerPrefs();
			}
		}
	}
}