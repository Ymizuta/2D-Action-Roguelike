using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponent
{
	[SerializeField] private float walkSpeed = 10f;

	public float WalkSpeed { get; set; }

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
	}

	private void MoveCharacter()
	{
		Vector2 movement = new Vector2(horizontalInput, verticalInput);
		Vector2 movementnormalized = movement.normalized;
		Vector2 movementSpeed = movementnormalized * WalkSpeed;
		this.controller.SetMovement(movementSpeed);
	}
}
