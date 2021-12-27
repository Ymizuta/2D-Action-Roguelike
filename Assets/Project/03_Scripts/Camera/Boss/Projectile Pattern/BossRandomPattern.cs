using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRandomPattern : BossBaseShot
{
	[Range(0f, 360f)][SerializeField] private float startAngle = 180f;
	[Range(0f, 360f)] [SerializeField] private float range = 360f;

	[SerializeField] private float minRandomSpeed = 1f;
	[SerializeField] private float maxRandomSpeed = 3f;

	[SerializeField] private float minDelay = 0.01f;
	[SerializeField] private float maxDelay = 0.1f;

	private float nextShotTime;
	private int shotIndex;

	private void Update()
	{
		Shoot();
	}

	private void Shoot()
	{
		if (!isShooting)
		{
			return;
		}

		if (nextShotTime >= 0f)
		{
			nextShotTime -= Time.deltaTime;
			if (nextShotTime >= 0f)
			{
				return;
			}
		}

		BossProjectile bossProjectile = GetBossProjectile(transform.position);
		if (bossProjectile == null)
		{
			return;
		}
		float speed = Random.Range(minRandomSpeed, maxRandomSpeed);
		float minAngle = startAngle - range / 2;
		float maxAngle = startAngle + range / 2f;
		float angle = Random.Range(minAngle, maxAngle);

		ShootProjectile(bossProjectile, speed, angle, projectileAcceleration);

		shotIndex++;

		if (shotIndex >= projectileAmount)
		{
			DisableShooting();
		}
		else
		{
			nextShotTime = Random.Range(minDelay, maxDelay);
			if (nextShotTime <= 0)
			{
				Update();
				nextShotTime = 0f;
			}
		}
	}

	public override void EnableShooting()
	{
		base.EnableShooting();
		shotIndex = 0;
	}
}
