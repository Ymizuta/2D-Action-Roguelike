using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/ActionFollow", fileName = "ActionFollow")]
public class ActionFollow : AIAction
{
	private float minDistanceToFollow = 1f;

	public override void Act(StateController stateController)
	{
		FollowTarget(stateController);
	}

	private void FollowTarget(StateController controller)
	{
		if (controller.Target == null)
		{
			return;
		}

		float horizontalVal = controller.transform.position.x < controller.Target.position.x ? 1 : -1;
		controller.CharacterMovement.SetHorizontal(horizontalVal);

		float verticalVal = controller.transform.position.y < controller.Target.position.y ? 1 : -1;
		controller.CharacterMovement.SetVertical(verticalVal);

		if (Mathf.Abs(controller.transform.position.x - controller.Target.position.x) < minDistanceToFollow)
		{
			controller.CharacterMovement.SetHorizontal(0);
		}
		if (Mathf.Abs(controller.transform.position.y - controller.Target.position.y) < minDistanceToFollow)
		{
			controller.CharacterMovement.SetVertical(0);
		}
	}
}
