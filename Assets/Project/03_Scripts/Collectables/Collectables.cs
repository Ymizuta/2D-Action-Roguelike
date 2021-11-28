using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
	[SerializeField] private bool isDestroyObject = true;

	private GameObject objectCollided;
	protected Character character;
	private SpriteRenderer spriteRenderer;
	private new Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		collider2D = this.gameObject.GetComponent<Collider2D>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		this.objectCollided = collision.gameObject;
		if (IsPickable())
		{
			Pick();
			PlayEffects();

			if (isDestroyObject)
			{
				Destroy(this.gameObject);
			}
			else
			{
				spriteRenderer.enabled = false;
				collider2D.enabled = false;
			}
		}
	}

	protected bool IsPickable()
	{
		this.character = objectCollided.GetComponent<Character>();
		if (character == null) return false;
		return character.Type == Character.CharacterType.Player;
	}

	protected virtual void Pick()
	{

	}

	protected virtual void PlayEffects()
	{

	}
}
