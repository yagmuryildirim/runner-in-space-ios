using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameEndUI;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Image backgroundPanel;

    private bool gamePaused;
    private bool gameMuted;

    [Header("Volume Info")]
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image inGameMuteIcon;

    [Header("Volume Sliders")]
    [SerializeField] private VolumeSlider[] sliders;

    private void Start()
    {
        backgroundPanel.sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("BG", "1-Purple_640x360"));
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].SetupSlider();
        }
        SwitchMenuTo(mainMenu);
        SetupMuteIcons();
    }

    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        uiMenu.SetActive(true);

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
        AudioManager.instance.PlaySFX(4);
    }

    public void RestartLevelButton() => SceneManager.LoadScene(1);

    public void PauseGameButton()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
    }

    public void MuteButton()
    {
        Color muteColor = new Color(1, 1, 1, .5f);
        gameMuted = AudioListener.volume == 0;
        gameMuted = !gameMuted;

        if (gameMuted)
        {
            muteIcon.color = muteColor;
            inGameMuteIcon.color = muteColor;
            AudioListener.volume = 0;
        }
        else
        {
            muteIcon.color = Color.white;
            inGameMuteIcon.color = Color.white;
            AudioListener.volume = 1;
        }
    }

    public void StartGameButton()
    {
        GameManager.instance.UnlockPlayer();
    }

    public void OpenGameEndUI() => SwitchMenuTo(gameEndUI);
    public void OpenInGameUI() => SwitchMenuTo(inGameUI);

    private void SetupMuteIcons()
    {
        Color muteColor = new Color(1, 1, 1, .5f);
        if (AudioListener.volume == 0)
        {
            muteIcon.color = muteColor;
            inGameMuteIcon.color = muteColor;
        }
        else
        {
            muteIcon.color = Color.white;
            inGameMuteIcon.color = Color.white;
        }
    }
}
