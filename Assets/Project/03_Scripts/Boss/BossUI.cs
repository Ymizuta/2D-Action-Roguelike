using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;

public class BossUI : MonoBehaviour
{
	[Header("Canvas")]
	[SerializeField] private CanvasGroup healthBarCanvasGroup = null;
	[SerializeField] private CanvasGroup introCanvasGroup = null;

	private void Awake()
	{
		healthBarCanvasGroup.gameObject.SetActive(false);
		introCanvasGroup.gameObject.SetActive(false);

		MessageBroker.Default.Receive<GameEvent.EventData>()
			.Where(data => data.Type == GameEvent.EventType.BossFight)
			.FirstOrDefault()
			.Subscribe(_ => 
			{
				// show intro panel
				introCanvasGroup.gameObject.SetActive(true);
				StartCoroutine(IEFade(introCanvasGroup, 0f, 1f, () =>
				{
					Observable.Timer(TimeSpan.FromSeconds(1f))
					.Subscribe(__ => 
					{
						// hide intro panel
						StartCoroutine(IEFade(introCanvasGroup, 1f, 0f, ()=> 
						{
							// show health bar
							healthBarCanvasGroup.gameObject.SetActive(true);
							StartCoroutine(IEFade(healthBarCanvasGroup, 0f, 1f));
						}));
					}).AddTo(this);
				}));
			}).AddTo(this);
	}

	private IEnumerator IEFade(CanvasGroup targetGroup, float from, float to, Action action = null)
	{
		float animationTime = 0.5f;
		float timer = 0f;
		targetGroup.alpha = from;
		while (animationTime > timer)
		{
			yield return null;
			targetGroup.alpha = Mathf.Lerp(from, to, timer / animationTime);
			timer += Time.deltaTime;
		}
		targetGroup.alpha = to;
		action?.Invoke();
	}
}
