using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiralPattern : BossBaseShot
{
	[Range(0f, 360f)] [SerializeField] private float startAngle = 180f;
	[Range(-360f, 360f)] [SerializeField] private float shiftAngle = 5f;
	[SerializeField] private float shotDelay = 0.5f;

	private int shotIndex;
	private float nexShotTime;

	protected override void Start()
	{
		base.Start();
		EnableShooting();
	}

	private void Update()
	{
		Shoot();
	}

	private void Shoot()
	{
		if (isShooting == false)
		{
			return;
		}

		if (nexShotTime >= 0f)
		{
			nexShotTime -= Time.deltaTime;
			if (nexShotTime >= 0f)
			{
				return;
			}
		}

		BossProjectile bossProjectile = GetBossProjectile(transform.position);

		float angle = startAngle + shiftAngle * shotIndex;

		ShootProjectile(bossProjectile, projectileSpeed, angle, projectileAcceleration);

		shotIndex++;

		if (shotIndex >= projectileAmount)
		{
			DisableShooting();
		}
		else
		{
			nexShotTime = shotDelay;
			if (nexShotTime <= 0)
			{
				Update();
				nexShotTime = 0f;
			}
		}
	}
}
