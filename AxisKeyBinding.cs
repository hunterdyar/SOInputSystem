using System;
using System.Collections;
using UnityEngine;

namespace Bloops.SOInputSystem
{
	public class AxisKeyBinding : IAxisBinding
	{
		[Serializable]
		public struct SerializableAxisKeyBinding
		{
			public string AxisName;
			public string PositiveKeyBindingIdentifier;
			public string NegativeKeyBindingIdentifier;
		}
		public string Identifier => identifier;
		[SerializeField] private string identifier;
		public IEnumerator Rebind()
		{
			yield return null;
		}
		
		private KeyBinding _positiveBinding;
		private KeyBinding _negativeBinding;

		private float t;
		
		public AxisKeyBinding(KeyBinding negativeBinding, KeyBinding positiveBinding)
		{
			_negativeBinding = negativeBinding;
			_positiveBinding = positiveBinding;
		}

		public void Tick()
		{
			//pull t towards 0
			
			//push t towards values.
		}

		//Todo Tick/Decay. Store the value as a float and push/pull in directions. Let DefaultInput tick them.
		public float GetAxis()
		{
			if (_negativeBinding.GetKey())
			{
				return _positiveBinding.GetKey() ? 0 : -1;
			}

			if (_positiveBinding.GetKey())
			{
				return _negativeBinding.GetKey() ? 0 : 1;
			}

			return 0;
		}
	}
}