using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogUI : MonoBehaviour
{
	[SerializeField] private CanvasGroup canvasGroup = null;
	[SerializeField] private Text logText = null;

	private Tween tween;

	private void Awake()
	{
		// ‰Šú‰»Žž‚Í“§–¾‚É‚µ‚Ä‚¨‚­
		canvasGroup.alpha = 0f;
		this.gameObject.SetActive(false);
	}

	public void Show()
	{
		this.gameObject.SetActive(true);
		// fade in
		if (tween != null) tween.Kill();
		tween = canvasGroup.DOFade(1f, 0.5f).From(0f);
	}

	public void Hide()
	{
		// fade out
		if (tween != null) tween.Kill();
		canvasGroup.DOFade(0f, 0.5f)
			.From(1f)
			.OnComplete(() => 
			{
				this.gameObject.SetActive(false);
			});
	}

	public void SetLog(string logMessage)
	{
		this.logText.text = logMessage;
	}
}
