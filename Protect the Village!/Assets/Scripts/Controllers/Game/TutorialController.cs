using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject movementMessage;
    public GameObject zoomMessage;
    public GameObject rotationMessage;
    public GameObject cameraMessage;
    public GameObject focusMessage;
    public GameObject attackMessage;

    public GameObject healMessage;
    public GameObject healMessage2;
    public GameObject healMessage3;
    public GameObject healMessage4;

    public GameObject ShopMessage1;
    public GameObject shopMessage2;

    public GameObject endMessage;

    public GameObject attackTimeline;

    public Transform player;

    public Camera mainCamera;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;

    public GameObject tutorialComponents;
    public UiController uiController;


    bool isCharacterMoved = false;

    bool isZoomIn = false;
    bool isZoomOut = false;

    bool isQpressed = false;
    bool isEpressed = false;

    bool isWpressed = false;
    bool isApressed = false;
    bool isSpressed = false;
    bool isDpressed = false;

    bool isFocuspressed = false;

    bool isAttacked = false;
    bool isEnemiesFound = false;

    public bool isWaitingforG = false;
    bool isHealed = false;
    bool isOpenedShop = false;

    int cameraSwitchCount = 0;
    int enemiesDead = 0;

    public static TutorialController Instance;

    float taskTime = 0;

    void Awake() {
        Instance = this;
        tutorialState = Tutorial_State.CutScene;
        camera1.GetComponent<AudioListener>().enabled = false;
        camera2.GetComponent<AudioListener>().enabled = false;
        camera3.GetComponent<AudioListener>().enabled = false;
        uiController = tutorialComponents.GetComponent<UiController>();
    }

    public enum Tutorial_State { CutScene, Movement, Zoom, Rotation, Camera, Focus, Attack, Heal, OpenShop, CloseShop, EndTutorial}
    public Tutorial_State tutorialState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialState) {
            case Tutorial_State.Movement:
                MovementTutorial();
                break;
            case Tutorial_State.Zoom:
                ZoomTutorial();
                break;
            case Tutorial_State.Rotation:
                RotationTutorial();
                break;
            case Tutorial_State.Camera:
                CamMovementTutorial();
                break;
            case Tutorial_State.Focus:
                FocusTutorial();
                break;
            case Tutorial_State.Attack:
                AttackTutorial();
                break;
        }
        
    }

    public void StartTutorial() {

    }

    private void StartAttackTutorial() {
        attackTimeline.SetActive(true);
        mainCamera.GetComponent<AudioListener>().enabled = false;
        camera3.GetComponent<AudioListener>().enabled = true;
        //attackMessage.SetActive(true);
        //DebugAssist.Instance.MakeAllImmune();

    }

    public void EndCutscene() {
        Debug.Log(tutorialState.ToString());
        camera2.GetComponent<AudioListener>().enabled = false;
        mainCamera.GetComponent<AudioListener>().enabled = true;
        movementMessage.SetActive(true);
        tutorialState = Tutorial_State.Movement;
        Debug.Log(tutorialState.ToString());
    }

    public void SwitchCamera() {
        switch (cameraSwitchCount) {
            case 0:
                camera2.GetComponent<AudioListener>().enabled = true;
                camera1.GetComponent<AudioListener>().enabled = false;                
                cameraSwitchCount++;
                break;
            case 1:
                mainCamera.GetComponent<AudioListener>().enabled = true;
                camera3.GetComponent<AudioListener>().enabled = false;
                cameraSwitchCount++;
                break;


        }
        
    }

    private void MovementTutorial() {
        if (isCharacterMoved) {
            tutorialState = Tutorial_State.Zoom;
            movementMessage.SetActive(false);
            zoomMessage.SetActive(true);
        }
    }

    private void ZoomTutorial() {
        if (isZoomIn) {
            if(taskTime == 0)
                taskTime = Time.time;
            if (Time.time - taskTime > 2) { 
                tutorialState = Tutorial_State.Rotation;
                zoomMessage.SetActive(false);
                rotationMessage.SetActive(true);
                taskTime = 0;
            }
        }
    }

    private void RotationTutorial() {
        if (isQpressed && isEpressed) {
            if (taskTime == 0)
                taskTime = Time.time;
            if (Time.time - taskTime > 2) {
                tutorialState = Tutorial_State.Camera;
                rotationMessage.SetActive(false);
                cameraMessage.SetActive(true);
                taskTime = 0;
            }
        }

    }

    private void CamMovementTutorial() {
        if (isWpressed && isApressed && isSpressed && isDpressed) {
            if (taskTime == 0)
                taskTime = Time.time;
            if (Time.time - taskTime > 1) {
                tutorialState = Tutorial_State.Focus;
                cameraMessage.SetActive(false);
                focusMessage.SetActive(true);
                taskTime = 0;
            }
        }
    }

    private void FocusTutorial() {
        if (isFocuspressed) {
            if (taskTime == 0)
                taskTime = Time.time;
            if (Time.time - taskTime > 1) {
                tutorialState = Tutorial_State.Attack;
                focusMessage.SetActive(false);
                StartAttackTutorial();
                taskTime = 0;
            }
        }
    }

    private void AttackTutorial() {
        if(enemiesDead == 2)
            isAttacked = true;        

        if (isAttacked) {
            if (taskTime == 0)
                taskTime = Time.time;
            if (Time.time - taskTime > 1) {
                tutorialState = Tutorial_State.Heal;
                attackMessage.SetActive(false);
                healMessage.SetActive(true);
            }
        } 

    }

    public void MovementDone() {
        isCharacterMoved = true;        
    }

    public void ZoomDone() {
        isZoomIn = true;
    }

    public void RotationDone(KeyCode code) {
        switch (code) {
            case KeyCode.Q:
                isQpressed = true;
                break;
            case KeyCode.E:
                isEpressed = true;
                break;
        }
    }

    public void CameraMovementDone(KeyCode code) {
        switch (code) {
            case KeyCode.W:
                isWpressed = true;
                break;
            case KeyCode.A:
                isApressed = true;
                break;
            case KeyCode.S:
                isSpressed = true;
                break;
            case KeyCode.D:
                isDpressed = true;
                break;
        }
    }

    public void FocusDone() {
        isFocuspressed = true;
    }

    public void HealingDone() {
        isHealed = true;
        tutorialState = Tutorial_State.OpenShop;
        healMessage4.SetActive(false);
        ShopMessage1.SetActive(true);
    }

    public void OpenShopDone() {
        tutorialState = Tutorial_State.CloseShop;
        ShopMessage1.SetActive(false);
        shopMessage2.SetActive(true);
    }

    public void CloseShopDone() {
        tutorialState = Tutorial_State.EndTutorial;
        shopMessage2.SetActive(false);
        endMessage.SetActive(true);
    }

    public static bool DoIfTutorial(TutorialController.Tutorial_State tutorial_State) {
        //Debug.Log(TutorialController.Instance.tutorialState.ToString());
        //Debug.Log(tutorial_State.ToString());
        return (!GameController.Instance.isInTutorial || 
            (GameController.Instance.isInTutorial && TutorialController.Instance.tutorialState == tutorial_State) || 
            (GameController.Instance.isInTutorial && TutorialController.Instance.tutorialState == Tutorial_State.Attack));
    }

    public void SpawnEnemy() {
        GameController.Instance.GetComponent<EnemySpawner>().SpawnSingleEnemy();
    }

    public void HealDone(int n) {
        GameObject[] messages = { healMessage, healMessage2, healMessage3, healMessage4};

        messages[n-1].SetActive(false);
        messages[n].SetActive(true);

        switch (n) {
            case 1:
                GameController.Instance.SetHealthPotions(1);
                GameController.Instance.SetCoins(4);
                player.GetComponent<CharacterStats>().currentHealth = 70;
                uiController.UpdateCoinNumber(4);
                uiController.UpdateHealthPotionsNumber(1);
                uiController.UpdatePlayerHealth(70, 100);

                GameController.Instance.ShowTutorialUI();
                break;
            case 3:
                isWaitingforG = true;
                break;
        }
    }

    public void EndTutorial() {
        endMessage.SetActive(false);
        GameController.Instance.EndTutorial();
    }

    public void EnemyKilled() {
        enemiesDead++;
    }
}
