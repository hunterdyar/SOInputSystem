using System;
using Bloops.SOInputSystem;
using UnityEngine;

namespace Testing
{
	public class TestMovement : MonoBehaviour
	{
		private Rigidbody2D rb;
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (DefaultInput.Bindings.GetKey("left"))
			{
				rb.velocity = Vector2.left;
			}else if (DefaultInput.Bindings.GetKey("right"))
			{
				rb.velocity = Vector2.right;
			}
			else if (DefaultInput.Bindings.GetKey("up"))
			{
				rb.velocity = Vector2.up;
			}
			else if (DefaultInput.Bindings.GetKey("down"))
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