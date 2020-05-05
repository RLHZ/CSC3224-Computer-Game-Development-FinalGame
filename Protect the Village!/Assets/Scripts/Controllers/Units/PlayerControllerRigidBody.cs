using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PotionConsumptionController), typeof(PlayerMotor))]
public class PlayerControllerRigidBody : GoodCharacterController {

    private string forwardMoveInputAxis = "Vertical";
    private string sidewaysMoveInputAxis = "Horizontal";

    public float rotationRate = 180;

    public float moveSpeed = 25;
    public float initialSpeed;

    private Rigidbody rb;


    private Vector3 worldpos;
    private float mouseX;
    private float mouseY;
    private float cameraDif;

    public Camera cam;

    private CharacterCombat characterCombat;
    CharacterStats stats;
    public AudioClip characterHit;

    public LayerMask movementMask;
    

    protected override void Awake() {
        base.Awake();
        GameSettings.GoodGuys.Add(transform);
        rb = GetComponent<Rigidbody>();
        cameraDif = cam.transform.position.y - rb.transform.position.y;
        initialSpeed = moveSpeed;
        characterCombat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        GameController.Instance.uiController.UpdatePlayerHealth(stats.maxHealth, stats.maxHealth);
        DontDestroyOnLoad(this.gameObject);
        motor = GetComponent<PlayerMotor>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (!GameController.isPaused && !GameController.isFinished) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    motor.MoveToPoint(hit.point);
                    isEnemyTarget = false;
                    RemoveFocus();
                    target = null;
                }
            }
            if (Input.GetMouseButtonDown(1)) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                    if (enemy != null) {
                        SetFocus(enemy);
                        isEnemyTarget = true;
                        target = enemy.transform;
                    }
                }
            }

            if (isEnemyTarget && focus != null && focus.IsAlive() && agent.remainingDistance <= agent.stoppingDistance) {
                AttackTarget();
            }
        }
        else if (GameController.isFinished) {
            agent.isStopped = true;
        }
    }


    void FaceTarget(Transform target) {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!GameController.isPaused && !GameController.isFinished) { 
            float forwardMoveAxis = Input.GetAxis(forwardMoveInputAxis);
            float sidewaysMoveAxis = Input.GetAxis(sidewaysMoveInputAxis);

           // moveSpeed = (true || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? initialSpeed * 2 : initialSpeed;

            anim.SetBool("IsRunning", agent.remainingDistance > agent.stoppingDistance);
        }
        if (target != null)
            FaceTarget(target);
    }

    private void ProcessMovement() {
        
    }


    private void Move(float forwardInput, float sidewaysInput) {
        rb.AddForce(transform.forward * forwardInput * moveSpeed, ForceMode.Force);
        rb.AddForce(transform.right * sidewaysInput * moveSpeed, ForceMode.Force);
    }


    void Turn() {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        worldpos = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));
        Vector3 lookDirection = new Vector3(worldpos.x, rb.transform.position.y, worldpos.z);

        rb.transform.LookAt(lookDirection);
    }


    public override void Die() {
        if (isAlive) {
            isAlive = false;
            base.Die();
            EnemyController.isPlayerDead = true;
            GameController.Instance.uiController.UpdatePlayerHealth(stats.currentHealth, stats.maxHealth);
            GameSettings.GoodGuys.Remove(transform);
        }
    }

    public override void GetHit(int damage) {
        base.GetHit(damage);
        audioSource.PlayOneShot(characterHit);
        GameController.Instance.uiController.UpdatePlayerHealth(stats.currentHealth, stats.maxHealth);
    }

}

