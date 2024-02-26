using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdvancedInteractionsHandler : MonoBehaviour, Interactable
{
    [System.Serializable]
    public class Interaction
    {
        public UnityEvent interactEvent;
        public bool playOnce = false;
        public bool hasPlayed = false;
    }

    [SerializeField] protected List<Interaction> interactions = new();

	public void OnInteract()
    {
        foreach (var interaction in interactions)
        {
            if (interaction.hasPlayed && interaction.playOnce)
                continue;

            interaction.interactEvent?.Invoke();
            interaction.hasPlayed = true;
        }
    }
}
