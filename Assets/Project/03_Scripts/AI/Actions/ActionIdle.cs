using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/ActionIdle", fileName = "ActionIdle")]
public class ActionIdle : AIAction
{
	private CharacterMovement movement;

	public override void Act(StateController stateController)
	{
		stateController.CharacterMovement.SetHorizontal(0);
		stateController.CharacterMovement.SetVertical(0);
	}
}
