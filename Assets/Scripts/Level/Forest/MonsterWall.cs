using System.Collections.Generic;
using UnityEngine;

public class MonsterWall : MonoAction
{
	[SerializeField] ParticleSystem particleSys;
	[SerializeField] TriggerDoor wallDoor;
	[SerializeField] int keysGrabbed = 0;
	[SerializeField] int keysRequired = 1;
	[SerializeField] SpriteRenderer[] stars;

	[SerializeField] ObjectiveHandler handler;

	public void Start()
	{
		if (wallDoor == null)
			wallDoor = transform.GetComponentInParent<TriggerDoor>() ?? null;

		wallDoor?.Lock();
		particleSys.Play();

		stars = GetComponentsInChildren<SpriteRenderer>();

		if (handler == null)
			handler = gameObject.GetComponent<ObjectiveHandler>() ?? gameObject.AddComponent<ObjectiveHandler>();

		handler.SetObjectiveName($"findkeys_{name}");
		handler.SetObjectiveDesc($"Find Photos ({keysGrabbed}/{keysRequired})");
	}

	public void UpdateWall()
	{
		keysGrabbed += 1;
		handler.SetObjectiveDesc($"Find Photos ({keysGrabbed}/{keysRequired})");
		if (stars.Length == keysRequired)
			stars[keysGrabbed-1].gameObject.SetActive( false );
		handler.UpdateObjective();

		if (keysGrabbed >= keysRequired)
		{
			wallDoor.Unlock();
			particleSys.Stop();
			handler.ResolveObjective();
		}
	}

	public override void PerformAction()
	{
		particleSys.Stop();
		base.PerformAction();
	}
}
