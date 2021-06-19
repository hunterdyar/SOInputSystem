using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace Bloops.SOInputSystem.SimpleRebinding
{
	public class RebindingMenuManager : MonoBehaviour
	{
		public InputBindings defaultBindings;
		public InputBindings bindings;
		public GameObject rebindButtonPrefab;
		private List<GameObject> _buttonObjects = new List<GameObject>();
		public void OnEnable()
		{
			//we should load from playerPrefs somewhere when the game starts. This menu probably isn't the right place for that.
			
			//bindings.LoadFromPlayerPrefs();
			DrawChildren();
		}

		private void OnDisable()
		{
			//Instead of passing the reference into the child, we could just save the settings when the menu is closed, probably. 
			//That would be better than having the buttons save every time, but i dont know how you like your menus to be set up. 
			//Maybe theyre not enabled/disabled objects. Or you're doing some fancy animation-out thing. 
			//Make it work for you, this is just example code.
			
			//As this line is not commented out, it does both. That's silly.
			bindings.SaveToPlayerPrefs();
		}

		public void ResetToDefault()
		{
			bindings.Clone(defaultBindings);
			ClearButtons();
			DrawChildren();
		}

		private void DrawChildren()
		{
			foreach (var b in bindings.GetKeyBindings())
			{
				var go = Instantiate(rebindButtonPrefab, transform);
				var button = go.GetComponent<RebindButton>();
				button.SetBinding(bindings, b);
				_buttonObjects.Add(go);
			}
		}

		private void ClearButtons()
		{
			foreach (var go in _buttonObjects)
			{
				Destroy(go);
			}
			_buttonObjects.Clear();
		}
	}
}