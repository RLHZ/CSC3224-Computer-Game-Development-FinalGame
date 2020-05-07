using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    bool isShopOpen;
    GameObject canvas;
    GameObject warning;

    public PlayerStats playerStats;

    public int maxArmourAvailable = 9;
    public int maxAttackAvailable = 23;
    public int maxAlliesAlive = 3;

    public int potionCost;
    public int armourCost;
    public int attackCost;
    public int allyCost;
    int totalCost;

    public Text costSummary;
    public Text potionCost_text;
    public Text armourCost_text;
    public Text attackCost_text;
    public Text allyCost_text;

    private int potionQty;
    private int armourQty;
    private int attackQty;
    private int allyQty;

    public Text potionQtyText;
    public Text armourQtyText;
    public Text attackQtyText;
    public Text allyQtyText;

    public Text armourLeftText;
    public Text attackLeftText;
    public Text allyMaxText;

    private int totalArmourBought;
    private int totalAttackBought;

    public enum BuyableItems { Potion, Armour, Attack, Ally}

    public static ShopController Instance;

    void Awake() {
        Instance = this;
        isShopOpen = false;
        ResetQty();
        canvas = gameObject.transform.Find("mediumBoard").gameObject;
        warning = gameObject.transform.Find("NotEnoughBoard").gameObject;
        canvas.SetActive(false);
        warning.SetActive(false);
        potionCost_text.text = potionCost.ToString();
        armourCost_text.text = armourCost.ToString();
        attackCost_text.text = attackCost.ToString();
        allyCost_text.text = allyCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (TutorialController.DoIfTutorial(TutorialController.Tutorial_State.OpenShop) && !isShopOpen && !GameController.isPaused && !GameController.isFinished) {
                OpenShopWindow();
                if (GameController.Instance.isInTutorial)
                    TutorialController.Instance.OpenShopDone();
            }
            else if (TutorialController.DoIfTutorial(TutorialController.Tutorial_State.CloseShop) && isShopOpen) {
                CloseShopWindow();                
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isShopOpen) {
            canvas.SetActive(false);
            warning.SetActive(false);
            isShopOpen = false;
        }   
    }

    private void ResetQty() {
        potionQty = 0;
        armourQty = 0;
        attackQty = 0;
        allyQty = 0;
        totalCost = 0;
        SetQuantitiesTexts();
    }

    private void OpenShopWindow() {
        Time.timeScale = 0;
        GameController.isPaused = true;
        ResetQty();
        CalculateTotal();     
        canvas.SetActive(true);
        isShopOpen = true;
    }

    public void CloseShopWindow() {
        if (GameController.Instance.isInTutorial)
            TutorialController.Instance.CloseShopDone();

        Time.timeScale = 1;
        GameController.isPaused = false;
        canvas.SetActive(false);
        warning.SetActive(false);
        isShopOpen = false;
    }

    public void IncreaseQuantity(BuyableItems item) {
        switch (item) {
            case BuyableItems.Potion:
                potionQty++;
                break;
            case BuyableItems.Armour:
                if(totalArmourBought + armourQty + 1 <= maxArmourAvailable)
                    armourQty++;
                break;
            case BuyableItems.Attack:
                if (totalAttackBought + attackQty + 1 <= maxAttackAvailable)
                    attackQty++;
                break;
            case BuyableItems.Ally:
                if (GetNumberAllies() + allyQty + 1 <= maxAlliesAlive)
                    allyQty++;
                break;
        }
        warning.SetActive(false);
        SetQuantitiesTexts();
        CalculateTotal();
    }

    public void DecreaseQuantity(BuyableItems item) {
        switch (item) {
            case BuyableItems.Potion:
                if(potionQty > 0) potionQty--;
                break;
            case BuyableItems.Armour:
                if (armourQty > 0) armourQty--;
                break;
            case BuyableItems.Attack:
                if (attackQty > 0) attackQty--;
                break;
            case BuyableItems.Ally:
                if (allyQty > 0) allyQty--;
                break;
        }
        warning.SetActive(false);
        SetQuantitiesTexts();
        CalculateTotal();
    }

    void SetQuantitiesTexts() {
        potionQtyText.text = potionQty.ToString();
        armourQtyText.text = armourQty.ToString();
        attackQtyText.text = attackQty.ToString();
        allyQtyText.text = allyQty.ToString();

        armourLeftText.text = "(" + (maxArmourAvailable - armourQty - totalArmourBought) + " Left)";
        attackLeftText.text = "(" + (maxAttackAvailable - attackQty - totalAttackBought) + " Left)";
        allyMaxText.text = "(Alive:  " + GetNumberAllies() + ", Max:  " + maxAlliesAlive + ")";
    }

    private void CalculateTotal() {
        int coinsAvailable = GameController.Instance.coinsAvailable;
        totalCost = potionQty * potionCost + attackQty * attackCost + armourQty * armourCost + allyQty * allyCost;
        int coinsLeft = coinsAvailable - totalCost;

        costSummary.text = string.Format("{0}\n{1}\n{2}", coinsAvailable, totalCost, coinsLeft);
    }

    public void Buy() {
        if (totalCost > GameController.Instance.coinsAvailable) {
            warning.SetActive(true);
        }
        else if (totalCost > 0) {
            GameController.Instance.SpendCoins(totalCost);

            playerStats.IncreaseArmour(armourQty);
            playerStats.IncreaseAttack(attackQty * 2);
            GameController.Instance.AddHealthPotions(potionQty);

            if (allyQty > 0) { 
                GameController.Instance.SpawnAllies(allyQty);
                GameController.Instance.UpdateAllies();
            }
            //Spawn x number of enemies 

            totalArmourBought += armourQty;
            totalAttackBought += attackQty;

            ResetQty();
            CalculateTotal();

            warning.SetActive(false);
        }
    }

    private int GetNumberAllies() {
            return GameSettings.allies.Count;
    }
}
