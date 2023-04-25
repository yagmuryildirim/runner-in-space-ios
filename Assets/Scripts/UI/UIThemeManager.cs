using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIThemeManager : MonoBehaviour
{

    [SerializeField] private Color currentColor;
    public static UIThemeManager instance;
    public GameObject mainUI;
    public GameObject navigationButtons;
    public GameObject startingPlatform;
    private Color defaultColor;
    private float alpha = 0.6f;

    private void Awake()
    {
        defaultColor = currentColor;
        ChangeColor(new Color(PlayerPrefs.GetFloat("r", defaultColor.r), PlayerPrefs.GetFloat("g", defaultColor.g), PlayerPrefs.GetFloat("b", defaultColor.b), alpha));
        instance = this;
    }

    public void ChangeColor(Color color)
    {
        currentColor = new Color(color.r, color.g, color.b, alpha);
        ChangeColorInGame();
        PlayerPrefs.SetFloat("r", color.r);
        PlayerPrefs.SetFloat("g", color.g);
        PlayerPrefs.SetFloat("b", color.b);
    }

    private void ChangeColorInGame()
    {
        Color fullOpacity = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
        startingPlatform.GetComponent<SpriteRenderer>().color = fullOpacity;
        navigationButtons.transform.GetChild(0).GetComponent<Image>().color = fullOpacity;
        navigationButtons.transform.GetChild(1).GetComponent<Image>().color = fullOpacity;

        foreach (Transform child in mainUI.transform)
        {
            if (child.name == "Game End UI") child.GetComponent<Image>().color = currentColor;
            foreach (Transform childOfChild in child)
            {
                if (childOfChild.name == "Start Button") continue;
                if (childOfChild.name == "Jump Button") continue;
                if (childOfChild.name == "Buttons")
                {
                    foreach (Transform button in childOfChild)
                    {
                        button.GetComponent<Image>().color = currentColor;
                    }
                }
                if (childOfChild.name == "Info Bar" || childOfChild.name == "Sliders")
                {
                    childOfChild.GetComponent<Image>().color = currentColor;
                }
                if (childOfChild.GetComponent<Button>() != null)
                {
                    childOfChild.GetComponent<Image>().color = currentColor;
                }
            }
        }
    }
}
