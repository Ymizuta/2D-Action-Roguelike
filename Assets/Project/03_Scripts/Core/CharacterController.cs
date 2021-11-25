using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CharacterController : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

	public Vector2 CurrentMovement { get; private set; }
	public bool IsNomalMove { get; set; }

	private Vector2 recoileMovement;

	public void Initialize()
	{
		this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
		this.IsNomalMove = true;

		Health health = this.gameObject.GetComponent<Health>();
		this.gameObject.FixedUpdateAsObservable()
			.Where(_ => health.CurrentHealth.Value > 0)
			.Subscribe(_ =>
			{
				if (IsNomalMove)
				{
					MoveCharacter();
				}
				Recoile();
			}).AddTo(this);
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
