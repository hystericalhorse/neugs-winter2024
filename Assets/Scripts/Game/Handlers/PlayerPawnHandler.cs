using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawnHandler : MonoBehaviour
{
	public List<Action> actions = new();
	public bool waiting;

	public void AddMove(Vector2 move)
	{
		actions.Add(new ValueAction<Vector2>(move));
	}

	public void AddPause(float seconds = 1)
	{
		actions.Add(new ValueAction<float>(seconds));
	}

	public void AddMoveTo(Transform desiredTransform)
	{
		actions.Add(new ValueAction<Transform>(desiredTransform));
	}

	[ContextMenu("Play")]
	public void Play() => StartCoroutine(PlayCoroutine());

	private IEnumerator PlayCoroutine()
    {
		waiting = false;
        PlayerManager.instance.GetPlayerController().DeactivateControls();
		Queue<Action> tape = new();

		foreach (var act in actions)
			tape.Enqueue(act);

		float waitingTime = 0;

		while (tape.Count > 0)
		{
			if (waiting)
			{
				waitingTime += Time.deltaTime;
				if (waitingTime >= 5)
					waiting = false;
				continue;
			}

			var action = tape.Dequeue();
			if (action is ValueAction<float>)
			{
				yield return new WaitForSeconds((action as ValueAction<float>).Value);
				continue;
			}

			if (action is ValueAction<Vector2>)
			{
				waiting = true;
				PlayerManager.instance.MovePlayerController((action as ValueAction<Vector2>).Value, () => { waiting = false; });
				continue;
			}

			if (action is ValueAction<Transform>)
			{

				Vector2 a = PlayerManager.instance.GetPlayerController().transform.position;
				Vector2 b = (action as ValueAction<Transform>).Value.position;
				var ab = b - a;
				waiting = true;
				PlayerManager.instance.MovePlayerController(ab, () => { waiting = false; });
				continue;
			}
		}

        yield return null;

		PlayerManager.instance.GetPlayerController().ActivateControls();
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