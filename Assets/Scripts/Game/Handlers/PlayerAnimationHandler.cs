using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
	string targetName;

	public void SetTargetProperty(string name) => targetName = name;

	public void SetBool(bool b)
    {
        PlayerManager.instance.GetPlayerController().GetComponent<Animator>().SetBool(targetName, b);
    }

	public void SetFloat(float f)
	{
		PlayerManager.instance.GetPlayerController().GetComponent<Animator>().SetFloat(targetName, f);
	}
}