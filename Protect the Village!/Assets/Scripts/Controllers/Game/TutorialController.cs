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

    public GameObject attackTimeline;

    public Transform player;

    public Camera mainCamera;
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;


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
    bool isHealed = false;
    bool isOpenedShop = false;

    int cameraSwitchCount = 0;

    public static TutorialController Instance;

    float taskTime = 0;

    void Awake() {
        Instance = this;
        tutorialState = Tutorial_State.CutScene;
        Debug.Log("Calling awake");
    }

    public enum Tutorial_State { CutScene, Movement, Zoom, Rotation, Camera, Focus, Attack, Heal, Shop}
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
                break;
            case Tutorial_State.Heal:
                break;
            case Tutorial_State.Shop:
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
        DebugAssist.Instance.MakeAllImmune();

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
                camera1.GetComponent<AudioListener>().enabled = false;
                camera2.GetComponent<AudioListener>().enabled = true;
                cameraSwitchCount++;
                break;
            case 1:
                camera3.GetComponent<AudioListener>().enabled = false;
                mainCamera.GetComponent<AudioListener>().enabled = true;
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
            }
        }
    }

    private void FocusTutorial() {
        if (isFocuspressed) {
            tutorialState = Tutorial_State.Attack;
            focusMessage.SetActive(false);
            StartAttackTutorial();
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

    public static bool DoIfTutorial(TutorialController.Tutorial_State tutorial_State) {
        //Debug.Log(TutorialController.Instance.tutorialState.ToString());
        //Debug.Log(tutorial_State.ToString());
        return (!GameController.Instance.isInTutorial || (GameController.Instance.isInTutorial && TutorialController.Instance.tutorialState == tutorial_State));
    }

    public void SpawnEnemy() {
        GameController.Instance.GetComponent<EnemySpawner>().SpawnSingleEnemy();
    }
}
