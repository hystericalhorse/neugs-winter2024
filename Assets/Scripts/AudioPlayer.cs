using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] string SoundName;
    [SerializeField, Range(0,1)] float volume = 1;
    [SerializeField, Range(0,1)] float pitch = 1;

    public void PlaySound()
    {
        AudioManager.instance.PlaySound(SoundName, pitch, volume);
    }
}
