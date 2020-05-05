using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopRowController : MonoBehaviour
{
    public ShopController.BuyableItems buyableItem;

    public void IncreaseQuantityHandler() {
        ShopController.Instance.IncreaseQuantity(buyableItem);
    }

    public void DecreaseQuantityHandler() {
        ShopController.Instance.DecreaseQuantity(buyableItem);
    }
}
