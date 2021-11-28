using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Collectables
{
	[SerializeField] private int healtToAdd = 1;
	[SerializeField] private ParticleSystem healthBonus = null;

	protected override void Pick()
	{
		AddHealth();
	}

	protected override void PlayEffects()
	{
		Instantiate(healthBonus, transform.position, Quaternion.identity);
	}

	private void AddHealth()
	{
		if (character != null)
		{
			character.gameObject.GetComponent<Health>().GainHealth(healtToAdd);
		}
	}
}
