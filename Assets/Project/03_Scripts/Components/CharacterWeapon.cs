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
		CreateWeapon(weapon, weaponHolderPosition);
	}

	protected override void Start()
	{
		base.Start();
		this.health = this.gameObject.GetComponent<Health>();
		CreateWeapon(weaponToUse, weaponHolderPosition);
		EquipWeapon(0);
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
			EquipWeapon(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && ownedWeapons.Count > 1)
		{
			EquipWeapon(1);
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

	private void CreateWeapon(Weapon weapon, Transform weaponPosition)
	{
		var newWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation, weaponPosition);
		newWeapon.Initialize(character);
		newWeapon.Returned();
		this.ownedWeapons.Add(newWeapon);
	}

	private void EquipWeapon(int idx)
	{
		if (CurrentWeapon != null)
		{
			// hide weapon & reticle
			CurrentWeapon.Returned();
			WeaponAim.HideReticle();
			//Destroy(GameObject.Find("Pool"));
		}

		// change weapon
		this.CurrentWeapon = ownedWeapons[idx];
		this.CurrentWeapon.Equiped();
		// change reticle
		this.WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();
		this.WeaponAim.ShowReticle();

		if (character.Type == Character.CharacterType.Player)
		{
			UIManager.Instance.UpdateWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MaxMagazineSize);
			UIManager.Instance.UpdateWeaponImage(this.CurrentWeapon.GetComponentInChildren<SpriteRenderer>().sprite);
		}
	}
}
