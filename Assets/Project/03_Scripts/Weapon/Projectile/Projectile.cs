using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 100f;

	private Vector2 Direction { get; set; }
	private bool IsFacingRight { get; set; }
	public float Speed { get; set; }

	private Rigidbody2D rigidbody2d;
	private SpriteRenderer spriteRenderer;

	private Vector2 movement;

	private bool isInitialized = false;

	public void Initialize()
	{
		if (isInitialized) return;

		this.rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
		this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		IsFacingRight = true;
		Speed = speed;

		isInitialized = true;
	}

	private void FixedUpdate()
	{
		MoveProjectile();
	}

	private void MoveProjectile()
	{
		movement = Direction * speed * Time.fixedDeltaTime;
		rigidbody2d.MovePosition(rigidbody2d.position + movement);
	}

	private void FlipProjectile()
	{
		spriteRenderer.flipX = !spriteRenderer.flipX;
	}

	public void SetDirection(Vector2 direction, Quaternion rotation, bool isFacingRight)
	{
		if (IsFacingRight != isFacingRight)
		{
			FlipProjectile();
		}
		this.Direction = direction;
		this.transform.rotation = rotation;
	}
}
