using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private float timeBtwShoot = 0.5f;
	[SerializeField] private bool useMagazine = true;
	[SerializeField] private int maxMagazineSize = 30;
	[SerializeField] private bool autoReload = true;
	public Character WeaponOwner { get; set; }
	public WeaponAmmo WeaponAmmo { get; set; }
	public int MaxMagazineSize => maxMagazineSize;
	public bool UseMagazine => useMagazine;
	public int CurrentAmmo { get; set; }
	public bool CanShoot { get; set; }

	private float nextShootTime;

	private void Awake()
	{
		WeaponAmmo = this.gameObject.GetComponent<WeaponAmmo>();
	}

	private void Update()
	{
		WeaponCanShoot();
	}

	public void TriggerShoot()
	{
		StartShooting();
	}

	private void StartShooting()
	{
		if (useMagazine)
		{
			if (WeaponAmmo.CanUseWeapon())
			{
				RequestShoot();
			}else if (autoReload)
			{
				Reload();
			}
		}
		else
		{
			RequestShoot();
		}
	}

	private void RequestShoot()
	{
		if (!CanShoot)
		{
			return;
		}

		Debug.Log($"Shooting Ammo : {CurrentAmmo}");
		WeaponAmmo.ConsumeAmmo();
		CanShoot = false;
	}

	private void WeaponCanShoot()
	{
		if (Time.time >= nextShootTime)
		{
			CanShoot = true;
			this.nextShootTime = Time.time + timeBtwShoot;
		}
	}

	public void Reload()
	{
		Debug.Log("Reload");
		WeaponAmmo.RefillAmmo();
	}

	public void SetOwner(Character owner)
	{
		this.WeaponOwner = owner;
	}
}
