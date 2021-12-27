using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Projectile : ProjectileBase
{
	[SerializeField] private float speed = 100f;

	private Vector2 Direction { get; set; }
	private bool IsFacingRight { get; set; }
	public float Speed { get; set; }

	private Vector2 movement;

	public override void Initialize()
	{
		if (isInitialized) return;

		base.Initialize();
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

	protected override void Stop()
	{
		this.Direction = Vector3.zero;
	}
}
