using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���h���b�v���s���N���X
/// </summary>
public class ItemDropper : MonoBehaviour
{
	[SerializeField] private GameObject[] dropItemPrefabs;

	// todo : �d�݂��������I����������
	public void Drop()
	{
		var idx = Random.Range(0, dropItemPrefabs.Length);
		Instantiate(dropItemPrefabs[idx],  this.gameObject.transform.position, default);
	}
}
