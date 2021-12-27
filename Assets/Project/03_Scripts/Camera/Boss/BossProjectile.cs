using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : ProjectileBase
{
	private float speed;
	private float angle;
	private float acceleration;
	private Transform thisTransform;

	private void Awake()
	{
		Initialize();
	}

	public override void Initialize()
	{
		if (isInitialized) return;

		base.Initialize();

		WeaponOwnerType = Character.CharacterType.AI;
		thisTransform = this.transform;

		isInitialized = true;
	}

	private void Update()
	{
		MoveProjectile();
	}

	public void Shoot(float newAngle, float newSpeed, float newAcceleration)
	{
		this.angle = newAngle;
		this.speed = newSpeed;
		this.acceleration = newAcceleration;

		Vector3 projectileAngle = thisTransform.rotation.eulerAngles;
		thisTransform.rotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, newAngle);

		ReuseInit();
	}

	private void MoveProjectile()
	{
		Vector3 projectileAngle = thisTransform.rotation.eulerAngles;
		Quaternion newRotation = thisTransform.rotation;

		// Set Rotation
		float angleToAdd = acceleration * Time.deltaTime;
		newRotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, projectileAngle.z + angleToAdd);

		// Apply Acceleration
		speed += acceleration * Time.deltaTime;

		// Move
		Vector3 newPosition = thisTransform.position + thisTransform.up * speed * Time.deltaTime;
		thisTransform.SetPositionAndRotation(newPosition, newRotation);
	}

	protected override void Stop()
	{
		this.speed = 0f;
	}
}
