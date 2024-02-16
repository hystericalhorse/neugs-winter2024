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
	[SerializeField] string defaultTextSound;
	
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

		//PlayerManager.instance.TogglePlayerController(false);
		PlayerManager.instance.GetPlayerController().DeactivateControls();

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

		shotImage?.gameObject.RecursiveSetActive(false);
		leftCharacterImage?.gameObject.RecursiveSetActive(false);
		rightCharacterImage?.gameObject.RecursiveSetActive(false);
		dialogueTextbox?.RecursiveSetActive(false);
		narratorTextbox?.RecursiveSetActive(false);

		//PlayerManager.instance.TogglePlayerController(true);
		PlayerManager.instance.GetPlayerController().ActivateControls();

		controls.Player.Interact.performed -= Next;
		controls.Disable();
		controls = null;

		currentShot.onShotEnd?.Invoke();
	}

	public IEnumerator TryDisplayText(string line, float textSpeed = 0.05f)
	{
		if (dialogueTextbox == null)
			StopCoroutine(TryDisplayText(line, textSpeed));

		typing = true;
		(dialogueTextbox.GetComponent<TextMeshProUGUI>() ?? dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>()).text = "";
		currentLineIndex = 0;
		yield return new WaitForEndOfFrame();
		while (typing)
		{
			try
			{

				(dialogueTextbox.GetComponent<TextMeshProUGUI>() ?? dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>()).text =
					(line.Length > 0) ? line.Substring(0, currentLineIndex + 1) : line;

				if (!currentShot.silent)
					if (currentShot.textSounds.Length <= 0 || !AudioManager.instance.PlaySound(currentShot.textSounds.GetRandom()))
						AudioManager.instance.PlaySound(defaultTextSound);
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
		if (shotImage != null)
		{
			if (shot.shotImage != null)
			{
				shotImage.sprite = shot.shotImage;
				shotImage.gameObject.RecursiveSetActive(true);
			}
			else
			{
				shotImage.sprite = null;
				shotImage.gameObject.RecursiveSetActive(false);
			}
		}

		if (leftCharacterImage != null)
		{
			if (shot.leftCharacterImage != null)
			{
				leftCharacterImage.sprite = shot.leftCharacterImage;
				leftCharacterImage.gameObject.RecursiveSetActive(true);
			}
			else
			{
				leftCharacterImage.sprite = null;
				leftCharacterImage.gameObject.RecursiveSetActive(false);
			}
		}

		if (rightCharacterImage != null)
		{
			if (shot.rightCharacterImage != null)
			{
				rightCharacterImage.sprite = shot.rightCharacterImage;
				rightCharacterImage.gameObject.RecursiveSetActive(true);
			}
			else
			{
				rightCharacterImage.sprite = null;
				rightCharacterImage.gameObject.RecursiveSetActive(false);
			}
		}

		currentScriptIndex = 0;
		currentLine = currentShot.shotScript[currentScriptIndex];

		narratorTextbox.RecursiveSetActive(true);
		dialogueTextbox.RecursiveSetActive(true);

		(narratorTextbox.GetComponent<TextMeshProUGUI>() ?? narratorTextbox.GetComponentInChildren<TextMeshProUGUI>()).text
			= currentShot.narrator != string.Empty ? currentShot.narrator : "*";

		currentShot.onShotBegin?.Invoke();
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

	public void Next(InputAction.CallbackContext context) => OnNext();

	[ContextMenu("Next")]
	public void OnNext()
	{
		if (typing)
		{
			StopCoroutine(TryDisplayText(currentLine, currentTextSpeed));
			(dialogueTextbox.GetComponent<TextMeshProUGUI>() ?? dialogueTextbox.GetComponentInChildren<TextMeshProUGUI>()).text = currentLine;
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
			currentShot.onShotEnd?.Invoke();
			currentShot = currentCutscene.Dequeue();
			TryDisplayShot(currentShot);
		}
		else
		{
			isPlaying = false;
		}
	}
}