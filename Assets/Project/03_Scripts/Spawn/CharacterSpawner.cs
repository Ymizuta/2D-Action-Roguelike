using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CharacterSpawner : MonoBehaviour
{
	[SerializeField] private Transform spawnTransform = null;

	public void ReserveSpawn(float interval, Action action = null)
	{
		if (spawnTransform == null) return;

		// ˆê’èŽžŠÔŒã‚É•œŠˆ
		Observable.Timer(TimeSpan.FromSeconds(10f))
			.Subscribe(_ =>
			{
				transform.position = spawnTransform.position;
				gameObject.SetActive(true);
				action?.Invoke();
			}).AddTo(this);
	}
}
