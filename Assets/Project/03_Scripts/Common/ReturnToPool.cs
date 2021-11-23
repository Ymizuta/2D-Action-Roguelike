using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
	[SerializeField] private float lifeTime = 2f;
	[SerializeField] private LayerMask objectMask;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (CheckLayer(collision.gameObject.layer, objectMask))
		{
			Return();
		}
	}

	private bool CheckLayer(int layer, LayerMask objectMask)
	{
		return ((1 << layer) & objectMask) != 0;
	}
}
