using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Vendor/Item", fileName = "VenderItem")]
public class VendorItem : ScriptableObject
{
	[SerializeField] private Collectables itemCollectable;
	[SerializeField] private Weapon weaponToSell;
	[SerializeField] private int cost;

	public Collectables ItemCollectable => itemCollectable;
	public Weapon Weapon => weaponToSell;
	public int Cost => cost;
}
