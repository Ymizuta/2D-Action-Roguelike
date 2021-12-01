using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private GameObject popupUi = null;
	[SerializeField] private GameObject storeUi = null;

	[Header("Items")]
	[SerializeField] private VendorItem itemHealth = null;
	[SerializeField] private VendorItem itemShield = null;
	[SerializeField] private VendorItem itemWeapon = null;

	private Character character;

	private void Awake()
	{
		popupUi.SetActive(false);
		storeUi.SetActive(false);
	}

	private void Update()
	{
		if (popupUi.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				popupUi.SetActive(false);
				storeUi.SetActive(true);
			}
		}

		BuyItem();
	}

	// todo ’¼‚µ‚½‚¢
	private void BuyItem()
	{
		if (storeUi.activeInHierarchy)
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				if (CoinManager.Instance.Coins.Value < itemHealth.Cost) return;
				(itemHealth.ItemCollectable as CHealth).AddHealth(character);
				ProductBought(itemHealth.Cost);
			}
			if (Input.GetKeyDown(KeyCode.N))
			{
				if (CoinManager.Instance.Coins.Value < itemShield.Cost) return;
				(itemShield.ItemCollectable as CShield).AddShield(character);
				ProductBought(itemShield.Cost);
			}
			if (Input.GetKeyDown(KeyCode.M))
			{
				if (CoinManager.Instance.Coins.Value < itemWeapon.Cost) return;
				character.GetComponent<CharacterWeapon>().GetNewWeapon(itemWeapon.Weapon);
				ProductBought(itemWeapon.Cost);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			character = collision.gameObject.GetComponent<Character>();
			popupUi.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			character = null;
			popupUi.SetActive(false);
			storeUi.SetActive(false);
		}
	}

	private void ProductBought(int amount)
	{
		storeUi.SetActive(false);
		CoinManager.Instance.RemoveCoin(amount);
	}
}
