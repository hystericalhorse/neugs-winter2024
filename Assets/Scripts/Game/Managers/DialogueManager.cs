using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
	public Sprite[] Portraits;
	public string ActorName;
	public string[] Lines;
}

public class DialogueManager : MonoBehaviourSingleton<DialogueManager>
{
	public bool isRunning = false;
	public Image imageCutscene;
	public Image textBox;
	public Image nameTextBox;
	public Image portrait;
	public TextMeshProUGUI text;
	public TextMeshProUGUI nameText;
	void Awake() => Set(this);
    private void Start()
    {
		//StopDialogue();
    }
    void OnDestroy() => Release();

	private void Update()
	{
		
	}

	Queue<Dialogue> script = new();

	public Dialogue currentDialogue;
	public int line;
	public string display;

	public void AddDialogue(Dialogue[] dlogs)
	{
		foreach (var dialog in dlogs)
			script.Enqueue(dialog);
	}

	public void NextLine()
	{
		if (!isRunning) return;

        imageCutscene.enabled = true;
		textBox.enabled= true;
		nameTextBox.enabled= true;
		text.enabled= true;
        nameText.enabled = true;
        portrait.enabled = true;

        if (line >= currentDialogue.Lines.Length - 1)
		{
			if (!script.TryDequeue(out currentDialogue))
			{
				StopDialogue();
				return;
			}

			line = 0;
		}
		else
		{
			line++;
		}

		display = currentDialogue.Lines[line];
		text.text = display;
		nameText.text = currentDialogue.ActorName;
		if (currentDialogue.Portraits.Length > line) portrait.sprite = currentDialogue.Portraits[line];

        //Debug.Log(display);
    }

	public void PlayDialogue()
	{
        line = 0;
        isRunning = true;
		if (currentDialogue is null) currentDialogue = script.Peek();
		NextLine();
	}

	public void StopDialogue()
	{
		line = 0;
		imageCutscene.enabled = false;
		textBox.enabled = false;
		text.enabled = false;
		isRunning = false;

		nameTextBox.enabled = false;
		nameText.enabled = false;
		portrait.enabled = false;

		currentDialogue = null;
	}
}

