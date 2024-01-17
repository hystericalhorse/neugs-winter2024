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
	public string ActorName;
	public string[] Lines;
}

public class DialogueManager : MonoBehaviourSingleton<DialogueManager>
{
	public bool isRunning = false;
	public Image imageCutscene;
	public Image textBox;
	public TextMeshProUGUI text;
	void Awake() => Set(this);
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
		text.enabled= true;

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

		display = currentDialogue.ActorName + ": " + currentDialogue.Lines[line];
		text.text = display;
		Debug.Log(display);
	}

	public void PlayDialogue()
	{
		isRunning = true;
		if (currentDialogue is null) currentDialogue = script.Peek();
		NextLine();
	}

	public void StopDialogue()
	{
		imageCutscene.enabled = false;
		textBox.enabled = false;
		text.enabled= false;
		isRunning = false;

		currentDialogue = null;
	}
}

