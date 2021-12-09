using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
	[Header("State")]
	[SerializeField] private AIState currentState;
	[SerializeField] private AIState remainState;

	public Transform Target { get; set; }

	public CharacterMovement CharacterMovement { get; set; }

	public Path Path { get; set; }

	public Collider2D Collider2D { get; set; }

	private void Awake()
	{
		CharacterMovement = this.gameObject.GetComponent<CharacterMovement>();
		Path = this.gameObject.GetComponent<Path>();
		Collider2D = GetComponent<Collider2D>();
	}

	private void Update()
	{
		currentState.EvaluateState(this);
	}

	public void TransitionToState(AIState nextState)
	{
		if (nextState != remainState)
		{
			currentState = nextState;
		}
	}
}