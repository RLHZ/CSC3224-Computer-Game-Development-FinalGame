using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButtonHandler : MonoBehaviour
{

    void Start() {
        DeActivate();
    }

   // public void Activate() {
        //transform.gameObject.SetActive(true);
   // }

    public void DeActivate() {
        //transform.gameObject.SetActive(false);
    }

    public void RestartGame() {
        GameController.isFinished = true;
        GameController.RestartGame();
    }

    public void QuitGame() {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

}
