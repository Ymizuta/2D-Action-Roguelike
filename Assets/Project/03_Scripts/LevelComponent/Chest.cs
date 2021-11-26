using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	[SerializeField] private GameObject[] rewards;
	[SerializeField] private float xRandomPosition = 1f;
	[SerializeField] private float yRandomPosition = 1f;

	private readonly int rewardedParameter = Animator.StringToHash("Rewarded");

	private bool canReward;
	private bool isRewardDelivered;
	private Vector3 randomPosition;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			if(canReward) RewardPlayer();
		}
	}

	private void RewardPlayer()
	{
		if (isRewardDelivered) return;
		randomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
		randomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);
		Instantiate(SelectRandomReward(), transform.position + randomPosition, Quaternion.identity);
		this.gameObject.GetComponent<Animator>().SetTrigger(rewardedParameter);
		isRewardDelivered = true;
	}

	private GameObject SelectRandomReward()
	{
		int randomRewardIdx = (int)Random.Range(0, rewards.Length);
		return rewards[randomRewardIdx];
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			canReward = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			canReward = false;
		}
	}
}
