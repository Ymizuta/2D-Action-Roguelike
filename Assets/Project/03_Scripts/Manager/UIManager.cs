using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UniRx.Triggers;

public class UIManager : Singleton<UIManager>
{
	[Header("Health")]
	[SerializeField] private Image healthBar = null;
	[SerializeField] private TextMeshProUGUI healthNumberText = null;

	[Header("Shield")]
	[SerializeField] private Image shieldBar = null;
	[SerializeField] private TextMeshProUGUI shieldNumberText = null;

	[Header("Weapon")]
	[SerializeField] private TextMeshProUGUI ammoText = null;
	[SerializeField] private Image weaponImg = null;

	[Header("Coin")]
	[SerializeField] private TextMeshProUGUI coinText = null;

	[Header("Dialog")]
	[SerializeField] private DialogUI dialogUI = null;

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

	public void UpdateCoin(int coin)
	{
		this.coinText.text = coin.ToString();
	}

	public void UpdateWeaponImage(Sprite sprite)
	{
		weaponImg.sprite = sprite;
		weaponImg.SetNativeSize();
	}

	public void ShowDialog(string message, float stayTime = 0f)
	{
		this.dialogUI.SetLog(message);
		this.dialogUI.Show();
		if (stayTime > 0)
		{
			Observable.Timer(System.TimeSpan.FromSeconds(stayTime))
				.Subscribe(_ => HideDiaLog()).AddTo(this);
		}
	}

	public void HideDiaLog()
	{
		this.dialogUI.Hide();
	}
}
