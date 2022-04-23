using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameEvent : MonoBehaviour
{
	public enum EventType
	{
		BossFight,
		BossDefeated,
		BossGateGimmick, // ボス前ギミック
	}

	[SerializeField] private EventType eventType;
	[SerializeField] private LayerMask eventLayer;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (MyLibrary.CheckLayer(collision.gameObject.layer, eventLayer))
		{
			MessageBroker.Default.Publish(new EventData() { Type = eventType});
			this.gameObject.SetActive(false);
		}
	}

	public struct EventData
	{
		public EventType Type;
	}
}
