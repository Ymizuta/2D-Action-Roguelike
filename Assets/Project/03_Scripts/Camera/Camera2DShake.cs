using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Camera2DShake : MonoBehaviour
{
	[SerializeField] private CharacterWeapon characterWeapon;
	[SerializeField] private float shakeVibrato = 10f;
	[SerializeField] private float shakeRandomeness = 0.1f;
	[SerializeField] private float shakeTime = 0.01f;

	private void Awake()
	{
		// shoot event
		characterWeapon.OnShootAsObservable.Subscribe(_ => Shake()).AddTo(this);
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
