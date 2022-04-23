using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

/// <summary>
/// 画面中央に表示されるメッセージUI
/// </summary>
public class CenterMessageUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup canvasGroup = null;

	public void Show(float stayTime = 0f)
	{
		this.gameObject.SetActive(true);
		this.canvasGroup.DOFade(1f, 1f).From(0f);
		if (stayTime > 0)
		{
			Observable.Timer(TimeSpan.FromSeconds(stayTime)).Subscribe(_ => Hide()).AddTo(this);
		}
	}

	public void Hide()
	{
		this.canvasGroup.DOFade(0f, 1f)
			.From(1f)
			.OnComplete(() => HideImmidiately());
	}

	public void HideImmidiately()
	{
		this.gameObject.SetActive(false);
	}
}
