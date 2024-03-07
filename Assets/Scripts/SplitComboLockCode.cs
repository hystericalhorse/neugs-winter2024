using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SplitComboLockCode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GenerateCodeScript genCode;
    [SerializeField] private GameObject[] lockers;
    [SerializeField] Sprite[] sprites;

    private float combo;
    private int combo1000s;
    private int combo100s;
    private int combo10s;
    private int combo1s;

    void Start()
    {
		SplitCode();

		Shot shot = new Shot();
		shot.shotScript.Add(combo1000s.ToString());
        shot.shotImage = sprites[0];
		lockers[0].GetComponent<SimpleCutscenePlayer>()?.ClearShots();
        
		lockers[0].GetComponent<SimpleCutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo100s.ToString());
        shot.shotImage = sprites[1];

		lockers[1].GetComponent<SimpleCutscenePlayer>()?.ClearShots();
		lockers[1].GetComponent<SimpleCutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo10s.ToString());
        shot.shotImage = sprites[2];

		lockers[2].GetComponent<SimpleCutscenePlayer>()?.ClearShots();
		lockers[2].GetComponent<SimpleCutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo1s.ToString());

        shot.shotImage = sprites[3];
		lockers[3].GetComponent<SimpleCutscenePlayer>()?.ClearShots();
		lockers[3].GetComponent<SimpleCutscenePlayer>()?.AddShot(shot);
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
}
