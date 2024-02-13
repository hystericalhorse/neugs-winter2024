using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SplitComboLockCode : CutscenePlayer
{
    // Start is called before the first frame update
    [SerializeField] private GenerateCodeScript genCode;
    [SerializeField] private CutscenePlayer[] lockers;
    private float combo;
    private int combo1000s;
    private int combo100s;
    private int combo10s;
    private int combo1s;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SplitCode()
    {
        //finally the math works
         combo = genCode.ReturnCode();
         combo1000s = (int)combo / 1000;
         int combo1000sRemainder = (int)combo % 1000;
        combo100s = combo1000sRemainder / 100;
        int combo100sRemainder = combo1000sRemainder % 100;
        combo10s = combo100sRemainder / 10;
         combo1s = combo100sRemainder % 10;
    }

    
    

    public override void OnInteract()
    {
        SplitCode();
        cutscene.Clear();

        Shot shot = new Shot();
        shot.shotScript.Add(combo1000s.ToString());

        //  shot.shotScript.Add(combination.x.ToString() + "-" + combination.y.ToString() + "-" + combination.z.ToString());

        lockers[0].AddSHot(shot);

        base.OnInteract();
    }
}
