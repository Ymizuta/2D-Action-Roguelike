using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class WeaponAim : MonoBehaviour
{
	[SerializeField] GameObject reticlePrefab = null;

	private GameObject reticle;
	private Vector3 reticlePosition;
	private Vector3 direction;

	private Vector3 currentAim = Vector3.zero;
	private Quaternion initRotation;
	private Quaternion lookRotation;
	private CharacterFlip characterFlip;

	public float CurrentAimAngle { get; set; }
	public Vector3 CurrentAimAbsolute { get; private set; }

	public void Initialize()
	{
		Cursor.visible = false;
		this.reticle = Instantiate(reticlePrefab);
		this.initRotation = transform.rotation;
		this.characterFlip = this.gameObject.GetComponent<Weapon>().WeaponOwner.GetComponent<CharacterFlip>();

		this.UpdateAsObservable()
			.Subscribe(_ => 
			{
				if (characterFlip.GetComponent<Character>().Type == Character.CharacterType.Player)
				{
					GetMousePosition();
				}else
				{
					EnemyAim();
				}
				MoveReticle();
				RotateWeapon();
			}).AddTo(this);
	}

	public void ShowReticle()
	{
		reticle.gameObject.SetActive(true);
	}

	public void HideReticle()
	{
		reticle.gameObject.SetActive(false);
	}

	private void GetMousePosition()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 5f;

		direction = Camera.main.ScreenToWorldPoint(mousePos);
		direction.z = transform.position.z;
		reticlePosition = direction;

		// Rotate
		currentAim = characterFlip.IsFaceRight ? direction - transform.position : transform.position - direction;
		CurrentAimAbsolute = direction - transform.position;
	}

	private void MoveReticle()
	{
		reticle.transform.position = reticlePosition;
	}

	public void RotateWeapon()
	{
		CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
		lookRotation = Quaternion.Euler((CurrentAimAngle * Vector3.forward));
		transform.rotation = lookRotation;
	}

	private void EnemyAim()
	{
		CurrentAimAbsolute = currentAim;
		currentAim = characterFlip.IsFaceRight ? currentAim : -currentAim;
		direction = currentAim - transform.position;
	}

	public void SetAim(Vector2 newAim)
	{
		currentAim = newAim;
	}
}
