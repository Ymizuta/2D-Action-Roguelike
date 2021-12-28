using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon : Collectables
{
	[SerializeField] private ItemData itemWeaponData = null;

	protected override void Pick()
	{
		EquipWeapon();
	}

	private void EquipWeapon()
	{
		if (character != null)
		{
			character.GetComponent<CharacterWeapon>().GetNewWeapon(itemWeaponData.Weapon);
		}
	}

	protected override void PlayEffects()
	{
		SoundManager.Instance.PlaySE(SoundManager.SE.ItemPicked, 0.6f);
	}
}
