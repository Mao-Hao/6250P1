using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float speed = 5f;

	private Rigidbody2D body;

	public Transform groundCheckPosition;
	public LayerMask groundLayer;

	private bool isGrounded;
	private bool jumped;

	public float jumpPower = 8f;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void Start()
	{

	}

	void Update()
	{
		CheckIfGrounded();
		PlayerJump();
	}

	void FixedUpdate()
	{
		PlayerWalk();
	}

	void PlayerWalk()
	{

		float h = Input.GetAxisRaw("Horizontal");

		if (h > 0)
		{
			body.velocity = new Vector2(speed, body.velocity.y);

			ChangeDirection(1);

		}
		else if (h < 0)
		{
			body.velocity = new Vector2(-speed, body.velocity.y);

			ChangeDirection(-1);

		}
		else
		{
			body.velocity = new Vector2(0f, body.velocity.y);
		}
	}

	void ChangeDirection(int direction)
	{
		Vector3 tempScale = transform.localScale;
		tempScale.x *= direction;
		transform.localScale = tempScale;
	}

	void CheckIfGrounded()
	{
		isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

		if (isGrounded)
		{
			// and we jumped before
			if (jumped)
			{
				jumped = false;
			}
		}

	}

	void PlayerJump()
	{
		if (isGrounded)
		{
			if (Input.GetKey(KeyCode.Space))
			{
				jumped = true;
				body.velocity = new Vector2(body.velocity.x, jumpPower);
			}
		}
	}

} // class