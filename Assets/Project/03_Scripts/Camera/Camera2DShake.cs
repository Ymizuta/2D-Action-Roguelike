using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DShake : MonoBehaviour
{
	[SerializeField] private float shakeVibrato = 10f;
	[SerializeField] private float shakeRandomeness = 0.1f;
	[SerializeField] private float shakeTime = 0.01f;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			Shake();
		}
	}

	public void Shake()
	{
		StartCoroutine(IEShake());
	}

	private IEnumerator IEShake()
	{
		for (int i = 0; i < shakeVibrato; i++)
		{
			Vector3 shakePosition = transform.position + Random.onUnitSphere * shakeRandomeness;
			yield return new WaitForSeconds(shakeTime);
			transform.position = shakePosition;
		}
	}
}
