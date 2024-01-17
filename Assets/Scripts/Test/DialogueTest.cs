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
	[SerializeField] Image nameTextBox;
	[SerializeField] Image portrait;
    public TextMeshProUGUI text;
    public TextMeshProUGUI nameText;

    private void Awake()
    {
        imageCutscene.enabled = false;
		textBox.enabled = false;
		text.enabled= false;
        nameTextBox.enabled = false;
        nameText.enabled = false;

		portrait.enabled = false;
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
