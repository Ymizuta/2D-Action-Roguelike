using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	public enum SE
	{
		Bullet = 0,
		Dash,
		Destruction,
		EnemyShot,
		Impact,
		ItemPicked,
		Pickup_Coin7,
	}

	public enum BGM
	{
		Music = 0,
	}

	[SerializeField] private AudioClip[] seClips;
	[SerializeField] private AudioClip[] bgnClips;

	private ObjectPooler soundObjectPooler;

	private void Awake()
	{
		soundObjectPooler = this.gameObject.GetComponent<ObjectPooler>();
	}

	public void PlayBGM(BGM bgm, float volume = 0.8f)
	{
		AudioClip clip = bgnClips[(int)bgm];
		PlaySound(clip, volume);
	}

	public void PlaySE(SE se, float volume = 0.8f)
	{
		AudioClip clip = seClips[(int)se];
		PlaySound(clip, volume);
	}

	private void PlaySound(AudioClip clip, float volume = 0.8f)
	{
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
