using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Health : MonoBehaviour
{
	[Header("Health")]
	[SerializeField] private float initHealth = 10f;
	[SerializeField] private float maxHealth = 10f;

	[Header("Shield")]
	[SerializeField] private float initShield = 10f;
	[SerializeField] private float maxShield = 10f;

	private bool destroyObject;
	private Character character;
	private CharacterController controller;
	private BoxCollider2D collider;
	private SpriteRenderer renderer;
	private bool isPlayer;

	private Subject<Unit> onDieSubject = new Subject<Unit>();

	public ReactiveProperty<float> CurrentHealth { get; set; } = new ReactiveProperty<float>();
	public ReactiveProperty<float> CurrentShield { get; set; } = new ReactiveProperty<float>();
	public float MaxHealth => maxHealth;

	public IObservable<Unit> OnDieAsObservable => onDieSubject;

	public void Initalize(bool isPlayer = false)
	{
		this.CurrentHealth.Value = initHealth;
		this.CurrentShield.Value = initShield;
		this.character = this.gameObject.GetComponent<Character>();
		this.controller = this.gameObject.GetComponent<CharacterController>();
		this.collider = this.gameObject.GetComponent<BoxCollider2D>();
		// todo ????????
		this.renderer = isPlayer ? character.CharacterSprite : this.gameObject.GetComponent<SpriteRenderer>();
		this.isPlayer = isPlayer;

		if (isPlayer)
		{
			this.CurrentHealth
				.Subscribe(health =>
				{
					if (health < 0) health = 0;
					UIManager.Instance.UpdateHealth(health, this.maxHealth);
				}).AddTo(this);
			this.CurrentShield
				.Subscribe(shield =>
				{
					if (shield < 0) shield = 0;
					UIManager.Instance.UpdateShield(this.CurrentShield.Value, this.maxShield);
				}).AddTo(this);
		}
	}

	public void TakeDamage(int damage)
	{
		if (CurrentHealth.Value <= 0 )
		{
			return;
		}

		if (0 < CurrentShield.Value)
		{
			CurrentShield.Value -= damage;
			if (CurrentShield.Value < 0) CurrentShield.Value = 0;
			return;
		}

		this.CurrentHealth.Value -= damage;
		if (this.CurrentHealth.Value < 0) this.CurrentHealth.Value = 0;
		if (CurrentHealth.Value <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// todo ????
		if (isPlayer)
		{
			this.collider.enabled = false;
			this.renderer.enabled = false;
		}else
		{
			gameObject.SetActive(false);

			// ?A?C?e???h???b?v
			this.gameObject.GetComponent<ItemDropper>()?.Drop();
			// ?????\??
			this.gameObject.GetComponent<CharacterSpawner>()?.ReserveSpawn(10f, 
				() => 
				{
					this.CurrentHealth.Value = maxHealth;
					this.CurrentShield.Value = maxShield;
					this.gameObject.GetComponent<EnemyHealth>().UpdateHealthBar();
				});
		}
		this.onDieSubject.OnNext(default);
	}

	public void Revive()
	{
		// todo ????
		if (isPlayer)
		{
			this.collider.enabled = true;
			this.renderer.enabled = true;
		}
		if (destroyObject)
		{
			this.gameObject.SetActive(true);
		}
		this.CurrentHealth.Value = initHealth;
		this.CurrentShield.Value = initShield;
	}

	public void GainHealth(int amount)
	{
		this.CurrentHealth.Value = Mathf.Min(CurrentHealth.Value + amount, maxHealth);
	}

	public void GainShield(int amount)
	{
		this.CurrentShield.Value = Mathf.Min(CurrentShield.Value + amount, maxShield);
	}

	private void DestroyObject()
	{
		if (destroyObject)
		{
			this.gameObject.SetActive(false);
		}
	}
}
