using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponent
{
	private enum FlipMode
	{
		MovementDirection,
		WeaponDirection,
	}

	private FlipMode flipMode = FlipMode.MovementDirection;
	private float threthold = 0.1f;

	protected override void HandleAbility()
	{
		base.HandleAbility();

		if (flipMode == FlipMode.MovementDirection)
		{
			FlipToMoveDirectin();
		}
		else
		{
			FlipToWeaponDirection();
		}
	}

	private void FlipToMoveDirectin()
	{
		if (controller.CurrentMovement.normalized.magnitude > threthold)
		{
			if (0 < controller.CurrentMovement.x) FaceDirection(1);
			else if (0 > controller.CurrentMovement.x) FaceDirection(-1);
		}
	}

	private void FlipToWeaponDirection()
	{
	}

	private void FaceDirection(int newDirection)
	{
		if (newDirection == 1)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
}
