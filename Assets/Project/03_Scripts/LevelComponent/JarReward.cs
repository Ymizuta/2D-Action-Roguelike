using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarReward : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float xRandomPosition = 2f;

	[SerializeField] private float yRandomPosition = 2f;
	[SerializeField] [Range(0, 100)] private float chanceToDrop = 50f;

	[Header("Rewards")]
	[SerializeField] private GameObject[] rewards;

	private Vector3 randomPosition;

	public void GiveReward()
	{
		float probability = Random.Range(0, 100);
		if (probability < chanceToDrop)
		{
			randomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
			randomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);
			Instantiate(SelectReward(), transform.position + randomPosition, Quaternion.identity);
		}
	}

	private GameObject SelectReward()
	{
		int randomIdx = Random.Range(0, rewards.Length);
		return rewards[randomIdx];
	}
}
