using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CoinManager : Singleton<CoinManager>
{
	public ReactiveProperty<int> Coins { get; private set; }

	private readonly string COINS_KEY = "MyGame_MyCoins_DontCheat";

    private void Start()
    {
		Coins = new ReactiveProperty<int>();
		LoadCoin();
		Coins.Subscribe(x => 
		{
			UIManager.Instance.UpdateCoin(x);
		}).AddTo(this);
    }

	private void LoadCoin()
	{
		//Coins.Value = PlayerPrefs.GetInt(COINS_KEY);
	}

	public void AddCoin(int amount)
	{
		Coins.Value += amount;
		//PlayerPrefs.SetInt(COINS_KEY, Coins.Value);
	}

	public void RemoveCoin(int amount)
	{
		Coins.Value -= amount;
		//PlayerPrefs.SetInt(COINS_KEY, Coins.Value);
	}
}
