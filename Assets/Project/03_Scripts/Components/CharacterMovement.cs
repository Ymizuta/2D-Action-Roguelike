using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
	[SerializeField] private float walkSpeed = 10f;

	public float WalkSpeed { get; set; }

	private readonly int movingParameter = Animator.StringToHash("Moving");

	protected override void Start()
	{
		base.Start();
		this.WalkSpeed = walkSpeed;
	}

	public void ResetSpeed()
	{
		this.WalkSpeed = walkSpeed;
	}

	protected override void HandleAbility()
	{
		base.HandleAbility();
		MoveCharacter();
		UpdateAnimation();
	}

	private void MoveCharacter()
	{
		Vector2 movement = new Vector2(horizontalInput, verticalInput);
		Vector2 movementnormalized = movement.normalized;
		Vector2 movementSpeed = movementnormalized * WalkSpeed;
		this.controller.SetMovement(movementSpeed);
	}

	private void UpdateAnimation()
	{
		if (Mathf.Abs(controller.CurrentMovement.x) >= 0.1f || Mathf.Abs(controller.CurrentMovement.y) >= 0.1f)
		{
			animator.SetBool(movingParameter, true);
		}
		else
		{
			animator.SetBool(movingParameter, false);
		}
	}
}
