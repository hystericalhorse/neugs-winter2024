using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour, Interactable
{
	[SerializeField] Dialogue dialogue;

	public void OnInteract()
	{
		if (!DialogueManager.instance.isRunning)
		{
			DialogueManager.instance.AddDialogue(new Dialogue[] { dialogue });
			DialogueManager.instance.PlayDialogue();
		}
			
		else
			DialogueManager.instance.NextLine();
	}
}
