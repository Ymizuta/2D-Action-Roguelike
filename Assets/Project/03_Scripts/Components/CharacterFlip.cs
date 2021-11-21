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

	[SerializeField] private FlipMode flipMode = FlipMode.MovementDirection;
	private float threthold = 0.1f;

	public bool IsFaceRight { get; private set; } = true;

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
		if (characterWeapon.WeaponAim.CurrentAimAbsolute.x > 0f) FaceDirection(1);
		else FaceDirection(-1);
	}

	private void FaceDirection(int newDirection)
	{
		if (newDirection == 1)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
			IsFaceRight = true;
		}
		else
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
			IsFaceRight = false;
		}
	}
}
