using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
	[SerializeField] private Character.CharacterType damageType = Character.CharacterType.AI;
	[SerializeField] private int damageToApply = 1;

	private Health playerHealth;

	private void Start()
	{
		playerHealth = this.gameObject.GetComponent<Health>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet" 
			&& collision.gameObject.GetComponent<Projectile>().WeaponOwner.Type == damageType)
		{
			TakeDamage();
		}
	}

	private void TakeDamage()
	{
		playerHealth.TakeDamage(damageToApply);
	}
}
