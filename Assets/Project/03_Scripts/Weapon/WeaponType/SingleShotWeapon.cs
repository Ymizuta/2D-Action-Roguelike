using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : Weapon
{
	[SerializeField] private Vector3 projectileSpawnPosition;
	[SerializeField] private Vector3 projectileSpread;

	private Vector3 ProjectileSpawnPosition;
	private Vector3 randomProjectileSpread;
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
		SoundManager.Instance.PlaySound(SoundManager.Sound.Bullet);

		Projectile projectile = pooler.GetObjectFromPool().GetComponent<Projectile>();
		projectile.transform.position = spawnPosition;
		projectile.gameObject.SetActive(true);
		projectile.Initialize();
		projectile.ReuseInit();
		projectile.GetComponent<SpriteRenderer>().flipX = false;
		projectile.WeaponOwner = WeaponOwner;

		// spread
		randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
		Quaternion spread = Quaternion.Euler(randomProjectileSpread);

		// Set direction and rotation
		bool isRight = WeaponOwner.GetComponent<CharacterFlip>().IsFaceRight;
		Vector2 newDirection = isRight ?  spread * transform.right: spread * -transform.right;
		projectile.SetDirection(newDirection, transform.rotation, isRight);

		// disable shot until next shot time
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
