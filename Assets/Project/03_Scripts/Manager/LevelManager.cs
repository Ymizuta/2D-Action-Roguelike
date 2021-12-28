using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Character character = null;
	[SerializeField] private Transform revivePosition = null;

	private void Start()
	{
		SoundManager.Instance.PlayBGM(SoundManager.BGM.Music);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			character.GetComponent<Health>().TakeDamage(1);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			character.GetComponent<Health>().Revive();
			character.transform.position = revivePosition.position;
		}
	}
}
