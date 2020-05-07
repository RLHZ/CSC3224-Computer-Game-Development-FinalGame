using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyController : AttackingCharacterController {
    // Start is called before the first frame update
    float destroyTime = 7f;

    EnemyController enemyTarget;

    NavMeshAgent agent;

    public AudioClip allyHit;

    List<Transform> targets;
    float lastSuccessfulLockingTarget;
    float secondsRetarget = 2f;

    protected override void Awake() {
        base.Awake();
        AssignNewEnemyTarget();
        agent = GetComponent<NavMeshAgent>();

        if (enemyTarget != null)
            agent.SetDestination(enemyTarget.transform.position);

        GameSettings.GoodGuys.Add(transform);
        GameSettings.allies.Add(transform);
    }

        // Update is called once per frame
    void Update()
    {
        if (enemyTarget == null || (enemyTarget != null && !enemyTarget.IsAlive()) || Time.time - lastSuccessfulLockingTarget > secondsRetarget) {
            EnemyController temp = AssignNewEnemyTarget();
            if (temp != null && temp != enemyTarget)
                enemyTarget = temp;
        }
        else {
            float enemyDistance = Vector3.Distance(enemyTarget.transform.position, transform.position);
            if (agent.destination != enemyTarget.transform.position) {
                agent.SetDestination(enemyTarget.transform.position);
            }
            if (enemyDistance <= agent.stoppingDistance) {
                FaceTarget(enemyTarget.transform);
                AttackTarget();
            }
        }

        anim.SetBool("IsRunning", agent.remainingDistance > agent.stoppingDistance);
    }

    EnemyController AssignNewEnemyTarget() {
        targets = GameSettings.BadGuys;
        if (targets != null && targets.Count > 0) {
            if(targets.Count > 1)
                SortTargetsByDistance();
            lastSuccessfulLockingTarget = Time.time;
            return targets[0].GetComponent<EnemyController>();
        }
        return null;
    }

    private void SortTargetsByDistance() {
        targets.Sort(delegate (Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, transform.position).CompareTo(Vector3.Distance(t2.position, transform.position));
        });
    }

    void FaceTarget(Transform target) {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public override void GetHit(int damage) {
        base.GetHit(damage);
        audioSource.PlayOneShot(allyHit);
    }

    public override void Die() {
        if (isAlive) {
            isAlive = false;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(agent);
            base.Die();
            GameSettings.GoodGuys.Remove(transform);
            GameSettings.allies.Remove(transform);
            GameController.Instance.UpdateAllies();
            Destroy(gameObject, destroyTime);
        }
    }
}
