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

    public int potionCost;
    public int armourCost;
    public int attackCost;
    int totalCost;

    public Text costSummary;
    public Text potionCost_text;
    public Text armourCost_text;
    public Text attackCost_text;

    private int potionQty;
    private int armourQty;
    private int attackQty;

    public Text potionQtyText;
    public Text armourQtyText;
    public Text attackQtyText;

    public enum BuyableItems { Potion, Armour, Attack}

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!isShopOpen && !GameController.isPaused && !GameController.isFinished) {
                OpenShopWindow();
            }
            else if (isShopOpen) {
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
                armourQty++;
                break;
            case BuyableItems.Attack:
                attackQty++;
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
        }
        warning.SetActive(false);
        SetQuantitiesTexts();
        CalculateTotal();
    }

    void SetQuantitiesTexts() {
        potionQtyText.text = potionQty.ToString();
        armourQtyText.text = armourQty.ToString();
        attackQtyText.text = attackQty.ToString();
    }

    private void CalculateTotal() {
        int coinsAvailable = GameController.Instance.coinsAvailable;
        totalCost = potionQty * potionCost + attackQty * attackCost + armourQty * armourCost;
        int coinsLeft = coinsAvailable - totalCost;

        costSummary.text = string.Format("{0}\n{1}\n{2}", coinsAvailable, totalCost, coinsLeft);
    }

    public void Buy() {
        if (totalCost > GameController.Instance.coinsAvailable) {
            warning.SetActive(true);
        }
        else if (totalCost > 0) {
            GameController.Instance.SpendCoins(totalCost);

            playerStats.IncreaseArmour(armourQty * 5);
            playerStats.IncreaseAttack(attackQty * 5);
            GameController.Instance.AddHealthPotions(potionQty);

            ResetQty();
            CalculateTotal();

            warning.SetActive(false);
        }
    }
}
