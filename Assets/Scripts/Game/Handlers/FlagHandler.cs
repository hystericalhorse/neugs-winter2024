using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHandler : MonoBehaviour
{
   [SerializeField] string flag;
   [SerializeField] bool value;

    public void SetFlag() => LevelManager.instance.SetFlag(flag, value);
    public void SetFlag(bool value) => LevelManager.instance.SetFlag(flag, value);
    public void SetFlag(string flag) => LevelManager.instance.SetFlag(flag, value);

	public void TargetFlag(string flag) => this.flag = flag;

	public void TargetValue(bool value) =>  this.value = value;
}
