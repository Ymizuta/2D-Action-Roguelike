using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decisions/Range To Attack", fileName = "DecisionToAttack")]
public class DecisionToAttack : AIDecision
{
	[SerializeField] private float minDistanceToAttack = 1.5f;

	public override bool Decide(StateController cotroller)
	{
		return PlayerInRangeToAttack(cotroller);
	}

	private bool PlayerInRangeToAttack(StateController controller)
	{
		if (controller.Target != null)
		{
			float distanceToPlayer = (controller.Target.position - controller.transform.position).sqrMagnitude;
			return distanceToPlayer < minDistanceToAttack;
		}
		return false;
	}
}
