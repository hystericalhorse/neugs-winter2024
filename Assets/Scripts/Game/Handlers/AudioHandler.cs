using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private float volume;
    private float pitch;

    public void PlaySound(string name) => AudioManager.instance.PlaySound(name);

    public void StopSound(string name) => AudioManager.instance.StopSound(name);
}
