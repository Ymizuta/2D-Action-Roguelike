using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
	[SerializeField] private float lifeTime = 2f;
	private Coroutine coroutine;

	public void Return()
	{
		this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		coroutine = StartCoroutine(ReturnCoroutine());
	}

	private void OnDisable()
	{
		if (coroutine == null) return;
		StopCoroutine(coroutine);
		coroutine = null;
	}

	private IEnumerator ReturnCoroutine()
	{
		yield return new WaitForSeconds(lifeTime);
		Return();
	}
}
