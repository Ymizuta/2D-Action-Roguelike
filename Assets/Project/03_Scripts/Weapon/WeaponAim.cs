using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
	[SerializeField] GameObject reticlePrefab = null;

	private GameObject reticle;
	private Vector3 reticlePosition;

	private void Start()
	{
		Cursor.visible = false;

		this.reticle = Instantiate(reticlePrefab);
	}

	private void Update()
	{
		GetMousePosition();
		MoveReticle();
	}

	private void GetMousePosition()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 5f;

		var screenPos = Camera.main.ScreenToWorldPoint(mousePos);
		screenPos.z = transform.position.z;
		reticlePosition = screenPos;
	}

	private void MoveReticle()
	{
		reticle.transform.position = reticlePosition;
	}
}
