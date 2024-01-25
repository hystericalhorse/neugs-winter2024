using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour, Interactable
{
	[SerializeField] DeprecatedDialogue dialogue;
	[SerializeField] Image imageCutscene;
	[SerializeField] Image textBox;
	[SerializeField] Image nameTextBox;
	[SerializeField] Image portrait;
    public TextMeshProUGUI text;
    public TextMeshProUGUI nameText;
    public Sprite backgroundSprite;

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
		if (backgroundSprite != null) imageCutscene.sprite = backgroundSprite;
		if (!DeprecatedDialogueManager.instance.isRunning)
		{
			DeprecatedDialogueManager.instance.AddDialogue(new DeprecatedDialogue[] { dialogue });
			DeprecatedDialogueManager.instance.PlayDialogue();
		}
			
		else
			DeprecatedDialogueManager.instance.NextLine();
	}
}
