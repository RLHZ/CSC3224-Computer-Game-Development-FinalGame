using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoodCharacterController : AttackingCharacterController {
    // Start is called before the first frame update

    protected PlayerMotor motor;
    protected NavMeshAgent agent;

    protected EnemyController focus;

    protected Transform target;

    protected bool isEnemyTarget = false;

    protected override void Awake() {
        base.Awake();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() {
       base.Die();            
    }

    public void EnemyTargetDied() {
        KillFocus();
        isEnemyTarget = false;
    }

    protected void KillFocus() {
        focus = null;
        motor.StopFollowingTarget();
    }


    protected void SetFocus(EnemyController newFocus) {
        focus = newFocus;
        motor.FollowTarget(newFocus);
        focus.OnFocused(this);
    }

    protected void RemoveFocus() {
        if (focus != null) {
            motor.StopFollowingTarget();
            focus.OnDefocused(this);
            focus = null;
        }
    }
}
