using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Bloops.SOInputSystem
{
    [Serializable]
    public class KeyBinding : IKeyBinding
    {
        public string Identifier => _identifier;
        [SerializeField]
        private string _identifier;
        public KeyCode KeyCode => _keyCode;
        [SerializeField]
        private KeyCode _keyCode;//todo: list/array/alternates.

        private KeyCode[] _invalidKeyCodes;//Rebinding system wont use any of these.
        public string DisplayName
        {
            get
            {
                if (displayName.Length == 0)
                {
                    return _identifier;
                }
                else
                {
                    return displayName;
                }
            }
        } //for rebinding,

        [Tooltip("When blank, system will just use Identifier.")]
        [SerializeField] private string displayName;
        public bool isRebinding { get; private set; }
        
        public KeyBinding(string identifier, KeyCode keyCode, KeyCode[] invalidKeyCodes)
        {
            _identifier = identifier.ToLower();
            _keyCode = keyCode;
            _invalidKeyCodes = invalidKeyCodes;
            isRebinding = false;
        }

        public KeyBinding(KeyBinding clone)
        {
            _identifier = clone._identifier;
            _keyCode = clone._keyCode;
            _invalidKeyCodes = clone._invalidKeyCodes;
            displayName = clone.displayName;
            isRebinding = false;
        }

        public bool GetKey()
        {
            return Input.GetKey(_keyCode);
        }

        public bool GetKeyDown()
        {
            return Input.GetKeyDown(_keyCode);
        }

        public bool GetKeyUp()
        {
            return Input.GetKeyUp(_keyCode);
        }

        public void SetKeyCode(KeyCode newKeyCode)
        {
            _keyCode = newKeyCode;
        }

        public IEnumerator Rebind()
        {
            isRebinding = true;
            while (isRebinding)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode) && !_invalidKeyCodes.Contains(keyCode))
                    {
                        _keyCode = keyCode;
                        isRebinding = false;
                        break;
                    }
                }

                if (isRebinding)
                {
                    yield return null;
                }
            }
        }

        public void CancelRebinding()
        {
            isRebinding = false;
        }

        public void SetInvalidKeys(KeyCode[] invalidKeys)
        {
            _invalidKeyCodes = invalidKeys;
        }
    }
}