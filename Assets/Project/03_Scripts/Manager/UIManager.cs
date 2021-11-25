using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
	[SerializeField] private Image healthBar = null;
	[SerializeField] private TextMeshProUGUI healthNumberText = null;
	[SerializeField] private Image shieldBar = null;
	[SerializeField] private TextMeshProUGUI shieldNumberText = null;
	[SerializeField] private TextMeshProUGUI ammoText = null;
	private float currentHealth;
	private float maxHealth;
	private float currentShield;
	private float maxShield;
	private float currentAmmo;
	private float maxAmmo;

	private void Update()
	{
		InternalUpdate();
	}

	private void InternalUpdate()
	{
		healthNumberText.text = $"{currentHealth} / {maxHealth}";
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, Time.deltaTime * 10f);
		shieldNumberText.text = $"{currentShield} / {maxShield}";
		shieldBar.fillAmount = Mathf.Lerp(shieldBar.fillAmount, currentShield / maxShield, Time.deltaTime * 10f);

		// Weapon
		ammoText.text = $"{currentAmmo} / {maxAmmo}";
	}

	public void UpdateHealth(float currentHealth, float maxHealth)
	{
		this.currentHealth = currentHealth;
		this.maxHealth = maxHealth;
	}

	public void UpdateShield(float currentShield, float maxShield)
	{
		this.currentShield = currentShield;
		this.maxShield = maxShield;
	}

	public void UpdateWeapon(float currentAmmo, float maxAmmo)
	{
		this.currentAmmo = currentAmmo;
		this.maxAmmo = maxAmmo;
	}
}
