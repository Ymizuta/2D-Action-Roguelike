using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShield : Collectables
{
	[SerializeField] private int shieldToAdd = 1;
	[SerializeField] private ParticleSystem healthBonus = null;

	protected override void Pick()
	{
		AddShield();
	}

	protected override void PlayEffects()
	{
		Instantiate(healthBonus, transform.position, Quaternion.identity);
	}

	private void AddShield()
	{
		if (character != null)
		{
			character.GetComponent<Health>().GainShield(shieldToAdd);
		}
	}
}
