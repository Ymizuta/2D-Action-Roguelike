using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	[SerializeField] private float attackDelay = 0.5f;

	private BoxCollider2D boxCollider;
	private bool isAttacking;
	private readonly int useMeleeWeapon = Animator.StringToHash("UseMeleeWeapon");

	private void Update()
	{
		Flip();
	}

	public override void Initialize(Character character)
	{
		base.Initialize(character);
		boxCollider = GetComponent<BoxCollider2D>();
	}

	public override void UseWeapon()
	{
		StartCoroutine(Attack());
	}

	private IEnumerator Attack()
	{
		if (isAttacking) yield break;

		Debug.Log("Attack");

		boxCollider.enabled = false;
		isAttacking = true;
		Animator.SetTrigger(useMeleeWeapon);

		yield return new WaitForSeconds(attackDelay);

		boxCollider.enabled = true;
		isAttacking = false;
	}

	private void Flip()
	{
		if (WeaponOwner.GetComponent<CharacterFlip>().IsFaceRight)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
	}
}
