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
    List<TextMeshProUGUI> textBoxes = new();
    [SerializeField] private TextMeshProUGUI textBox;
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
        UpdateText();
    }

    //public void RemoveCompletedObjective()
    //{
    //    if (objectivesList.Count != 0)
    //    {
    //        objectivesList.RemoveAt(0);
    //        Debug.Log(objectivesList.Count);
    //        UpdateText();
    //    }
    //}

    private void UpdateText()
    {
		gameObject.SetActive(objectives.Count() > 0);

        textBox.text = string.Empty;
        foreach (var objective in objectives)
        {
            textBox.text += "\n" + objective.Item2;
		}

        //if(objectivesList.Count == 0) 
        //{
        //    textBoxs[0].text = "You completed all your tasks";
        //    textBoxs[1].text = "";
        //    textBoxs[2].text = "";
        //}
        //
        //if(objectivesList.Count >= textBoxs.Count)
        //{
        //    for (int i = 0; i < textBoxs.Count; i++)
        //    {
        //        textBoxs[i].text = objectivesList[i];
        //    }
        //}
        //else
        //{
        //    int difference = textBoxs.Count - objectivesList.Count;
        //    if(difference == 1)
        //    {
        //        for(int i = 0;i < textBoxs.Count -1;i++)
        //        {
        //            textBoxs[i].text = objectivesList[i];
        //        }
        //        textBoxs[2].text = "";
        //    }
        //    if(difference == 2)
        //    {
        //        textBoxs[0].text = objectivesList[0];
        //        textBoxs[1].text = "";
        //        textBoxs[2].text = "";
        //    }
        //}
    }

    private IEnumerator CrossOutText(string name)
    {
        int index = 0;
        foreach (var objective in objectives)
        {
			if (objective.Item1 == name)
			{
                int indexB = index + objective.Item2.Length;

                string textB = textBox.text.Substring(0, index);
				string text = textBox.text.Substring(index,objective.Item2.Length);
                string textA = textBox.text.Substring(indexB, textBox.text.Length - (textB.Length + text.Length));

				for (var i = index; i < text.Length; i++)
				{
					textBox.text = textB + "<s>" + text.Substring(0, i) + "</s>" + text.Substring(i, text.Length - i) + textA;

					yield return new WaitForSeconds(0.1f);
				}

				objectives.Remove(objective);
                break;
			}

			index += objective.Item2.Length - 1;
		}	

        yield return new WaitForSeconds(0.5f);
		UpdateText();
    }

    public void AddObjective(string name, string description)
    {
        objectives.Add((name,description));

        UpdateText();
    }

    public void RemoveObjective(string name)
    {
        StartCoroutine(CrossOutText(name));
	}

    [ContextMenu("StrikeThat")]
    public void TestStrikeThrough()
    {
        RemoveObjective("explore");
    }

    //public void NewObjective(string description)
    //{
	//	objectivesList.Add(description);
    //    UpdateText();
	//}

    //public void RemoveObjective(int index)
    //{
	//	objectivesList.RemoveAt(index);
    //    UpdateText();
	//}
}
