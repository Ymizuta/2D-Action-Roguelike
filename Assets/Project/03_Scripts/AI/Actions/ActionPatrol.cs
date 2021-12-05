using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/ActionPatrol", fileName = "ActionPatrol")]
public class ActionPatrol : AIAction
{
	private Vector2 newDirection;

	public override void Act(StateController stateController)
	{
		Patrol(stateController);
	}

	private void Patrol(StateController controller)
	{
		newDirection = controller.Path.CurrentPoint - controller.transform.position;
		newDirection = newDirection.normalized;

		controller.CharacterMovement.SetHorizontal(newDirection.x);
		controller.CharacterMovement.SetVertical(newDirection.y);
	}
}
