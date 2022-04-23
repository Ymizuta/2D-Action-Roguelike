using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �{�X�p�̏���
/// </summary>
public class Boss : MonoBehaviour
{
	[SerializeField] private GameObject bossObject = null;

	private void Awake()
	{
		bossObject.SetActive(false);
		// �C�x���g���s�Ń{�X���o��
		MessageBroker.Default.Receive<GameEvent.EventData>()
		.Where(data => data.Type == GameEvent.EventType.BossFight)
		.FirstOrDefault()
		.Subscribe(_ => 
		{
			bossObject.SetActive(true);
		}).AddTo(this);
	}
}
