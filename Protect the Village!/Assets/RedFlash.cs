using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlash : MonoBehaviour
{

    public GameObject panel;

    public float duration = 0.2f;

    float startTime = 0;
    bool showPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showPanel && Time.time - startTime >= duration) {
            showPanel = false;
            panel.SetActive(false);
        }
        
    }

    public void FlashScreen() {
        startTime = Time.time;
        showPanel = true;
        panel.SetActive(true);
    }


}
