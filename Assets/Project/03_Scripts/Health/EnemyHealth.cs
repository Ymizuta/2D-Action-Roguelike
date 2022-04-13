using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private GameObject healthBarObject = null;
	[SerializeField] private Image healthBar = null;
	[SerializeField] private int damage = 1;

	private Health enemyHealth;

	private void Start()
	{
		enemyHealth = this.gameObject.GetComponent<Health>();
		UpdateHealthBar();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			TakeDamage();
			UpdateHealthBar();
			if (healthBarObject != null && enemyHealth.CurrentHealth.Value <= 0) healthBarObject.SetActive(false);
		}
	}

	private void TakeDamage()
	{
		enemyHealth.TakeDamage(damage);
	}

	public void UpdateHealthBar()
	{
		if(healthBar != null) healthBar.fillAmount = enemyHealth.CurrentHealth.Value / enemyHealth.MaxHealth;
	}
}
