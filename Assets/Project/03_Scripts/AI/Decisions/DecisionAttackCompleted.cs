using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decisions/DecisionAttackCompleted", fileName = "DecisionAttackCompleted")]
public class DecisionAttackCompleted : AIDecision
{
	public override bool Decide(StateController cotroller)
	{
		return AttackCompleted(cotroller);
	}

	private bool AttackCompleted(StateController controller)
	{
		if (controller.CharacterWeapon.CurrentWeapon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length
			 > controller.CharacterWeapon.CurrentWeapon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			return true;
		}
		return false;
	}
}
