using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/Weapon", fileName = "Item_Weapon")]
public class ItemData : ScriptableObject
{
	[SerializeField] private Weapon equipedWeapon = null;
	[SerializeField] private Sprite weaponSprite = null;

	public Weapon Weapon => equipedWeapon;
	public Sprite WeaponSprite => weaponSprite;
}
