using UnityEngine;

public class MonsterWall : MonoAction
{
	[SerializeField] ParticleSystem particleSys;
	[SerializeField] TriggerDoor wallDoor;
	[SerializeField] int keysGrabbed = 0;
	[SerializeField] int keysRequired = 1;

	public void Start()
	{
		if (wallDoor == null)
			wallDoor = transform.GetComponentInParent<TriggerDoor>() ?? null;

		wallDoor?.Lock();
		particleSys.Play();
	}

	public void UpdateWall()
	{
		keysGrabbed += 1;
		if (keysGrabbed >= keysRequired)
		{
			wallDoor.Unlock();
			particleSys.Stop();
		}
	}

	public override void PerformAction()
	{
		particleSys.Stop();
		base.PerformAction();
	}
}
