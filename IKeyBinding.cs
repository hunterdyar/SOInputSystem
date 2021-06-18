using System.Collections;

namespace Bloops.SOInputSystem
{
	public interface IKeyBinding
	{
		public string Identifier { get; }
		IEnumerator Rebind();

		bool GetKey();

		bool GetKeyUp();

		bool GetKeyDown(); 
	}
}