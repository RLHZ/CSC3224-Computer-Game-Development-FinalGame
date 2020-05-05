using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveCharacterController : MonoBehaviour
{
    public GameObject floatingTextPrefab;
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

    public virtual void GetHit(int damage) {
        if(floatingTextPrefab != null) ShowFloatingText(damage);
    }

    public bool IsAlive() { return isAlive; }

    private void ShowFloatingText(int damage) {
        var prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        prefab.GetComponent<TextMesh>().text = (-damage).ToString();
    }
}
