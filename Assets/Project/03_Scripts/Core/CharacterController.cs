using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

	public Vector2 CurrentMovement { get; private set; }

	// Start is called before the first frame update
	void Start()
    {
		this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		MoveCharacter();
	}

	public void MoveCharacter()
	{
		Vector2 currentMovePosition = rigidbody2D.position + this.CurrentMovement * Time.fixedDeltaTime;
		rigidbody2D.MovePosition(currentMovePosition);
	}

	public void SetMovement(Vector2 newPosition)
	{
		this.CurrentMovement = newPosition;
	}
}
