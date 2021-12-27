using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ProjectileBase : MonoBehaviour
{
	[Header("Effect")]
	[SerializeField] protected ParticleSystem inpulseEffect = null;

	[SerializeField] protected LayerMask objectMask;

	protected Rigidbody2D rigidbody2d;
	protected SpriteRenderer spriteRenderer;
	protected Collider2D collider;

	protected bool isInitialized = false;

	public Character.CharacterType WeaponOwnerType { get; set; }

	public virtual void Initialize()
	{
		this.rigidbody2d = this.gameObject.GetComponent<Rigidbody2D>();
		this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		this.collider = this.gameObject.GetComponent<Collider2D>();
	}

	public virtual void ReuseInit()
	{
		spriteRenderer.enabled = true;
		rigidbody2d.isKinematic = false;
		collider.enabled = true;
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

	protected virtual void Stop()
	{
	}

	private bool CheckLayer(int layer, LayerMask objectMask)
	{
		return ((1 << layer) & objectMask) != 0;
	}
}
