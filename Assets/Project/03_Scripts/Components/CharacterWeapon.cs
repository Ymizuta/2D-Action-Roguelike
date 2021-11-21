using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
	[SerializeField] private Transform weaponHolderPosition;
	[SerializeField] private Weapon weaponToUse;

	public Weapon CurrentWeapon { get; set; }
	public WeaponAim WeaponAim { get; set; }

	protected override void Start()
	{
		base.Start();
		EquipWeapon(weaponToUse, weaponHolderPosition);
	}

	protected override void HandleInput()
	{
		if (Input.GetMouseButton(0))
		{
			Shoot();
		}

		if (Input.GetMouseButtonUp(0))
		{
			StopWeapon();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
	}

	protected override void HandleAbility()
	{
		base.HandleAbility();
	}

	private void Shoot()
	{
		CurrentWeapon.TriggerShoot();
	}

	private void StopWeapon()
	{
		CurrentWeapon.StopWeapon();
	}

	private void Reload()
	{
		CurrentWeapon.Reload();
	}

	private void EquipWeapon(Weapon weapon, Transform weaponPosition)
	{
		this.CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation, weaponPosition);
		this.CurrentWeapon.SetOwner(character);
		this.WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();
	}
}
