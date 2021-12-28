using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	public enum Sound
	{
		Bullet = 0,
		Dash,
		Destruction,
		EnemyShot,
		Impact,
		ItemPicked,
		Music,
		Pickup_Coin7,
	}

	[SerializeField] private AudioClip[] clips;

	private ObjectPooler soundObjectPooler;

	private void Awake()
	{
		soundObjectPooler = this.gameObject.GetComponent<ObjectPooler>();
	}

	public void PlaySound(Sound sound, float volume = 0.8f)
	{
		AudioClip clip = clips[(int)sound];
		GameObject audioPooled = soundObjectPooler.GetObjectFromPool();
		AudioSource audioSource = null;

		if (audioPooled != null)
		{
			audioPooled.SetActive(true);
			audioSource = audioPooled.GetComponent<AudioSource>();
		}

		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.Play();

		StartCoroutine(IEReturnPooledObject(audioPooled, clip.length + 1f));
	}

	private IEnumerator IEReturnPooledObject(GameObject pooledObject, float time)
	{
		yield return new WaitForSeconds(time);
		pooledObject.SetActive(false);
	}
}
