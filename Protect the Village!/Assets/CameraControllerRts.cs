using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRts : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform followTransform;
    private Transform currentTransform;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;


    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    protected Vector3 localRot;
    protected float cameraDist = 10f;

    public float MouseSensitivity = 40f;
    public float ScrollSensitvity = 2f;
    public float orbitSensitivity = 10f;
    public float scrollSensitivity = 6f;
    public bool pressed = false;

    public float MIN_X = 15;
    public float MAX_X = 115;
    public float MIN_Y = 20;
    public float MAX_Y = 100;
    public float MIN_Z = -10;
    public float MAX_Z = 90;


    public float MIN_CAM_Z = -10;
    public float MAX_CAM_Z = 90;

    public float MIN_CAM_Y = -10;
    public float MAX_CAM_Y = 90;

    float smooth = 0.01f; //0.01 - super smooth, 1 - super sharp 

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        currentTransform = followTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTransform != null) {
            currentTransform = followTransform;
            transform.position = followTransform.position;
            //transform.position = Vector3.Lerp(transform.position, followTransform.position + new Vector3(0, 2.0f, 0), smooth);
        }
        else { 
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            currentTransform = followTransform;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
             Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
            ) {
            currentTransform = null;
        }

        HandleZoomInput();
        HandleRotationInput();
    }

        void HandleZoomInput() {
        if (Input.mouseScrollDelta.y != 0) {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
       newZoom = new Vector3(
              newZoom.x,
              Mathf.Clamp(newZoom.y, MIN_CAM_Y, MAX_CAM_Y),
              Mathf.Clamp(newZoom.z, MIN_CAM_Z, MAX_CAM_Z));
              

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    void HandleRotationInput() {
        if (Input.GetKey(KeyCode.Q)) {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        if (Input.GetKey(KeyCode.E)) {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }


    void HandleMovementInput() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            newPosition += (transform.right * -movementSpeed);
        }

       
        newPosition = new Vector3(
              Mathf.Clamp(newPosition.x, MIN_X, MAX_X),
              Mathf.Clamp(newPosition.y, MIN_Y, MAX_Y),
              Mathf.Clamp(newPosition.z, MIN_Z, MAX_Z));

        /*if (Input.GetKey(KeyCode.R)) {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F)) {
            newZoom -= zoomAmount;
        }*/

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        
    }

}
