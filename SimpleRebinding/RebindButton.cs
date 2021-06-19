using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Bloops.SOInputSystem.SimpleRebinding
{
	public class RebindButton : MonoBehaviour
	{
		[SerializeField] private Text keyCodeText;
		[SerializeField] private Text labelText;
		[SerializeField] private Button button;
		private KeyBinding _binding;
		private InputBindings _bindings;
		private void Start()
		{
			button.onClick.AddListener(OnButtonPress);
			labelText.text = _binding.DisplayName;
			keyCodeText.text = _binding.KeyCode.ToString();
		}

		void OnButtonPress()
		{
			if (_binding != null)
			{
				StartCoroutine(RebindRoutine());
			}
		}

		private IEnumerator RebindRoutine()
		{
			keyCodeText.text = "<Press Any Key>";
			yield return _binding.Rebind();//waits for key press.
			keyCodeText.text = _binding.KeyCode.ToString();
			
			//instantly save.
			_bindings.SaveToPlayerPrefs();
		}
		
		public void SetBinding(InputBindings inputBindings, KeyBinding binding)
		{
			_bindings = inputBindings;
			_binding = binding;
		}
	}
}