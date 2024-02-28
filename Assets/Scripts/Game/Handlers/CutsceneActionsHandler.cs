using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Search;
using UnityEngine;

public class CutsceneActionsHandler : MonoBehaviour
{
	public Queue<Action> actions = new();
	public bool waiting = false;
	public float waitingTime = 0;

	public void Awake() => Clear();

	public void Clear() => actions.Clear();

	public void AddMove(Vector2 move)
	{
		actions.Enqueue(new ValueAction<Vector2>(move));
	}

	public void AddPause(float seconds = 1)
	{
		actions.Enqueue(new ValueAction<float>(seconds));
	}

	public void AddMoveTo(Transform desiredTransform)
	{
		actions.Enqueue(new ValueAction<Transform>(desiredTransform));
	}

	public void AddCameraReposition(Transform targetTransform)
	{
		actions.Enqueue(new RepositionCameraAction(targetTransform));
	}

	public void AddCameraReset()
	{
		actions.Enqueue(new ResetCameraAction());
	}
	public void AddInteract()
	{
		actions.Enqueue(new PlayerInteractionAction());
	}
	public void AddSetActive(GameObject go)
	{
		actions.Enqueue(new ActivateGOAction(go));
	}
	public void AddSetInactive(GameObject go)
	{
		actions.Enqueue(new DeactivateGOAction(go));
	}
	public void AddMonoAction(MonoAction action)
	{
		actions.Enqueue(action);
	}

	public enum Direction { up,down,left,right }
	public void AddFaceDirection(int direction)
	{
		switch ((Direction) direction)
		{
			default:
			case Direction.up:
				actions.Enqueue(new PlayerFaceDirectionAction(Vector2.up));
				break;
			case Direction.down:
				actions.Enqueue(new PlayerFaceDirectionAction(Vector2.down));
				break;
			case Direction.left:
				actions.Enqueue(new PlayerFaceDirectionAction(Vector2.left));
				break;
			case Direction.right:
				actions.Enqueue(new PlayerFaceDirectionAction(Vector2.right));
				break;
		}
	}

	[ContextMenu("Play")]
	public void Play() => StartCoroutine(PlayCoroutine());

	private IEnumerator PlayCoroutine()
    {
		waiting = false;
		waitingTime = 0;
		Queue<Action> tape = new();

		tape = new(actions); // Create a copy to preserve the original actions :)


		while (tape.Count > 0)
		{
			PlayerManager.instance.GetPlayerController().DeactivateControls();
			PlayerManager.instance.GetCameraController().Pause();
			yield return null;

			if (waiting)
			{
				waitingTime -= Time.deltaTime;
				if (waitingTime <= 0)
					StopWaiting();
				continue;
			}

			var action = tape.Dequeue();
			if (action is ValueAction<float>)
			{
				StartWaiting((action as ValueAction<float>).Value);
				continue;
			}

			if (action is PlayerFaceDirectionAction)
			{
				PlayerManager.instance.GetPlayerController().FaceDirection((action as PlayerFaceDirectionAction).Value);
				continue;
			}

			if (action is ValueAction<Vector2>)
			{
				StartWaiting();
				yield return null;
				PlayerManager.instance.MovePlayerController((action as ValueAction<Vector2>).Value, () => { StopWaiting(); });
				continue;
			}

			if (action is RepositionCameraAction)
			{
				StartWaiting();
				yield return null;
				PlayerManager.instance.MoveCameraController((action as RepositionCameraAction).Value, () => { StopWaiting(); });
				continue;
			}

			if (action is ValueAction<Transform>)
			{
				Vector2 a = PlayerManager.instance.GetPlayerController().transform.position;
				Vector2 b = (action as ValueAction<Transform>).Value.position;
				var ab = b - a;
				StartWaiting();
				yield return null;
				PlayerManager.instance.MovePlayerController(ab, () => { StopWaiting(); });
				continue;
			}

			if (action is ResetCameraAction)
			{
				PlayerManager.instance.GetCameraController().SoftResetController();
				continue;
			}

			if (action is PlayerInteractionAction)
			{
				PlayerManager.instance.GetPlayerController().TryInteract();
				continue;
			}

			if (action is ActivateGOAction)
			{
				(action as ActivateGOAction).GO.SetActive(true);
				continue;
			}
			if (action is DeactivateGOAction)
			{
				(action as DeactivateGOAction).GO.SetActive(false);
				continue;
			}

			if (action is MonoAction)
			{
				(action as MonoAction).PerformAction();
				continue;
			}
		}

		yield return null;
		PlayerManager.instance.GetPlayerController().ActivateControls();
		PlayerManager.instance.GetCameraController().Unpause();
	}

	public void StartWaiting(float time = 900)
	{
		waitingTime = time;
		waiting = true;
	}

	public void StopWaiting()
	{
		waitingTime = 0;
		waiting = false;
	}
}

public interface Action { }
public class ValueAction<T> : Action
{
	public ValueAction(T value)
	{
		Value = value;
	}

	public T Value;
}

public class RepositionCameraAction : ValueAction<Transform>
{
	public RepositionCameraAction(Transform value) : base(value) { }
}

public class PlayerFaceDirectionAction : ValueAction<Vector2>
{
	public PlayerFaceDirectionAction(Vector2 value) : base(value) { }
}
public class ResetCameraAction : Action { }
public class PlayerInteractionAction : Action { }

public class ActivateGOAction : Action
{
	public ActivateGOAction(GameObject go) { GO = go; }

	public GameObject GO;
}

public class DeactivateGOAction : Action
{
	public DeactivateGOAction(GameObject go) { GO = go; }

	public GameObject GO;
}

public abstract class MonoAction : MonoBehaviour, Action
{
	public virtual void PerformAction() { }
}