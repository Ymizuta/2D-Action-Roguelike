using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterComponent
{
	[SerializeField] private float dashDistance = 5f;
	[SerializeField] private float dashDuration = 0.5f;
	private bool isDashing = false;
	private float dashTimer;
	private Vector2 dashOrigin;
	private Vector2 dashDestination;

	protected override void HandleInput()
	{
		base.HandleInput();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Dash();
		}
	}

	protected override void HandleAbility()
	{
		base.HandleAbility();

		if (isDashing)
		{
			if (dashDuration < dashTimer)
			{
				StopDash();
			}
			else
			{
				Vector2 newPosition = Vector2.Lerp(dashOrigin, dashDestination, dashTimer / dashDuration);
				controller.MoveCharacter(newPosition);
				dashTimer += Time.fixedDeltaTime;
			}
		}
	}

	private void Dash()
	{
		isDashing = true;
		controller.IsNomalMove = false;
		dashTimer = 0f;
		this.dashOrigin = transform.position;
		this.dashDestination = this.dashOrigin + (controller.CurrentMovement.normalized * dashDistance);

		SoundManager.Instance.PlaySE(SoundManager.SE.Dash);
	}

	private void StopDash()
	{
		isDashing = false;
		controller.IsNomalMove = true;
	}
}
