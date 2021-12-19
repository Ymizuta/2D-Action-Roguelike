using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Decisions/DecisionCanSeePlayer", fileName = "DecisionCanSeePlayer")]
public class DecisionCanSeePlayer : AIDecision
{
	[SerializeField] private LayerMask obstacleMask;
	private CharacterFlip playerFlip;

	public override bool Decide(StateController cotroller)
	{
		if (playerFlip == null) { playerFlip = cotroller.Player.GetComponent<CharacterFlip>(); }
		return CanSeePlayer(cotroller);
	}

	private bool CanSeePlayer(StateController controller)
	{
		if (controller.FOV != null)
		{
			// Check the distance to player
			float viewDistance = controller.FOV.pointLightInnerRadius;
			var distanceToPlayer = (controller.Player.position - controller.transform.position).sqrMagnitude;
			if (distanceToPlayer < Mathf.Pow(viewDistance, 2f))
			{
				float viewAngle = controller.FOV.pointLightInnerAngle;
				Vector2 directionToPlayer = (controller.transform.position - controller.Player.position);
				Vector2 faceDirection = playerFlip.IsFaceRight ? Vector2.right : Vector2.left;
				float angleBtwEnemyAndPlayer = Vector2.Angle(directionToPlayer, faceDirection);

				if (!Physics2D.Linecast(controller.transform.position, controller.Player.position, obstacleMask))
				{
					if (controller.Target == null) controller.Target = controller.Player;
					if (angleBtwEnemyAndPlayer < viewAngle / 2f) return true;
				}
			}
		}
		controller.Target = null;
		return false;
	}
}
