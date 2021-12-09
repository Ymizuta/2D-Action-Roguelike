using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/ActionWander", fileName = "ActionWander")]
public class ActionWander : AIAction
{
	[Header("Wander Settings")]
	[SerializeField] private float wanderArea = 3f;
	[SerializeField] private float wanderTime = 2f;

	[Header("Obstacle Settings")]
	[SerializeField] private Vector2 obstacleBoxCheckSize = new Vector2(2f, 2f);
	[SerializeField] private LayerMask obstacleMask;

	private Vector2 wanderDirection;
	private float wanderCheckTime;

	public override void Act(StateController stateController)
	{
		Wander(stateController);
		EvaluateObstacles(stateController);
	}

	private void Wander(StateController controller)
	{
		if (Time.time > wanderCheckTime)
		{
			wanderDirection.x = UnityEngine.Random.Range(-wanderArea, wanderArea);
			wanderDirection.y = UnityEngine.Random.Range(-wanderArea, wanderArea);

			controller.CharacterMovement.SetHorizontal(wanderDirection.x);
			controller.CharacterMovement.SetVertical(wanderDirection.y);

			wanderCheckTime = Time.time + wanderTime;
		}
	}

	private void EvaluateObstacles(StateController controller)
	{
		RaycastHit2D hit = Physics2D.BoxCast(
			controller.Collider2D.bounds.center, 
			obstacleBoxCheckSize, 
			0f, 
			wanderDirection, 
			wanderDirection.magnitude, 
			obstacleMask);

		if (hit)
		{
			wanderCheckTime = Time.time;
		}
	}

	private void OnEnable()
	{
		wanderCheckTime = 0f;
	}
}
