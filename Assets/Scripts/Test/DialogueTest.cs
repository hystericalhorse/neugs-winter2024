using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour, Interactable
{
	[SerializeField] Dialogue dialogue;
	[SerializeField] Image imageCutscene;
	[SerializeField] Image textBox;
    public TextMeshProUGUI text;

    private void Awake()
    {
        imageCutscene.enabled = false;
		textBox.enabled = false;
		text.enabled= false;
    }

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
