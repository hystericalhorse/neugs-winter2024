using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SplitComboLockCode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GenerateCodeScript genCode;
    [SerializeField] private GameObject[] lockers;
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

		lockers[0].GetComponent<CutscenePlayer>()?.ClearShots();
		lockers[0].GetComponent<CutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo100s.ToString());

		lockers[1].GetComponent<CutscenePlayer>()?.ClearShots();
		lockers[1].GetComponent<CutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo10s.ToString());

		lockers[2].GetComponent<CutscenePlayer>()?.ClearShots();
		lockers[2].GetComponent<CutscenePlayer>()?.AddShot(shot);

		shot = new();
		shot.shotScript.Add(combo1s.ToString());

		lockers[3].GetComponent<CutscenePlayer>()?.ClearShots();
		lockers[3].GetComponent<CutscenePlayer>()?.AddShot(shot);
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
