using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
	[SerializeField] GameObject reticlePrefab = null;

	private GameObject reticle;
	private Vector3 reticlePosition;

	private Vector3 currentAim = Vector3.zero;
	private Quaternion initRotation;
	private Quaternion lookRotation;
	private CharacterFlip characterFlip;

	public float CurrentAimAngle { get; set; }
	public Vector3 CurrentAimAbsolute { get; private set; }

	private void Start()
	{
		Cursor.visible = false;

		this.reticle = Instantiate(reticlePrefab);
		this.initRotation = transform.rotation;
		this.characterFlip = this.gameObject.GetComponent<Weapon>().WeaponOwner.GetComponent<CharacterFlip>();
	}

	private void Update()
	{
		GetMousePosition();
		MoveReticle();
		RotateWeapon();
	}

	private void GetMousePosition()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 5f;

		var direction = Camera.main.ScreenToWorldPoint(mousePos);
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

	private void RotateWeapon()
	{
		CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
		lookRotation = Quaternion.Euler((CurrentAimAngle * Vector3.forward));
		transform.rotation = lookRotation;
	}
}