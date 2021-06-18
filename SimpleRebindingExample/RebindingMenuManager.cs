using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace Bloops.SOInputSystem.SimpleRebinding
{
	public class RebindingMenuManager : MonoBehaviour
	{
		public InputBindings bindings;
		public GameObject rebindButtonPrefab;
		private List<GameObject> _buttonObjects = new List<GameObject>();
		public void OnEnable()
		{
			DrawChildren();
		}

		private void OnDisable()
		{
			ClearButtons();
		}

		private void DrawChildren()
		{
			foreach (var b in bindings.GetKeyBindings())
			{
				var go = Instantiate(rebindButtonPrefab, transform);
				var button = go.GetComponent<RebindButton>();
				button.SetBinding(b);
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