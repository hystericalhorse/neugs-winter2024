using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{

    //[SerializeField] List<string> objectivesList;
    [SerializeField] List<(string, string)> objectives;
    List<TMP_Text> textBoxes = new();
    [SerializeField] private TextMeshProUGUI textBox;
	[SerializeField] private TMP_Text titleBox;
    // Start is called before the first frame update


    //We have one objective displayed at a time
    //Dynamicly add and remove objectives... 
    //How would we add the objectives in real time???


    //instead of adding new ones as we progress what if you set the whole list to begin with but only show the first 3
    //Then we simmply remove the first one from the list and update its text files to match the next 3

    //Maybe this will work.


    void Awake()
    {
        objectives = new();
		gameObject.RecursiveSetActive(false);
    }

    private IEnumerator CrossOutText(string name)
    {
		for (int i = 0; i < objectives.Count; i++)
        {
			if (objectives[i].Item1 == name)
			{
				var textBox = textBoxes[i];
				var text = textBox.text;
                int indexB = text.Length;

				for (var index = 0; index < indexB; index++)
				{
					textBox.text = "<s>" + text.Substring(0, index) + "</s>" + text.Substring(index, text.Length - (index + 1));
					yield return new WaitForFixedUpdate();
				}

				objectives.Remove(objectives[i]);
				yield return new WaitForSeconds(1f);
				StartCoroutine(FadeOut(textBox));
                break;
			}
		}	

        yield return new WaitForSeconds(0.5f);
		UpdateObjectives();
    }

    private void UpdateObjectives()
    {
		if (!gameObject.activeSelf && objectives.Count() > 0)
        {
			gameObject.RecursiveSetActive(true);
			StartCoroutine(FadeIn());
            return;
		}
	}

    private IEnumerator FadeOut(TMP_Text textBox)
    {
		while (textBox.text.Length > 2)
		{
			textBox.text = textBox.text.Substring(1, textBox.text.Length - 1);
			yield return new WaitForFixedUpdate();
		}

		textBoxes.Remove(textBox);
		Destroy(textBox.gameObject);

		if (textBoxes.Count < 1)
			gameObject.RecursiveSetActive(false); //TODO: Animate Hide List
		else
			UpdateTextboxes();
	}

    private IEnumerator FadeIn()
    {
		yield return new WaitForFixedUpdate();
	}

	public void AddObjective(string name, string description)
    {
		gameObject.RecursiveSetActive(true);

		objectives.Add((name,description));
		var textbox = Instantiate(textBox.gameObject).GetComponent<TMP_Text>();
		textbox.rectTransform.SetParent(gameObject.GetComponent<RectTransform>());

		textbox.text = description;

		textBoxes.Add(textbox);

		UpdateTextboxes();

		UpdateObjectives();
    }

    public void RemoveObjective(string name)
    {
        StartCoroutine(CrossOutText(name));
	}

	private void UpdateTextboxes()
	{
		var tbRect = textBoxes[0].rectTransform.rect;
		tbRect.width = gameObject.GetComponent<RectTransform>().rect.width;
		textBoxes[0].rectTransform.localScale = Vector3.one;
		textBoxes[0].rectTransform.anchoredPosition = titleBox.rectTransform.anchoredPosition - (Vector2.up * titleBox.rectTransform.rect.height);
		for (int i = 1; i < textBoxes.Count; i++)
		{
			var tbRect_ = textBoxes[i].rectTransform.rect;
			textBoxes[i].rectTransform.localScale = Vector3.one;
			textBoxes[i].rectTransform.anchoredPosition = textBoxes[i - 1].rectTransform.anchoredPosition - (Vector2.up * textBoxes[i - 1].rectTransform.rect.height);
		}
	}
}
