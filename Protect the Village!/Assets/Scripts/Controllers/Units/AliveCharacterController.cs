using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCharacterController : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource audioSource;
    public AudioClip dieSound;
    protected bool isAlive;

    protected virtual void Awake() {
        isAlive = true;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Die() {
        anim.SetBool("IsDead", true);
        audioSource.PlayOneShot(dieSound);
        enabled = false;
    }

    public virtual void GetHit() {
    }
}
