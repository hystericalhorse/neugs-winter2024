using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour, Interactable
{
	[SerializeField] Dialogue dialogue;
	[SerializeField] Image imageCutscene;

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
