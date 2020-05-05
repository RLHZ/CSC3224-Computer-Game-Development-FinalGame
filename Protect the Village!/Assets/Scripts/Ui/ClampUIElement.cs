using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClampUIElement : MonoBehaviour
{

    public Canvas canvas;
    public Camera camera;

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = camera.WorldToScreenPoint(this.transform.position);
        canvas.transform.position = namePos;
    }
}
