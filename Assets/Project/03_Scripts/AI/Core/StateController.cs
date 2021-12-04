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

	private void Awake()
	{
		CharacterMovement = this.gameObject.GetComponent<CharacterMovement>();
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
