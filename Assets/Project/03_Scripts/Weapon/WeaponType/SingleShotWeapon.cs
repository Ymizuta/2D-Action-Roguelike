using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : Weapon
{
	[SerializeField] private Vector3 projectileSpawnPosition;

	private Vector3 ProjectileSpawnPosition;
	private ObjectPooler pooler;

	private void Start()
	{
		pooler = this.gameObject.GetComponent<ObjectPooler>();
	}

	protected override void RequestShoot()
	{
		base.RequestShoot();
		if (CanShoot)
		{
			EvaluateProjectileSpawnPosition();
			SpawnProjectile(ProjectileSpawnPosition);
		}
	}

	private void SpawnProjectile(Vector2 spawnPosition)
	{
		Projectile projectile = pooler.GetObjectFromPool().GetComponent<Projectile>();
		projectile.transform.position = spawnPosition;
		projectile.gameObject.SetActive(true);
		projectile.Initialize();

		bool isRight = WeaponOwner.GetComponent<CharacterFlip>().IsFaceRight;
		Vector2 newDirection = isRight ? transform.right : -transform.right;
		projectile.SetDirection(newDirection, transform.rotation, isRight);

		CanShoot = false;
	}

	private void EvaluateProjectileSpawnPosition()
	{
		if (WeaponOwner.GetComponent<CharacterFlip>().IsFaceRight)
		{
			ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
		}
		else
		{
			Vector3 val = projectileSpawnPosition;
			val.y = -projectileSpawnPosition.y;
			ProjectileSpawnPosition = transform.position - transform.rotation * val;
		}
	}

	private void OnDrawGizmosSelected()
	{
		EvaluateProjectileSpawnPosition();

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
	}
}
