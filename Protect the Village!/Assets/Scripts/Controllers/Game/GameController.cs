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
    public GameObject tutorialWelcomeCanvas;
    public GameObject generalUICanvas;
    public GameObject tutorialUICanvas;

    public GameObject tutorialTimeline;
    public GameObject attackTimeline;

    public Camera mainCamera;
    public Camera tutorialCamera;

    public static bool isPaused = false;
    public static bool isFinished = false;

    public float gameDuration = 180f;

    private float gameTimer;

    public int coinsAvailable = 0;//{ get; set; }
    public int healthPotionsAvailable { get;  set; }
    int staminaPotionsAvailable;

    public GameAmbientSoundController soundController;
    public AudioClip moneySpent;

    AllySpawner allySpawner;

    private static bool isFirstTimePlaying = true;
    public bool isInTutorial = false;
    private static bool isChoiceMade = false;
    public TutorialController tutorialController;

    public static GameController Instance;
    void Awake() {
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        endGameCanvas.SetActive(false);
        tutorialWelcomeCanvas.SetActive(false);
        tutorialTimeline.SetActive(false);
        attackTimeline.SetActive(false);
        isPaused = false;
        isFinished = false;
        Instance = this;
        gameTimer = 0;
        pauseScript = GetComponent<PauseScript>();
        tutorialController = GetComponent<TutorialController>();
        soundController = GetComponent<GameAmbientSoundController>();
        uiController = GetComponent<UiController>();
        allySpawner = GetComponent<AllySpawner>();
        uiController.UpdateCoinNumber(coinsAvailable);
        uiController.UpdateHealthPotionsNumber(healthPotionsAvailable);
        uiController.UpdateAllyNumber();
        tutorialCamera.GetComponent<AudioListener>().enabled = false;        

        if (isFirstTimePlaying) {
            Time.timeScale = 0;
            isPaused = true;
            isFirstTimePlaying = false;
            tutorialWelcomeCanvas.SetActive(true);
            generalUICanvas.SetActive(false);
        }
        else {
            Time.timeScale = 1;
            isPaused = false;
            isInTutorial = false;
            tutorialWelcomeCanvas.SetActive(false);
            generalUICanvas.SetActive(true);            
        }


    }

    void Start() {
        soundController = GetComponent<GameAmbientSoundController>();
        if(!isFirstTimePlaying && isChoiceMade == true)
            soundController.PlayGameMusic();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInTutorial && isChoiceMade) {
            pauseScript.PauseGame();
        }

        if (gameTimer >= gameDuration && !isFinished && !isInTutorial && isChoiceMade)
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

    public void EndTutorial() {
        tutorialUICanvas.SetActive(false);
        generalUICanvas.SetActive(true);
        isInTutorial = false;
        RestartGame();
    }

    public void SkipTutorial() {
        Time.timeScale = 1;
        isPaused = false;
        isInTutorial = false;
        tutorialWelcomeCanvas.SetActive(false);
        generalUICanvas.SetActive(true);
        soundController.PlayGameMusic();
        isChoiceMade = true;
    }

    public void PlayTutorial() {
        mainCamera.GetComponent<AudioListener>().enabled = false;
        tutorialCamera.GetComponent<AudioListener>().enabled = true;
        isInTutorial = true;
        isPaused = false;
        tutorialWelcomeCanvas.SetActive(false);
        Time.timeScale = 1;
        tutorialTimeline.SetActive(true);
        tutorialController.StartTutorial();
        soundController.PlayTutorialMusic();
        isChoiceMade = true;
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

    public void UpdateAllies() {
        uiController.UpdateAllyNumber();
    }

    public void UpdateBuildingNumber() {
        uiController.UpdateBuildingNumber();
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

    public void SetHealthPotions(int n) {
        healthPotionsAvailable = n;
    }
    public void SetCoins(int n) {
        coinsAvailable = n;
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

    public void SpawnAllies(int count) {
        allySpawner.SpawnAllies(count);
    }

    public void ShowTutorialUI() {
        tutorialUICanvas.SetActive(true);
    }


}
