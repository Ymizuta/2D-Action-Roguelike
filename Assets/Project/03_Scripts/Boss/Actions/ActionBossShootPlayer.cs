using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AI/Action/BossShootPlayer", fileName = "Boss Shoot Player")]
public class ActionBossShootPlayer : AIAction
{
	private BossRandomPattern RandomPattern { get; set; }
	private BossSpiralPattern SpriralPattern { get; set; }
	private BossCirclePattern CirclePattern { get; set; }
	private Health Health { get; set; }

	private BossBaseShot currentShot;
	private float shotDelay = 3f;
	private float nextShotTime;

	public override void Act(StateController stateController)
	{
		Shoot(stateController);
	}

	private void Shoot(StateController controller)
	{
		if (Time.time >= nextShotTime)
		{
			if (Health == null) Health = controller.Character.GetComponent<Health>();

			if (CirclePattern == null) CirclePattern = controller.Character.GetComponent<BossCirclePattern>();
			if (RandomPattern == null) RandomPattern = controller.Character.GetComponent<BossRandomPattern>();
			if (SpriralPattern== null) SpriralPattern = controller.Character.GetComponent<BossSpiralPattern>();

			float ratio = Health.CurrentHealth.Value / Health.MaxHealth;
			if (ratio >= 0.8f)
			{
				currentShot = CirclePattern;
			}
			else if (ratio >= 0.4f)
			{
				currentShot = RandomPattern;
			}
			else
			{
				currentShot = SpriralPattern;
			}
			currentShot.EnableShooting();
			nextShotTime += shotDelay;
		}
	}

	private void OnEnable()
	{
		nextShotTime = 0f;
	}
}
