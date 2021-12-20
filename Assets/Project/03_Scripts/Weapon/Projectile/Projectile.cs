using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float speed = 100f;
	[SerializeField] private LayerMask objectMask;

	[Header("Effect")]
	[SerializeField] private ParticleSystem inpulseEffect = null;

	private Vector2 Direction { get; set; }
	private bool IsFacingRight { get; set; }
	public float Speed { get; set; }
	public Character WeaponOwner { get; set; }

	private Rigidbody2D rigidbody2d;
	private SpriteRenderer spriteRenderer;
	private BoxCollider2D collider;

	private Vector2 movement;

	private bool isInitialized = false;

	public void Initialize()
	{
		if (isInitialized) return;

		this.rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
		this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		this.collider = this.gameObject.GetComponent<BoxCollider2D>();
		IsFacingRight = true;
		Speed = speed;

		isInitialized = true;
	}

	/// <summary>
	/// call everytime when reused
	/// </summary>
	public void ReuseInit()
	{
		spriteRenderer.enabled = true;
		rigidbody2d.isKinematic = false;
		collider.enabled = true;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (CheckLayer(collision.gameObject.layer, objectMask))
		{
			spriteRenderer.enabled = false;
			rigidbody2d.isKinematic = true;
			collider.enabled = false;
			this.inpulseEffect.Play();
			this.inpulseEffect.GetComponent<ParticleSystemCallback>().OnStopParticleAsObservable
				.FirstOrDefault()
				.Subscribe(_ => 
				{
					// return to pool
					this.gameObject.GetComponent<ReturnToPool>().Return();
				}).AddTo(this);
		}
	}

	private bool CheckLayer(int layer, LayerMask objectMask)
	{
		return ((1 << layer) & objectMask) != 0;
	}
}
