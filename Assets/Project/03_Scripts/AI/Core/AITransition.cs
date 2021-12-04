using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class AITransition
{
	public AIDecision decision;
	public AIState TrueState;
	public AIState FalseState;
}
