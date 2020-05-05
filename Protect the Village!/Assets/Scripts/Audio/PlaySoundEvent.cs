using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEvent : MonoBehaviour
{
    public AudioClip footStep;

    public List<AudioClip> enemySpawn;

    AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootStep() {
        audioSource.PlayOneShot(footStep);
    }

    public void PlayEnemySpawn() {
        audioSource.PlayOneShot(enemySpawn[Random.Range(0, enemySpawn.Count)]);
    }
}
