using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AttackingCharacterController {

    public float buildingStoppingDistance = 4f;
    public float lookRadius = 10f;
    float destroyTime = 7f;
    public bool isAttackingBuilding;
    bool notifiedWinning;

    public static bool isAllBuildingsDestroyed;
    public static bool isPlayerDead;

    bool isNewBuildingTarget = false;

    public Transform buildingTarget;
    Transform buildingTargetHitArea;
    Transform player;
    NavMeshAgent agent;

    public AudioClip skeletonHit;

    public List<GameObject> pickupObjects;
    
    protected override void Awake() {
        base.Awake();
        player = PlayerManager.instance.player.transform;

        AssignNewBuilding(GameSettings.Buildings);
        
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(buildingTargetHitArea.position);
        GameSettings.BadGuys.Add(transform);

        isAttackingBuilding = true;
        notifiedWinning = false;
        isAllBuildingsDestroyed = false;
        isPlayerDead = false;
    }

    // Update is called once per frame
    void Update() {

        //Enemy's Winning condition
        if (!isAllBuildingsDestroyed && !isPlayerDead && !GameController.isFinished) {
            float playerDistance = Vector3.Distance(player.position, transform.position);

            if (playerDistance <= lookRadius && !DebugAssist.Instance.isIgnorePlayer) {
                isAttackingBuilding = false;

                if (agent.destination != player.position) { 
                    agent.SetDestination(player.position);
                    isNewBuildingTarget = true;
                }
                if (playerDistance <= agent.stoppingDistance) {
                    FaceTarget(player);
                    AttackTarget();
                }
            }
            else {
                float buildingDistance = Vector3.Distance(buildingTargetHitArea.position, transform.position);

                if (isNewBuildingTarget/*agent.destination != buildingTargetHitArea.position*/) {
                    agent.SetDestination(buildingTargetHitArea.position);
                    isAttackingBuilding = true;
                    isNewBuildingTarget = false;
                }

                if (buildingDistance <= buildingStoppingDistance) {
                    FaceTarget(buildingTargetHitArea);
                    AttackTarget();
                }
            }

            anim.SetBool("IsWalk", agent.remainingDistance > agent.stoppingDistance);
        }
        else if (!notifiedWinning && !GameController.isFinished) { 
            agent.isStopped = true;
            anim.SetTrigger("Celebrate");
            anim.SetBool("IsWalk", false);

            GameController.EndGame();
            notifiedWinning = true;
        }
    }

    void FaceTarget(Transform target) {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }




    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public override void Die() {
        if (isAlive) {
            isAlive = false;
            GetComponent<CapsuleCollider>().enabled = false;
            base.Die();
            SpawnPickupObjects();
            GameSettings.BadGuys.Remove(transform);
            Destroy(gameObject, destroyTime);
        }
    }
 
    public void AssignNewBuilding (List<Transform> buildingsLeft) {
        if (buildingsLeft.Count == 0) {
            isAllBuildingsDestroyed = true;
        }
        else { 
            buildingTarget = buildingsLeft[Random.Range(0, buildingsLeft.Count)];
            buildingTargetHitArea = buildingTarget.Find("HitArea");
            isNewBuildingTarget = true;
        }
    }

    public override void GetHit() {
        audioSource.PlayOneShot(skeletonHit);
    }

    void SpawnPickupObjects() {
        float randomChoice = Random.Range(0, 100);
        GameObject objectToSpawn;

        if (randomChoice < 20) 
            objectToSpawn = pickupObjects[1];
        else
            objectToSpawn = pickupObjects[0];

        Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}
