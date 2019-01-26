using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour {

    public enum SoundTypes { Shot, Clue, Scope};

    public AudioSource audioSource;
    public AudioClip ShotSound;
    public AudioClip ClueSound;
    public AudioClip ScopeSound;
    public AudioClip MusicSound;

    public static SoundMaster Instance;

    // Use this for initialization
    void Start () {

        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void PlaySingleSound(SoundTypes soundType) {
        switch (soundType) {
            case SoundTypes.Shot:
                audioSource.PlayOneShot(ShotSound);
                break;
            case SoundTypes.Clue:
                audioSource.PlayOneShot(ClueSound);
                break;
            case SoundTypes.Scope:
                audioSource.PlayOneShot(ScopeSound);
                break;

        }
    }
}
