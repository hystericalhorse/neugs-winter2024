using UnityEngine;
using UnityEngine.Events;

public abstract class FlagSwitch : MonoBehaviour, Interactable
{
    [SerializeField] protected string flagName;
    [SerializeField] protected bool updateValue;
    [Space()]
    [SerializeField] protected UnityEvent onInteractSuccess;
    [SerializeField] protected UnityEvent onInteractFailure;

    public virtual void OnInteract()
    {
        if (LevelManager.instance.UpdateLevel(flagName, updateValue))
            onInteractSuccess?.Invoke();
        else
			onInteractFailure?.Invoke();
	}
}