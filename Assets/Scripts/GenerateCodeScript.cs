using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCodeScript : MonoBehaviour, Interactable
{
    //Used to generate the code for the padlock puzzle, apply this to any object that needs to generate a code from 0 - 9999
    //Unoptimized :( currently needs to be attached to the object that genrates the code and needs that object to be refenced to the padlock/ whatever you need to enter the code into
    //Feel free to adjust as you see fit please leave comments on what you changed and how it now workd :D
    float code;
    bool codeGenerated = false;
   

    public void OnInteract()
    {
        GenrateCode();
        Debug.Log(code);
    }

    float GenrateCode()
    {
        //Simply generates the number when interacted on
        code = Random.Range(0, 9999);
        codeGenerated= true;
        return code;
    }

    public float ReturnCode()
    {
        //This is used mostly so the padlock/anything that really requires a code can recive the code
        //Is also the reason i need the two of them to be friends
        return code;
    }

    public bool CheckedAnswer()
    {
        return codeGenerated;
    }

}
