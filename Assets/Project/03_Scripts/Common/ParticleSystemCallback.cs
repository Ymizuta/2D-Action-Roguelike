using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ParticleSystemCallback : MonoBehaviour
{
	private Subject<Unit> onStopParticle = new Subject<Unit>();

	public IObservable<Unit> OnStopParticleAsObservable => onStopParticle;

	private void OnParticleSystemStopped()
	{
		onStopParticle.OnNext(default);
	}
}
