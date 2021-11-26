using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : MonoBehaviour
{
	[Header("Sprits")]
	[SerializeField] private Sprite damegedSprite = null;
	[SerializeField] private Sprite destroySprite = null;

	[Header("Settings")]
 	[SerializeField] private int damage = 1;
	[SerializeField] private bool isDamegable;

	private Health health;
	private SpriteRenderer spriteRenderer;
	private Collider2D collider2D;

    void Start()
    {
		health = this.gameObject.GetComponent<Health>();
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		collider2D = this.gameObject.GetComponent<Collider2D>();
		if (health)
		{
			health.Initalize();
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			TakeDamage();
		}
	}

	private void TakeDamage()
	{
		health.TakeDamage(damage);

		if (health.CurrentHealth.Value > 0)
		{
			if(isDamegable) this.spriteRenderer.sprite = damegedSprite;
		}

		if (health.CurrentHealth.Value <= 0)
		{
			if (destroySprite == null)
			{
				Destroy(this.gameObject);
			}else
			{
				this.spriteRenderer.sprite = destroySprite;
				collider2D.enabled = false;
			}
		}
	}
}
