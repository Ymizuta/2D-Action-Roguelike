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

	private void Awake()
	{
		WeaponAmmo = this.gameObject.GetComponent<WeaponAmmo>();
	}

	public void TriggerShoot()
	{
		Shoot();
	}

	private void Shoot()
	{
		Debug.Log("Shooting");
		WeaponAmmo.ConsumeAmmo();
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
