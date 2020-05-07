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

    Transform goodGuysTarget;
    float lastRadiusCheck;

    public AudioClip skeletonHit;

    public List<GameObject> pickupObjects;

    bool isFocus = false;

    private List<GoodCharacterController> charactersAttackingThis = new List<GoodCharacterController>();
    
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
            if (Time.time - lastRadiusCheck > 1)
                goodGuysTarget = GetGoodGuysTarget();

            

            if (goodGuysTarget != null) {//playerDistance <= lookRadius && !DebugAssist.Instance.isIgnorePlayer) {
                isAttackingBuilding = false;
                float enemyDistance = Vector3.Distance(goodGuysTarget.position, transform.position);

                if (agent.destination != goodGuysTarget.position) { 
                    agent.SetDestination(goodGuysTarget.position);
                    isNewBuildingTarget = true;
                }
                if (enemyDistance <= agent.stoppingDistance) {
                    FaceTarget(goodGuysTarget);
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

    public void OnFocused(GoodCharacterController character) {
        charactersAttackingThis.Add(character);
    }

    public void OnDefocused(GoodCharacterController character) {
        charactersAttackingThis.Remove(character);
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public override void Die() {
        if (isAlive) {
            isAlive = false;
            foreach (GoodCharacterController character in charactersAttackingThis ) {
                character.EnemyTargetDied();
            }
            charactersAttackingThis.Clear();
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(agent);
            base.Die();
            SpawnPickupObjects();
            GameSettings.BadGuys.Remove(transform);
            Destroy(gameObject, destroyTime);

            if (GameController.Instance.isInTutorial)
                TutorialController.Instance.EnemyKilled();
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

    public override void GetHit(int damage) {
        base.GetHit(damage);
        audioSource.PlayOneShot(skeletonHit);
    }

    void SpawnPickupObjects() {
        float randomChoice = Random.Range(0, 100);
        GameObject objectToSpawn;

        if (randomChoice < 20) 
            objectToSpawn = pickupObjects[1];
        else
            objectToSpawn = pickupObjects[0];

        Instantiate(objectToSpawn, transform.position + new Vector3(1,0,0), transform.rotation);
    }

    Transform GetGoodGuysTarget() {
        Transform closestTransform = null;
        float closestDistance = 100000;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, lookRadius);

        foreach (Collider collider in hitColliders) {
            if (!collider.gameObject.tag.Equals("Good") && !collider.gameObject.tag.Equals("Player")) continue;
            Transform currentTransform = collider.transform;
            float distance = Vector3.Distance(currentTransform.position, transform.position);
            if (distance < closestDistance)
                closestTransform = currentTransform;
        }
        lastRadiusCheck = Time.time;
        return closestTransform;
    }
}
