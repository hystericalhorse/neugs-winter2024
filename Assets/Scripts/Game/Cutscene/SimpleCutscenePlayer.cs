using UnityEngine;

public interface CutscenePlayer { public void Play(); }

public class SimpleCutscenePlayer : MonoBehaviour, Interactable, CutscenePlayer
{
	public bool interactable = true;
	[SerializeField] protected Cutscene cutscene;

	[ContextMenu("Plays Cutscene")]
    public void Play() => CutsceneManager.instance.StartCutscene(cutscene);

	public virtual void OnInteract()
    {
        if (interactable)
		    CutsceneManager.instance.StartCutscene(cutscene);
    }

    public void AddShot(Shot shot = default)
    {
        cutscene.Add(shot);
    }

    public void ClearShots()
    {
		cutscene.Clear();
	}
}