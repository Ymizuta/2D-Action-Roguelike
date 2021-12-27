using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StateController : MonoBehaviour
{
	[Header("State")]
	[SerializeField] private AIState currentState;
	[SerializeField] private AIState remainState;

	[Header("FOV")]
	[SerializeField] private Light2D fov;

	public Transform Target { get; set; }

	public Character Character { get; set; }
	public CharacterMovement CharacterMovement { get; set; }
	public CharacterWeapon CharacterWeapon { get; set; }
	public CharacterFlip CharacterFlip { get; set; }

	public Path Path { get; set; }

	public Collider2D Collider2D { get; set; }

	public Light2D FOV => fov;

	public Transform Player { get; set; }

	private void Awake()
	{
		Character = this.GetComponent<Character>();
		CharacterMovement = this.gameObject.GetComponent<CharacterMovement>();
		CharacterWeapon = this.gameObject.GetComponent<CharacterWeapon>();
		CharacterFlip = this.gameObject.GetComponent<CharacterFlip>();
		Path = this.gameObject.GetComponent<Path>();
		Collider2D = GetComponent<Collider2D>();
		Player = GameObject.Find("Player").transform;
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
