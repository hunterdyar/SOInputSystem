using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

namespace Bloops.SOInputSystem
{
	[CreateAssetMenu(fileName = "InputBindings", menuName = "Bloops/Input System/Bindings", order = 0)]
	public class InputBindings : ScriptableObject
	{

		private Dictionary<string, KeyBinding> _keyBindingsMap;
		private Dictionary<string, AxisKeyBinding> _axisBindingsMap;
		[SerializeField] private List<KeyBinding> keyBindings;
		[SerializeField] private List<AxisKeyBinding.SerializableAxisKeyBinding> axisBindings;
		[Tooltip("Rebinding wont allow these keys to be pressed, but one can manually override.")]
		[SerializeField] private KeyCode[] invalidKeys;
		
		/// <summary>
		/// Updates an internal dictionary that keeps binding access snappy. If there is a null reference error, this may need to run. It should be run by the editor OnValidate (needed whenever keyBindings changes).
		/// </summary>
		[ContextMenu("Reinitialize")]
		
		public void Refresh()
		{
			//Update keybindings map
			_keyBindingsMap = new Dictionary<string, KeyBinding>();
			foreach (var b in keyBindings)
			{
				if (!_keyBindingsMap.ContainsKey(b.Identifier))
				{
					_keyBindingsMap.Add(b.Identifier.ToLower(), b);
				}
				else
				{
					//TOdo: create multi-key binding.
				}
				b.SetInvalidKeys(invalidKeys);
			}
			//update axis bindings map.
			_axisBindingsMap = new Dictionary<string, AxisKeyBinding>();
			foreach (var a in axisBindings)
			{
				if (!_axisBindingsMap.ContainsKey(a.AxisName))
				{
					AxisKeyBinding axis = new AxisKeyBinding(GetKeyBinding(a.NegativeKeyBindingIdentifier), GetKeyBinding(a.PositiveKeyBindingIdentifier));
					_axisBindingsMap.Add(a.AxisName.ToLower(), axis);
				}
			}
		}

		public bool GetKey(string input)
		{
			if(_keyBindingsMap.TryGetValue(input.ToLower(),out var binding))
			{
				return binding.GetKey();
			}
			else
			{
				Debug.LogError($"Input binding \"{input}\" was not found.");
				return false;
			}
		}

		public bool GetKeyDown(string input)
		{
			if (_keyBindingsMap.TryGetValue(input.ToLower(), out var binding))
			{
				return binding.GetKeyDown();
			}
			else
			{
				Debug.LogError($"Input binding \"{input}\" was not found.");
				return false;
			}
		}

		public bool GetKeyUp(string input)
		{
			if (_keyBindingsMap.TryGetValue(input.ToLower(), out var binding))
			{
				return binding.GetKeyUp();
			}
			else
			{
				Debug.LogError($"Input binding \"{input}\" was not found.");
				return false;
			}
		}

		public float GetAxis(string input)
		{
			if (_axisBindingsMap.TryGetValue(input.ToLower(), out var binding))
			{
				return binding.GetAxis();
			}
			else
			{
				Debug.LogError($"Axis \"{input}\" was not found.");
				return 0;
			}
		}

		[ContextMenu("Add Default Bindings")]
		public void AddDefaultBindings()
		{
			var up = new KeyBinding("up", KeyCode.W, invalidKeys);
			var down = new KeyBinding("down", KeyCode.S, invalidKeys);
			var left = new KeyBinding("left", KeyCode.A, invalidKeys);
			var right = new KeyBinding("right", KeyCode.D, invalidKeys);

			keyBindings.Add(up);
			keyBindings.Add(down);
			keyBindings.Add(left);
			keyBindings.Add(right);
			
			Refresh();
		}

		public void SaveToPlayerPrefs()
		{
			foreach (KeyBinding kb in keyBindings)
			{
				PlayerPrefs.SetInt(name + "_kb_" +kb.Identifier, (int) kb.KeyCode);
			}

			foreach (AxisKeyBinding.SerializableAxisKeyBinding ab in axisBindings)
			{
				PlayerPrefs.SetString(name + "_ab_" + ab.AxisName + "p", ab.PositiveKeyBindingIdentifier);
				PlayerPrefs.SetString(name + "_ab_" + ab.AxisName + "n", ab.NegativeKeyBindingIdentifier);
			}
		}

		public void LoadFromPlayerPrefs()
		{
			foreach (KeyBinding kb in keyBindings)
			{
				KeyCode bind = (KeyCode)PlayerPrefs.GetInt(name+"_kb_" + kb.Identifier, (int) kb.KeyCode);
				kb.SetKeyCode(bind);
			}

			foreach (AxisKeyBinding.SerializableAxisKeyBinding ab in axisBindings)
			{
				var pos = PlayerPrefs.GetString(name+"_ab_" + ab.AxisName + "p", ab.PositiveKeyBindingIdentifier);
				var neg = PlayerPrefs.GetString(name+"_ab_" + ab.AxisName + "n", ab.NegativeKeyBindingIdentifier);
				ab.NegativeKeyBindingIdentifier = neg;
				ab.PositiveKeyBindingIdentifier = pos;
			}
			
			Refresh();
		}

		public IEnumerator GetRebindRoutine(string input)
		{
			if (_keyBindingsMap.TryGetValue(input, out var binding))
			{
				return binding.Rebind();
			}

			return null;
		}

		public KeyBinding GetKeyBinding(string input)
		{
			if (_keyBindingsMap.TryGetValue(input, out var binding))
			{
				return binding;
			}

			return null;
		}
		
		private void AddBinding(IKeyBinding binding)
		{
			if (binding is KeyBinding kb)
			{
				keyBindings.Add(kb);
				Refresh();
			}
		}
		public List<KeyBinding> GetKeyBindings()
		{
			return keyBindings;
		}

		private List<AxisKeyBinding.SerializableAxisKeyBinding> GetSerializedAxisBindings()
		{
			return axisBindings;
		}

		public List<AxisKeyBinding> GetAxisKeyBindings()
		{
			return _axisBindingsMap.Values.ToList();
		}

		private void OnValidate()
		{
			Refresh();
		}

		public void Clone(InputBindings other)
		{
			keyBindings = other.GetKeyBindings().Select(a => new KeyBinding(a)).ToList();
			axisBindings = other.GetSerializedAxisBindings();
			Refresh();
		}
	}
}