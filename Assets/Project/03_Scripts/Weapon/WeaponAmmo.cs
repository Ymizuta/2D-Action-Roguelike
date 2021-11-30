using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
	private Weapon weapon;

	public void Initialize()
	{
		this.weapon = this.gameObject.GetComponent<Weapon>();
		RefillAmmo();
	}

	public void ConsumeAmmo()
	{
		if (!weapon.UseMagazine || weapon.CurrentAmmo <= 0) return;
		weapon.CurrentAmmo--;
	}

	public bool CanUseWeapon()
	{
		return weapon.CurrentAmmo > 0;
	}

	public void RefillAmmo()
	{
		if (!weapon.UseMagazine) return;
		weapon.CurrentAmmo = weapon.MaxMagazineSize;
	}
}
