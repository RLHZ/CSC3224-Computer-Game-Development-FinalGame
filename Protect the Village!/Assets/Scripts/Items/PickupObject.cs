using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private float timeExistingTimer;
    public float limitTimeExisting = 10f;
    public float timeBlinking = 4f;
    MeshRenderer meshRenderer;

    public AudioClip objectPickupSound;

    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    void Update() {
        timeExistingTimer += Time.deltaTime;

        if (timeExistingTimer >= limitTimeExisting - timeBlinking) { 
            if(timeExistingTimer > limitTimeExisting)
                Destroy(gameObject);
            else
                Blink();
        }
    }

    public virtual void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            GameController.Instance.soundController.PlaySingleSound(objectPickupSound);
            Destroy(gameObject);
        }
    }

    void Blink() {
        if (Mathf.Repeat(timeExistingTimer, 1.0f) < 0.5){
            meshRenderer.enabled = false;
        }
        else {
            meshRenderer.enabled = true;
        }
    }
}
