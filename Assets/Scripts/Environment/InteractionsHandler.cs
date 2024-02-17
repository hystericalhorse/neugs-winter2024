using UnityEngine;
using UnityEngine.Events;

public class InteractionsHandler : MonoBehaviour, Interactable
{
    [SerializeField] protected UnityEvent onInteract;
    public void OnInteract() => onInteract?.Invoke();
}
