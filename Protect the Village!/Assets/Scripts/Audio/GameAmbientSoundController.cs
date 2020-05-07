using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAmbientSoundController : MonoBehaviour
{
    AudioSource[] audioSource;
    public AudioClip lostJingle;
    public AudioClip winJingle;

    void Awake() {
        audioSource = GetComponents<AudioSource>();
        audioSource[1].Play();
        audioSource[2].Play();
        //audioSource[3].Play();
    }

    public void PlayGameLost() {
        audioSource[0].PlayOneShot(lostJingle);
        audioSource[3].Stop();
    }

    public void PlayGameWon() {
        audioSource[0].PlayOneShot(winJingle);
        audioSource[3].Stop();
    }

    public void PlaySingleSound(AudioClip clip) {
        audioSource[0].PlayOneShot(clip);
    }

    public void PlayTutorialMusic() {
        audioSource[4].Play();
        audioSource[3].Stop();
    }

    public void PlayGameMusic() {
        audioSource[3].Play();
        audioSource[4].Stop();
    }

}
