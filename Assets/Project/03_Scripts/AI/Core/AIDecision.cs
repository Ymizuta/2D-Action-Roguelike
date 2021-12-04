using UnityEngine;
using System.Collections;

public abstract class AIDecision : ScriptableObject
{
	public abstract bool Decide(StateController cotroller);
}
