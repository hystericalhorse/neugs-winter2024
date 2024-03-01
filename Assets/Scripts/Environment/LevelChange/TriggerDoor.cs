using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// Moves the player to a new level.
public class TriggerDoor : MonoBehaviour, Door
{
	public bool Locked = false;

    [SerializeField] Room thisRoom;
    [SerializeField] Room targetRoom;
    [Space]
    [SerializeField] Transform targetTransform;
	[SerializeField] bool transition = true;
	[Space]
	[SerializeField] UnityEvent lockedInteraction;

	public void Start()
	{
		var col = GetComponent<Collider2D>() ?? gameObject.AddComponent<BoxCollider2D>();
		col.isTrigger = !Locked;
	}

	public void OnInteract(bool withTransition = false)
    {
		if (!Locked)
		{
			Thru(transition);
		}
		else
		{
			lockedInteraction?.Invoke();
		}
		
	}

	public void Thru(bool withTransition = false)
	{
		if (withTransition)
		{
			var ts = FindAnyObjectByType<TransitionScreen>();

			ts.beforeFadeIn.AddListener(() => {
				PlayerManager.instance.GetPlayerController().DeactivateControls();
				PlayerManager.instance.GetCameraController().Pause();
				PlayerManager.instance.PlacePlayerController(this.targetTransform.transform.position);
			});

			ts.afterFadeIn.AddListener(() => {
				thisRoom?.OnExitRoom();
			});

			ts.beforeFadeOut.AddListener(() => {
				PlayerManager.instance.GetCameraController().Unpause();
				PlayerManager.instance.GetPlayerController().ActivateControls();
				targetRoom?.OnEnterRoom();
			});

			ts.Transition(0);
		}
		else
		{
			PlayerManager.instance.PlacePlayerController(this.targetTransform.transform.position);
			PlayerManager.instance.GetCameraController().NoLerpResetPosition();

			targetRoom?.OnEnterRoom();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<PlayerController>(out _))
			OnInteract();
	}

	public void Lock()
	{
		Locked = true;
		GetComponent<Collider2D>().isTrigger = false;


	}
	public void Unlock()
	{
		Locked = false;
		GetComponent<Collider2D>().isTrigger = true;
	}
}
