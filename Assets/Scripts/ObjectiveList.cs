using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{

    [SerializeField] List<string> objectivesList;
    [SerializeField] private List<TextMeshProUGUI> textBoxs;
    // Start is called before the first frame update


    //We have one objective displayed at a time
    //Dynamicly add and remove objectives... 
    //How would we add the objectives in real time???


    //instead of adding new ones as we progress what if you set the whole list to begin with but only show the first 3
    //Then we simmply remove the first one from the list and update its text files to match the next 3

    //Maybe this will work.


    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveCompletedObjective()
    {
        if (objectivesList.Count != 0)
        {
            objectivesList.RemoveAt(0);
            Debug.Log(objectivesList.Count);
            UpdateText();
            
        }

    }

    private void UpdateText()
    {
        if(objectivesList.Count == 0) 
        {
            textBoxs[0].text = "You completed all your tasks";
            textBoxs[1].text = "";
            textBoxs[2].text = "";
        }

        if(objectivesList.Count >= textBoxs.Count)
        {
            for (int i = 0; i < textBoxs.Count; i++)
            {
                textBoxs[i].text = objectivesList[i];

            }
        }
        else
        {
            int difference = textBoxs.Count - objectivesList.Count;
            if(difference == 1)
            {
                for(int i = 0;i < textBoxs.Count -1;i++)
                {
                    textBoxs[i].text = objectivesList[i];
                }
                textBoxs[2].text = "";
            }
            if(difference == 2)
            {
                textBoxs[0].text = objectivesList[0];
                textBoxs[1].text = "";
                textBoxs[2].text = "";
            }
        }


    }

}
