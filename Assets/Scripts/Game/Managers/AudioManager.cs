using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

	public void PlaySound(string name)
	{
		foreach (var sound in sounds)
		{
			if (sound.name == name)
			{
				sound.source.Play();
				return;
			}
		}
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
