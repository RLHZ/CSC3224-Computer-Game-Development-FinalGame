using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UiController), typeof(PauseScript))]
public class GameController : MonoBehaviour
{
    public PauseScript pauseScript;
    public UiController uiController;
    public Text timerText;
    public Text endGameText;
    public GameObject endGameCanvas;
    public GameObject menuCanvas;

    public static bool isPaused = false;
    public static bool isFinished = false;

    public float gameDuration = 180f;

    private float gameTimer;

    public int coinsAvailable = 0;//{ get; set; }
    public int healthPotionsAvailable { get; private set; }
    int staminaPotionsAvailable;

    public GameAmbientSoundController soundController;
    public AudioClip moneySpent;

    public static GameController Instance;
    void Awake() {
        Time.timeScale = 1;
        endGameCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        isPaused = false;
        isFinished = false;
        Instance = this;
        gameTimer = 0;
        pauseScript = GetComponent<PauseScript>();
        soundController = GetComponent<GameAmbientSoundController>();
        uiController = GetComponent<UiController>();
        uiController.UpdateCoinNumber(coinsAvailable);
        uiController.UpdateHealthPotionsNumber(healthPotionsAvailable);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseScript.PauseGame();
        }

        if (gameTimer >= gameDuration && !isFinished)
            WinGame();

        if (!isPaused && !isFinished)
            UpdateTimer();   
    }

    public static void EndGame() {
        isFinished = true;
        Instance.endGameText.text = "You Lost.";
        Instance.soundController.PlayGameLost();
        Instance.endGameCanvas.SetActive(true);
    }

    public static void WinGame() {
        isFinished = true;
        Instance.endGameText.text = "You Won!";
        Instance.soundController.PlayGameWon();
        Instance.endGameCanvas.SetActive(true);
    }

    public static void RestartGame() {
        GameSettings.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isFinished = false;
        isPaused = false;
    }

    void UpdateTimer() {
        
        gameTimer += Time.deltaTime;

        float timeLeft = gameDuration - gameTimer;

        string minutes = gameTimer >= gameDuration ? "00" : Mathf.Floor(timeLeft / 60).ToString("00");
        string seconds = gameTimer >= gameDuration ? "00" : (timeLeft % 60).ToString("00");

        timerText.text = (string.Format("Protect the Village!\n{0}:{1}", minutes, seconds));
    }

    public void AddCoins(int coinsToAdd) {
        UpdateCoins(coinsToAdd);    
    }

    public void RemoveCoins(int coinsToRemove) {
        UpdateCoins(-coinsToRemove);
    }

    void UpdateCoins(int number) {
        coinsAvailable += number;
        uiController.UpdateCoinNumber(coinsAvailable);
    }

    public void AddPotion() {
        healthPotionsAvailable++;
        uiController.UpdateHealthPotionsNumber(healthPotionsAvailable);
    }

    public void RemovePotion() {
        if(healthPotionsAvailable > 0) { 
            healthPotionsAvailable--;
            uiController.UpdateHealthPotionsNumber(healthPotionsAvailable);
        }
    }

    public void ShowMenu() {
        menuCanvas.SetActive(true);
    }

    public void HideMenu() {
        menuCanvas.SetActive(false);
    }

    public void SpendCoins(int ammount) {
        if(ammount <= coinsAvailable) { 
            coinsAvailable -= ammount;
            soundController.PlaySingleSound(moneySpent);
            uiController.UpdateCoinNumber(coinsAvailable);
        }
    }

    public void AddHealthPotions(int ammount) {
        healthPotionsAvailable += ammount;
        uiController.UpdateHealthPotionsNumber(healthPotionsAvailable);
    }
}
