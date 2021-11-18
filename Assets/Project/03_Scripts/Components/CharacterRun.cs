using UnityEngine;
using System.Collections;

public class CharacterRun : CharacterComponent
{
	[SerializeField] private float runSpeed = 10f;

	protected override void HandleInput()
	{
		base.HandleInput();

		if (Input.GetKey(KeyCode.LeftShift))
		{
			Run();
		}

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			StopRun();
		}
	}

	private void Run()
	{
		movement.WalkSpeed = runSpeed;
	}

	private void StopRun()
	{
		movement.ResetSpeed();
	}
}
