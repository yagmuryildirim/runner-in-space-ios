using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;

    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI coinsText;

    private float distance;
    private float coins;

    private void Start()
    {
        player = GameManager.instance.player;
        InvokeRepeating("UpdateInfo", 0, .2f);
    }

    private void UpdateInfo()
    {
        distance = GameManager.instance.distance;
        coins = GameManager.instance.coins;
        if (distance > 0)
            distanceText.text = distance.ToString("#,#") + " M";

        if (coins > 0)
            coinsText.text = coins.ToString("#,#");
    }

}
