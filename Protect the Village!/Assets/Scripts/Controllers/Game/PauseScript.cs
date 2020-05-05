using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    

    public void PauseGame() {
        if (GameController.isPaused) {
            Time.timeScale = 1;
            GameController.isPaused = false;
            GameController.Instance.HideMenu();
        }
        else {
            Time.timeScale = 0;
            GameController.isPaused = true;
            GameController.Instance.ShowMenu();
        }
    }

    
}
