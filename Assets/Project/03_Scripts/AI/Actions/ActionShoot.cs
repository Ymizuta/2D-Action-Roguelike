using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/Shoot", fileName = "ActionShoot")]
public class ActionShoot : AIAction
{
	private Vector2 aimDirection;

	public override void Act(StateController stateController)
	{
		DeterminateAim(stateController);
		ShootPlayer(stateController);
	}

	private void ShootPlayer(StateController controller)
	{
		controller.CharacterMovement.SetHorizontal(0);
		controller.CharacterMovement.SetVertical(0);

		if (controller.CharacterWeapon.CurrentWeapon != null)
		{
			controller.CharacterWeapon.CurrentWeapon.Aim.SetAim(aimDirection);
			//controller.CharacterWeapon.CurrentWeapon.Aim.RotateWeapon();
			controller.CharacterWeapon.CurrentWeapon.UseWeapon();
		}
	}

	private void DeterminateAim(StateController controller)
	{
		aimDirection = controller.Target.position - controller.transform.position;
	}
}
