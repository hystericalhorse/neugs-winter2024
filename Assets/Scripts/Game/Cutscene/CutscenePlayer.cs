using UnityEngine;

public class CutscenePlayer : MonoBehaviour, Interactable
{
    [SerializeField] Cutscene cutscene;

	[ContextMenu("Plays Cutscene")]
    public void Play() => CutsceneManager.instance.StartCutscene(cutscene);

	public void OnInteract()
    {
        CutsceneManager.instance.StartCutscene(cutscene);
    }
}