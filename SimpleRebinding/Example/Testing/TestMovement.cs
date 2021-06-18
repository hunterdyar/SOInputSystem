using Bloops.SOInputSystem;
using UnityEngine;

namespace Testing
{
	public class TestMovement : MonoBehaviour
	{
		private Rigidbody2D rb;
		private InputBindings input;
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
			input = DefaultInput.Bindings;//Shortcut to default input bindings. Not actually needed.
		}

		void Update()
		{
			if (input.GetKey("left"))
			{
				rb.velocity = Vector2.left;
			}else if (input.GetKey("right"))
			{
				rb.velocity = Vector2.right;
			}
			else if (input.GetKey("up"))
			{
				rb.velocity = Vector2.up;
			}
			else if (input.GetKey("down"))
			{
				rb.velocity = Vector2.down;
			}
			else
			{
				rb.velocity = Vector2.zero;
			}
		}
	}
}