using System.Collections;
using UnityEngine;

// Moves the player to a new level.
public class TriggerDoor : MonoBehaviour
{
    [SerializeField] Room thisRoom;
    [SerializeField] Room targetRoom;
    [Space]
    [SerializeField] Transform targetTransform;
	[SerializeField] bool transition = true;

    public void OnTeleport(bool withTransition = false)
    {
		if (!withTransition)
		{
			if (Vector2.Distance(PlayerManager.instance.GetPlayerController().transform.position, transform.position) > 0.3f)
			{
				thisRoom.OnExitRoom();

				PlayerManager.instance.PlacePlayerController(this.targetTransform.transform.position);
				PlayerManager.instance.GetCameraController().NoLerpResetPosition();

				targetRoom?.OnEnterRoom();
			}
		}
		else
		{
			if (Vector2.Distance(PlayerManager.instance.GetPlayerController().transform.position, transform.position) > 0.3f)
				StartCoroutine(TransitionTeleport());
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
        if (Vector2.Distance(PlayerManager.instance.GetPlayerController().transform.position, transform.position) > 0.3f)
        {
            thisRoom.OnExitRoom();

			PlayerManager.instance.PlacePlayerController(targetTransform.transform.position);
            PlayerManager.instance.GetCameraController().NoLerpResetPosition();

			targetRoom?.OnEnterRoom();
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>())
			OnTeleport(transition);
	}
}
