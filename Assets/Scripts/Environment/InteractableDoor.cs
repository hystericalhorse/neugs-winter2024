using System.Collections;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, Interactable, Door
{
    public bool Locked = false;

	[SerializeField] Room thisRoom;
	[SerializeField] Room targetRoom;
	[Space]
    [SerializeField] Transform targetTransform;
	[SerializeField] bool transition = true;

	public void OnInteract()
    {
        if (!Locked)
        {
			if (!transition)
			{
				thisRoom.OnExitRoom();

				PlayerManager.instance.PlacePlayerController(this.targetTransform.transform.position);
				PlayerManager.instance.GetCameraController().NoLerpResetPosition();

				targetRoom?.OnEnterRoom();
			}
			else
			{
				StartCoroutine(TransitionTeleport());
			}
		}
    }

	public IEnumerator TransitionTeleport()
	{
		thisRoom?.OnExitRoom();

		PlayerManager.instance.TogglePlayerController(false);
		StartCoroutine(FindAnyObjectByType<TransitionScreen>().FadeIn());
		yield return new WaitForSeconds(FindAnyObjectByType<TransitionScreen>().transitionTime);


		PlayerManager.instance.PlacePlayerController(this.targetTransform.transform.position);
		PlayerManager.instance.GetCameraController().NoLerpResetPosition();

		targetRoom?.OnEnterRoom();

		StartCoroutine(FindAnyObjectByType<TransitionScreen>().FadeOut());
		yield return new WaitForSeconds(FindAnyObjectByType<TransitionScreen>().transitionTime);

		PlayerManager.instance.TogglePlayerController(true);
	}

	public void OnTeleport(Transform targetTransform)
	{
		thisRoom.OnExitRoom();

		PlayerManager.instance.PlacePlayerController(targetTransform.transform.position);
		PlayerManager.instance.GetCameraController().NoLerpResetPosition();

		targetRoom?.OnEnterRoom();
	}

	public void Lock()
	{
		Locked = true;
	}
	public void Unlock()
	{
		Locked = false;
	}
}
