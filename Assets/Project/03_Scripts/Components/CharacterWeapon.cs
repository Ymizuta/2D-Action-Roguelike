using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharacterWeapon : CharacterComponent
{
	[SerializeField] private Transform weaponHolderPosition;
	[SerializeField] private Weapon weaponToUse;

	private List<Weapon> ownedWeapons = new List<Weapon>();
	private Health health;

	public Weapon CurrentWeapon { get; set; }
	public WeaponAim WeaponAim { get; set; }

	// event
	private Subject<Unit> onShootSubject = new Subject<Unit>();

	public IObservable<Unit> OnShootAsObservable => onShootSubject;

	public void GetNewWeapon(Weapon weapon)
	{
		// get weapon
		ownedWeapons.Add(weapon);
	}

	protected override void Start()
	{
		base.Start();
		this.health = this.gameObject.GetComponent<Health>();
		EquipWeapon(weaponToUse, weaponHolderPosition);

		// get weapon
		this.ownedWeapons.Add(weaponToUse);
	}

	protected override void HandleInput()
	{
		if (Input.GetMouseButton(0))
		{
			if(health.CurrentHealth.Value > 0)Shoot();
		}

		if (Input.GetMouseButtonUp(0))
		{
			StopWeapon();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			if (health.CurrentHealth.Value > 0) Reload();
		}

		// Equip weapon
		if (Input.GetKeyDown(KeyCode.Alpha1) && ownedWeapons.Count > 1)
		{
			EquipWeapon(ownedWeapons[0], weaponHolderPosition);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && ownedWeapons.Count > 1)
		{
			EquipWeapon(ownedWeapons[1], weaponHolderPosition);
		}
	}

	protected override void HandleAbility()
	{
		base.HandleAbility();
	}

	private void Shoot()
	{
		CurrentWeapon.TriggerShoot();
		if (character.Type == Character.CharacterType.Player)
		{
			UIManager.Instance.UpdateWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MaxMagazineSize);
			onShootSubject.OnNext(default);
		}
	}

	private void StopWeapon()
	{
		CurrentWeapon.StopWeapon();
	}

	private void Reload()
	{
		CurrentWeapon.Reload();
		if (character.Type == Character.CharacterType.Player) UIManager.Instance.UpdateWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MaxMagazineSize);
	}

	private void EquipWeapon(Weapon weapon, Transform weaponPosition)
	{
		if (CurrentWeapon != null)
		{
			WeaponAim.DestroyReticle();
			Destroy(GameObject.Find("Pool"));
			Destroy(CurrentWeapon.gameObject);
		}

		this.CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation, weaponPosition);
		this.CurrentWeapon.SetOwner(character);
		this.WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

		if (character.Type == Character.CharacterType.Player) UIManager.Instance.UpdateWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MaxMagazineSize);
	}
}
