using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
	public string name;

	[HideInInspector] public AudioSource source;
	public AudioClip clip;

	[Range(0, 1)] public float volume = 0.5f;
	[Range(0, 1)] public float pitch = 0.5f;

	public bool loop = false;
}

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
	void Awake() => Set(this);
	void OnDestroy() => Release();

	[SerializeField] AudioMixerGroup mixerGroup;
	[SerializeField] Sound[] sounds;
	[SerializeField] Sound[] music;
	int musicIndex = 0;

    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.playOnAwake = false;
            sound.source.outputAudioMixerGroup = mixerGroup;
        }

        foreach (var sound in music)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            sound.source.playOnAwake = false;
            sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public bool PlaySound(string name)
	{
		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				sound.source.Play();
				return true;
			}
		}

		return false;
	}

	public bool PlaySound(string name, float pitch)
	{
		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				sound.source.pitch = pitch;

				sound.source.Play();
				StartCoroutine(ResetSound(name, sound.source.clip.length));
				return true;
			}
		}

		return false;
	}

	public bool PlaySound(string name, float pitch, float volume)
	{
		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				sound.source.pitch = pitch;
				sound.source.volume = volume;

				sound.source.Play();
				StartCoroutine(ResetSound(name, sound.source.clip.length));
				return true;
			}
		}

		return false;
	}

	public IEnumerator ResetSound(string name, float inSeconds)
	{
		yield return new WaitForSeconds(inSeconds);

		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				sound.source.pitch = sound.pitch;
				sound.source.volume = sound.volume;
			}
		}
	}

	public void StopSound(string name)
	{
		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				sound.source.Stop();
				return;
			}
		}
	}

	public bool IsPlaying(string name)
	{
		foreach (var sound in sounds)
		{
			if (sound.name.ToLower() == name.ToLower())
			{
				return sound.source.isPlaying;
			}
		}

		return false;
	}

	Queue<Sound> pauseSounds = new();
	public void PauseSounds()
	{
		pauseSounds.Clear();
		foreach (var sound in sounds)
		{
			if (sound.source.isPlaying)
			{
				sound.source.Pause();
				pauseSounds.Enqueue(sound);
			}
		}

		foreach (var sound in music)
		{
			if (sound.source.isPlaying)
			{
				sound.source.Pause();
				pauseSounds.Enqueue(sound);
			}
		}
	}

	public void UnPauseSounds()
	{
		while (pauseSounds.Count > 0)
		{
			var sound = pauseSounds.Dequeue();
			sound.source.UnPause();
		}
	}

	public void StopSounds()
	{
		foreach (var sound in sounds)
		{
			sound.source.Stop();
		}

		foreach (var sound in music)
		{
			sound.source.Stop();
		}
	}
}
