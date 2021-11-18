using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
	protected float horizontalInput;
	protected float verticalInput;

	protected CharacterController controller;
	protected CharacterMovement movement;

	protected virtual void Start()
	{
		controller = this.gameObject.GetComponent<CharacterController>();
		movement = this.gameObject.GetComponent<CharacterMovement>();
	}

	private void Update()
	{
		HandleAbility();
	}

	protected virtual void HandleAbility()
	{
		InternalInput();
		HandleInput();
	}

	protected virtual void HandleInput()
	{
	}

	protected virtual void InternalInput()
	{
		this.horizontalInput = Input.GetAxisRaw("Horizontal");
		this.verticalInput = Input.GetAxisRaw("Vertical");
	}
}
