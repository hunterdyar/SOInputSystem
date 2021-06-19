using Bloops.SOInputSystem;
using UnityEngine;

namespace Testing
{
	public class TestJumpMovement : MonoBehaviour
	{
		private Rigidbody2D rb;
	
		//The second way to setup input is to not use DefaultInput.Bindings, but instead just directly reference the scriptable object. 
		//This way allows for easy setup for local multiplayer, for example.
		
		[SerializeField] private InputBindings input;
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (input.GetKey("jump"))
			{
				rb.velocity = new Vector2(rb.velocity.x, 5);
			}
		}
	}
}