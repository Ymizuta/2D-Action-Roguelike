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
	private float currentHealth;
	private float maxHealth;
	private float currentShield;
	private float maxShield;

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
	}

	public void UpdateHealth(float currentHealth, float maxHealth, float currentShield, float maxShield)
	{
		this.currentHealth = currentHealth;
		this.maxHealth = maxHealth;
		this.currentShield = currentShield;
		this.maxShield = maxShield;
	}
}
