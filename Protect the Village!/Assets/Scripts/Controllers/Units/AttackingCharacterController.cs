using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingCharacterController : AliveCharacterController {

    float attackStartTime;
    public float attackWaitTime = 1;
    public float attackDistance = 1.5f;
    public float buildingAttackDistance = 10f;
    // Start is called before the first frame update
    public List<AudioClip> attackSound;


    protected override void Awake() {
        base.Awake();
        attackStartTime = Time.time;
    }


    protected void AttackTarget() {
        if (Time.time - attackStartTime > attackWaitTime) {
            anim.SetTrigger("Attacking");
            attackStartTime = Time.time;
        }
    }

    public void SwordSwing() {
        audioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Count)]);
    }

    public override void Die() {
        base.Die();       
        Destroy(transform.Find("Cube_Healthbar").gameObject);
    }

    public override void GetHit(int damage) {
        base.GetHit(damage);
    }
}