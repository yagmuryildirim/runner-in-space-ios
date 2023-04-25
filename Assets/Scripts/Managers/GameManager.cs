using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public UIManager ui;
    private AdManager adManager;

    [Header("UI")]
    [SerializeField] private GameObject gameUI;

    [Header("Score")]
    public int coins;
    public float distance;
    public int score;

    [Header("Colors")]
    public Color platformColor;

    private int coinBoost = 1;

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
    }

    private void Start()
    {
        PlayerPrefs.GetInt("Coins", 0);
        adManager = FindObjectOfType<AdManager>();
        adManager.CreateBannerView();
        adManager.LoadRewardedAd();
    }

    private void Update()
    {
        if (player == null) return;
        if (player.transform.position.x > distance) distance = player.transform.position.x;
    }

    public void SetBoost(BoostType boostType)
    {
        if (boostType == BoostType.coin)
            coinBoost = 2;
        else
            player.SetBoost(boostType);
    }

    public void UnlockPlayer() => player.isRunning = true;

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        SaveInfo();
        ui.OpenGameEndUI();
    }

    public void IncrementCoins()
    {
        coins += coinBoost;
    }

    public void ContinueGame()
    {
        adManager.DisableRewardPopup();
        StartCoroutine(ContinueGameCo());
    }

    private IEnumerator ContinueGameCo()
    {
        UnlockPlayer();
        player.transform.position = new Vector3(player.transform.position.x + 5, 10);
        player.gameObject.SetActive(true);
        ui.OpenInGameUI();
        Time.timeScale = 0.6f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
    }

    private void SaveInfo()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", savedCoins + coins);

        score = (int)(distance * coins);
        PlayerPrefs.SetInt("LastScore", score);

        //New highscore
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            FindObjectOfType<LeaderboardManager>().ReportScore(score);
        }
    }
}
