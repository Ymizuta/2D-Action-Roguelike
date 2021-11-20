using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public float CurrentHealth { get; set; }
	public float CurrentShield { get; set; }

	private void Awake()
	{
		this.CurrentHealth = initHealth;
		this.CurrentShield = initShield;
		this.character = this.gameObject.GetComponent<Character>();
		this.controller = this.gameObject.GetComponent<CharacterController>();
		this.collider = this.gameObject.GetComponent<BoxCollider2D>();
		this.renderer = this.gameObject.GetComponent<SpriteRenderer>();
		UIManager.Instance.UpdateHealth(this.CurrentHealth, this.maxHealth, this.CurrentShield, this.maxShield);
	}

	public void TakeDamage(int damage)
	{
		if (CurrentHealth <= 0 )
		{
			return;
		}

		if (0 < CurrentShield)
		{
			CurrentShield -= damage;
			if (CurrentShield < 0) CurrentShield = 0;
			UIManager.Instance.UpdateHealth(this.CurrentHealth, this.maxHealth, this.CurrentShield, this.maxShield);
			return;
		}

		this.CurrentHealth -= damage;
		if (this.CurrentHealth < 0) this.CurrentHealth = 0;
		UIManager.Instance.UpdateHealth(this.CurrentHealth, this.maxHealth, this.CurrentShield, this.maxShield);
		if (CurrentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		this.character.enabled = false;
		this.controller.enabled = false;
		this.collider.enabled = false;
		this.renderer.enabled = false;
	}

	public void Revive()
	{
		this.character.enabled = true;
		this.controller.enabled = true;
		this.collider.enabled = true;
		this.renderer.enabled = true;
		if (destroyObject)
		{
			this.gameObject.SetActive(true);
		}
		this.CurrentHealth = initHealth;
		this.CurrentShield = initShield;
		UIManager.Instance.UpdateHealth(this.CurrentHealth, this.maxHealth, this.CurrentShield, this.maxShield);
	}

	private void DestroyObject()
	{
		if (destroyObject)
		{
			this.gameObject.SetActive(false);
		}
	}
}
