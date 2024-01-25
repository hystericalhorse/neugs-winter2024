using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WhiteboardTest : MonoBehaviour, Interactable
{
	[SerializeField] DeprecatedDialogue dialogue;
	[SerializeField] Image imageCutscene;
	[SerializeField] Image textBox;
	[SerializeField] Image nameTextBox;
	[SerializeField] Image portrait;
    public TextMeshProUGUI text;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI combo;
    public Sprite backgroundSprite;

    private void Awake()
    {
        imageCutscene.enabled = false;
		textBox.enabled = false;
		text.enabled= false;
        nameTextBox.enabled = false;
        nameText.enabled = false;

		portrait.enabled = false;
		if (combo != null) combo.enabled = false;
    }

    public void OnInteract()
	{
        combo.enabled = true;
		var combination = FindAnyObjectByType<LockedBox>().GetComboVec3();

        combo.text = "              " + combination.y.ToString() + "\n " + combination.x.ToString() + "\n                        " + combination.z.ToString();
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
