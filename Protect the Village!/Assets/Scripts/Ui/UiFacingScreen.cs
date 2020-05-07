using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFacingScreen : MonoBehaviour
{

    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        camera = Camera.main;
        if(camera != null)
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
                            camera.transform.rotation * Vector3.up);
    }
}
