using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public static List<Transform> BadGuys = new List<Transform>();
    public static List<Transform> GoodGuys = new List<Transform>();
    public static List<Transform> Buildings = new List<Transform>();

    public static List<Transform> allies = new List<Transform>();

    void Awake() {
        instance = this;
    }

    public static void Reset() {
        BadGuys = new List<Transform>();
        GoodGuys = new List<Transform>();
        Buildings = new List<Transform>();
        allies = new List<Transform>();
    }


}
