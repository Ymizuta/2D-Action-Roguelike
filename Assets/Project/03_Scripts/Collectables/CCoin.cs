using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCoin : Collectables
{
	[SerializeField] private int coinToAdd = 20;

	protected override void Pick()
	{
		AddCoin();
	}

	private void AddCoin()
	{
		CoinManager.Instance.AddCoin(coinToAdd);
	}

	protected override void PlayEffects()
	{
		SoundManager.Instance.PlaySE(SoundManager.SE.Pickup_Coin7, 0.6f);
	}
}
