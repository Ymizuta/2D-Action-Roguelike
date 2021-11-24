using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private float timeBtwShoot = 0.5f;
	[SerializeField] private bool useMagazine = true;
	[SerializeField] private int maxMagazineSize = 30;
	[SerializeField] private bool autoReload = true;
	[SerializeField] private float recoileForce = 5f;

	[Header("Effect")]
	[SerializeField] private ParticleSystem mazzleEffect = null;

	public Character WeaponOwner { get; set; }
	public WeaponAmmo WeaponAmmo { get; set; }
	public int MaxMagazineSize => maxMagazineSize;
	public bool UseMagazine => useMagazine;
	public int CurrentAmmo { get; set; }
	public bool CanShoot { get; set; }

	private readonly int weaponUseParameter = Animator.StringToHash("WeaponUse");

	private float nextShootTime;
	private CharacterController controller;
	private Animator animator;

	private void Awake()
	{
		WeaponAmmo = this.gameObject.GetComponent<WeaponAmmo>();
		animator = this.gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		WeaponCanShoot();
		RotateWeapon();
	}

	public void StopWeapon()
	{
		controller.ApplyRecoile(Vector2.zero);
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

	protected virtual void RequestShoot()
	{
		if (!CanShoot)
		{
			return;
		}
		animator.SetTrigger(weaponUseParameter);
		WeaponAmmo.ConsumeAmmo();
		mazzleEffect.Play();
		Recoile();
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
		this.controller = owner.GetComponent<CharacterController>();
	}

	private void Recoile()
	{
		var direction = WeaponOwner.GetComponent<CharacterFlip>().IsFaceRight ? Vector2.left : Vector2.right;
		controller.ApplyRecoile(direction * recoileForce);
	}

	private void RotateWeapon()
	{
		if (WeaponOwner.gameObject.GetComponent<CharacterFlip>().IsFaceRight)
		{
			transform.localScale = Vector3.one;
		}
		else
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
}
