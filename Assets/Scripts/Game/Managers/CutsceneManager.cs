using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;

public class CutsceneManager : MonoBehaviourSingleton<CutsceneManager>
{
	[SerializeField] Image shotImage;
	[SerializeField] Image leftCharacterImage;
	[SerializeField] Image rightCharacterImage;
	[SerializeField] GameObject narratorTextbox;
	[SerializeField] GameObject dialogueTextbox;
	
	bool isPlaying = false;
	bool typing = false;

	Queue<Shot> currentCutscene = new();

	Shot currentShot;
	int currentScriptIndex;

	string currentLine;

	int currentLineIndex;
	float currentTextSpeed;

	PlayerControls controls;

	private void Awake() => Set(this);
	private void OnDestroy() => Release();

	private IEnumerator PlayCutscene()
	{
		controls ??= new();
		controls.Player.Interact.performed += Next;
		controls.Enable();

		PlayerManager.instance.TogglePlayerController(false);

		isPlaying = true;
		currentShot = currentCutscene.Dequeue();
		TryDisplayShot(currentShot);

		while (isPlaying)
		{
			yield return null;
		}

		if (shotImage) shotImage.sprite = null;
		if (leftCharacterImage) leftCharacterImage.sprite = null;
		if (rightCharacterImage) rightCharacterImage.sprite = null;

		shotImage?.gameObject.SetActive(false);
		leftCharacterImage?.gameObject.SetActive(false);
		rightCharacterImage?.gameObject.SetActive(false);
		dialogueTextbox?.SetActive(false);
		narratorTextbox?.SetActive(false);

		PlayerManager.instance.TogglePlayerController(true);

		controls.Player.Interact.performed -= Next;
		controls.Disable();
		controls = null;
	}

	public IEnumerator TryDisplayText(string line, float textSpeed = 0.05f)
	{
		if (dialogueTextbox == null)
			StopCoroutine(TryDisplayText(line, textSpeed));

		typing = true;
		dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>().text = "";
		currentLineIndex = 0;
		yield return new WaitForEndOfFrame();
		while (typing)
		{
			try
			{
				dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>().text = line.Substring(0, currentLineIndex + 1);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			yield return new WaitForSeconds(textSpeed);

			currentLineIndex++;
			if (currentLineIndex >= line.Length)
				typing = false;
		}
	}

	public void TryDisplayShot(Shot shot)
	{
		if (shotImage) shotImage.sprite = null;
		if (leftCharacterImage) leftCharacterImage.sprite = null;
		if (rightCharacterImage) rightCharacterImage.sprite = null;

		if (shot.shotImage != null && shotImage != null)
		{
			shotImage.sprite = shot.shotImage;
			shotImage.gameObject.SetActive(true);
		}


		if (shot.leftCharacterImage != null && leftCharacterImage != null)
		{
			leftCharacterImage.sprite = shot.leftCharacterImage;
			leftCharacterImage.gameObject.SetActive(true);
		}

		if (shot.rightCharacterImage != null && rightCharacterImage != null)
		{
			rightCharacterImage.sprite = shot.rightCharacterImage;
			rightCharacterImage.gameObject.SetActive(true);
		}

		currentScriptIndex = 0;
		currentLine = currentShot.shotScript[currentScriptIndex];

		narratorTextbox.SetActive(true);
		dialogueTextbox.SetActive(true);

		narratorTextbox.GetComponentInChildren<TextMeshProUGUI>().text
			= currentShot.narrator != string.Empty ? currentShot.narrator : "*";

		StartCoroutine(TryDisplayText(currentLine));
	}

	public void StartCutscene(Cutscene cutscene)
	{
		if (!isPlaying)
		{
			currentCutscene.Clear();
			foreach (Shot shot in cutscene.Get())
				currentCutscene.Enqueue(shot);
			StartCoroutine(PlayCutscene()); 
		}
		else
		{
			foreach (Shot shot in cutscene.Get())
				currentCutscene.Enqueue(shot);
		}
	}

	public void Next(InputAction.CallbackContext context)
	{
		if (typing)
		{
			StopCoroutine(TryDisplayText(currentLine, currentTextSpeed));
			dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>().text = currentLine;
			typing = false;
		}
		else
		if (currentScriptIndex < (currentShot.shotScript.Count - 1))
		{
			currentScriptIndex++;
			currentLine = currentShot.shotScript[currentScriptIndex];
			StartCoroutine(TryDisplayText(currentLine));
		}
		else
		if (currentCutscene.Count > 0)
		{
			currentShot = currentCutscene.Dequeue();
			TryDisplayShot(currentShot);
		}
		else
		{
			isPlaying = false;
		}
	}

	[ContextMenu("Next")]
	public void OnNext()
	{
		if (typing)
		{
			StopCoroutine(TryDisplayText(currentLine, currentTextSpeed));
			dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>().text = currentLine;
			typing = false;
		}
		else
		if (currentScriptIndex < (currentShot.shotScript.Count - 1))
		{
			currentScriptIndex++;
			currentLine = currentShot.shotScript[currentScriptIndex];
			StartCoroutine(TryDisplayText(currentLine));
		}
		else
		if (currentCutscene.Count > 0)
		{
			currentShot = currentCutscene.Dequeue();
			TryDisplayShot(currentShot);
		}
		else
		{
			isPlaying = false;
		}
	}
}