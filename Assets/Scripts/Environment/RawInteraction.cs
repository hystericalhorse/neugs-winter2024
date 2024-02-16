using UnityEngine;
using UnityEngine.Events;

public class RawInteraction : MonoBehaviour, Interactable
{
    [SerializeField] private UnityEvent onInteract;
    public void OnInteract() => onInteract?.Invoke();
}
