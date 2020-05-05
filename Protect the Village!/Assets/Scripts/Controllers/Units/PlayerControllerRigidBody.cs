using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PotionConsumptionController))]
public class PlayerControllerRigidBody : AttackingCharacterController {

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

    public Camera camera;

    private CharacterCombat characterCombat;
    CharacterStats stats;
    public AudioClip characterHit;

    protected override void Awake() {
        base.Awake();
        GameSettings.GoodGuys.Add(transform);
        rb = GetComponent<Rigidbody>();
        cameraDif = camera.transform.position.y - rb.transform.position.y;
        initialSpeed = moveSpeed;
        characterCombat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        GameController.Instance.uiController.UpdatePlayerHealth(stats.maxHealth, stats.maxHealth);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!GameController.isPaused && !GameController.isFinished) { 
            float forwardMoveAxis = Input.GetAxis(forwardMoveInputAxis);
            float sidewaysMoveAxis = Input.GetAxis(sidewaysMoveInputAxis);

            moveSpeed = (true || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? initialSpeed * 2 : initialSpeed;

            Move(forwardMoveAxis, sidewaysMoveAxis);
            Turn();
            SetAnimation();

            CombatInput();
        }
    }



    private void Move(float forwardInput, float sidewaysInput) {
        rb.AddForce(transform.forward * forwardInput * moveSpeed, ForceMode.Force);
        rb.AddForce(transform.right * sidewaysInput * moveSpeed, ForceMode.Force);
    }

    //private void Turn(float input) {
    //  transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    // }

    void Turn() {
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;

        worldpos = camera.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cameraDif));
        Vector3 lookDirection = new Vector3(worldpos.x, rb.transform.position.y, worldpos.z);

        rb.transform.LookAt(lookDirection);
    }

    void SetAnimation() {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            if(true || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { 
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsWalking", true);
            }
            else { 
               
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsWalking", true);

            }
        }
        else {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsWalking", false);
        }
    }

    void CombatInput() {
        if (Input.GetMouseButtonDown(0))
            AttackTarget();

        else if (Input.GetMouseButtonDown(1))
            anim.SetTrigger("Defending");
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

    public override void GetHit() {
        audioSource.PlayOneShot(characterHit);
        GameController.Instance.uiController.UpdatePlayerHealth(stats.currentHealth, stats.maxHealth);
    }

}

