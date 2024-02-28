using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWall : MonoAction
{
	[SerializeField] ParticleSystem particleSys;

	public override void PerformAction()
	{
		particleSys.Stop();
		base.PerformAction();
	}
}
