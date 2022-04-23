using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムドロップを行うクラス
/// </summary>
public class ItemDropper : MonoBehaviour
{
	[SerializeField] private GameObject[] dropItemPrefabs;

	// todo : 重みをつけた抽選処理を入れる
	public void Drop()
	{
		var idx = Random.Range(0, dropItemPrefabs.Length);
		Instantiate(dropItemPrefabs[idx],  this.gameObject.transform.position, default);
	}
}
