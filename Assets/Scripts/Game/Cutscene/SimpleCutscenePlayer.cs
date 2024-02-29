using UnityEngine;

public interface CutscenePlayer { }

public class SimpleCutscenePlayer : MonoBehaviour, Interactable, CutscenePlayer
{
    [SerializeField] protected Cutscene cutscene;

	[ContextMenu("Plays Cutscene")]
    public void Play() => CutsceneManager.instance.StartCutscene(cutscene);

	public virtual void OnInteract()
    {
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