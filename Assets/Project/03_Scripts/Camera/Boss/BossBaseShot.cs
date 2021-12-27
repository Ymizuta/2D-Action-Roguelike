using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseShot : MonoBehaviour
{
	[SerializeField] protected int projectileAmount = 20;
	[SerializeField] protected float projectileSpeed = 2f;
	[SerializeField] protected float projectileAcceleration = 0f; 

	protected ObjectPooler pooler;
	protected  bool isShooting;
	protected Character weaponOwner;

	protected virtual void Start()
	{
		pooler = GetComponent<ObjectPooler>();
		weaponOwner = GetComponent<Character>();
	}

	protected BossProjectile GetBossProjectile(Vector3 newPosition)
	{
		GameObject bossProjectilePooled = pooler.GetObjectFromPool();
		BossProjectile bossProjectile = bossProjectilePooled.GetComponent<BossProjectile>();

		bossProjectile.transform.position = newPosition;
		bossProjectilePooled.SetActive(true);

		return bossProjectile;
	}

	protected void ShootProjectile(BossProjectile bossProjectile, float speed, float angle, float acceleration)
	{
		if (bossProjectile == null)
		{
			return;
		}
		bossProjectile.WeaponOwner = weaponOwner;
		bossProjectile.Shoot(angle, speed, acceleration);
	}

	protected virtual void EnableShooting()
	{
		isShooting = true;
	}

	protected virtual void DisableShooting()
	{
		isShooting = false;
	}
}
