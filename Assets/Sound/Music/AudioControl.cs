using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour {

	public static float musicVolume = 0.2f;
	public static float soundVolume = 1.0f;
	public bool isMusic;
	public AudioClip musicFile;

	void Start()
	{
		StartBGMusic ();
	}

	private void StartBGMusic(){
		AudioSource audio = GetComponent<AudioSource>();
		if (isMusic) {
			audio.volume = musicVolume;
		} else {
			audio.volume = soundVolume;
		}
		audio.clip = musicFile;
		audio.Play();
	}
}