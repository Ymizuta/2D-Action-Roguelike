using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/ActionMellee", fileName = "ActionMellee")]
public class ActionMeleeAttack : AIAction
{
	public override void Act(StateController stateController)
	{
		Attack(stateController);
	}

	private void Attack(StateController controller)
	{
		controller.CharacterMovement.SetHorizontal(0);
		controller.CharacterMovement.SetVertical(0);

		controller.CharacterWeapon.CurrentWeapon.UseWeapon();
	}
}
