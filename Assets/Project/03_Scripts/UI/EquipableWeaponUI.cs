using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipableWeaponUI : MonoBehaviour
{
	[SerializeField] private Text numberText = null;
	[SerializeField] private Image weaponImg = null;

	public void Initialize(int keyNumber, Sprite sprite)
	{
		this.numberText.text = $"{keyNumber}";
		this.weaponImg.sprite = sprite;
		this.weaponImg.SetNativeSize();
	}
}
