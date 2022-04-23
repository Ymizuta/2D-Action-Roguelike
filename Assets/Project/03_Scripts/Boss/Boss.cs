using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ボス用の処理
/// </summary>
public class Boss : MonoBehaviour
{
	[SerializeField] private GameObject bossObject = null;

	private void Awake()
	{
		bossObject.SetActive(false);
		// イベント発行でボスを出現
		MessageBroker.Default.Receive<GameEvent.EventData>()
		.Where(data => data.Type == GameEvent.EventType.BossFight)
		.FirstOrDefault()
		.Subscribe(_ => 
		{
			bossObject.SetActive(true);
		}).AddTo(this);
	}
}
