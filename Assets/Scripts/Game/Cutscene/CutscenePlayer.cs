using UnityEngine;

public class CutscenePlayer : MonoBehaviour, Interactable
{
    [SerializeField] protected Cutscene cutscene;

	[ContextMenu("Plays Cutscene")]
    public void Play() => CutsceneManager.instance.StartCutscene(cutscene);

	public virtual void OnInteract()
    {
		CutsceneManager.instance.StartCutscene(cutscene);
    }
}