using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

	public Vector2 CurrentMovement { get; private set; }
	public bool IsNomalMove { get; set; }

	private Vector2 recoileMovement;

	// Start is called before the first frame update
	void Start()
    {
		this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
		this.IsNomalMove = true;
	}

	private void FixedUpdate()
	{
		if (IsNomalMove)
		{
			MoveCharacter();
		}
		Recoile();
	}

	private void MoveCharacter()
	{
		Vector2 currentMovePosition = rigidbody2D.position + this.CurrentMovement * Time.fixedDeltaTime;
		rigidbody2D.MovePosition(currentMovePosition);
	}

	public void MoveCharacter(Vector2 newPosition)
	{
		rigidbody2D.MovePosition(newPosition);
	}

	public void SetMovement(Vector2 newPosition)
	{
		this.CurrentMovement = newPosition;
	}

	public void ApplyRecoile(Vector2 recoileMovement)
	{
		this.recoileMovement = recoileMovement;
	}

	private void Recoile()
	{
		rigidbody2D.AddForce(recoileMovement);
	}
}
