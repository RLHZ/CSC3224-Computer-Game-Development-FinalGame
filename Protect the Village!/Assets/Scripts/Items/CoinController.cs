using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : PickupObject
{
    public override void OnTriggerEnter(Collider collider) {
        base.OnTriggerEnter(collider);
        GameController.Instance.AddCoins(Mathf.FloorToInt(Random.Range(1, 5)));
    }
}
