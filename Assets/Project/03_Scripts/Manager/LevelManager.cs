using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private Character player = null;
	[SerializeField] private Transform revivePosition = null;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		// bgm
		//SoundManager.Instance.PlayBGM(SoundManager.BGM.Music);

		// player
		var playerHealth = player.GetComponent<Health>();
		playerHealth.OnDieAsObservable
			.Do(_ => UIManager.Instance.ShowDieMessage())
			.Delay(System.TimeSpan.FromSeconds(2f))
			.Subscribe(_ => 
			{
				UIManager.Instance.HideDieMessageImmidiately();
				playerHealth.Revive();
				player.transform.position = revivePosition.position;
			}).AddTo(this);
	}
}
