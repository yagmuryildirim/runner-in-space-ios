using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Colors
{
    public Color color;
    public Sprite sprite;
    public int price;
}
[System.Serializable]
public struct Boost
{
    public BoostType boost;
    public int price;
}

public enum BoostType { jump, speed, coin }

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] Image backgroundPanel;

    [Header("Plaform")]
    [SerializeField] private Colors[] colors;
    [SerializeField] private GameObject colorButton;
    [SerializeField] private Transform colorsParent;
    [SerializeField] private Image colorDisplay;

    [Header("Boosts")]
    [SerializeField] private Boost[] boostsArray;
    [SerializeField] private GameObject boostButton;
    [SerializeField] private Transform boostParent;

    private void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
        for (int i = 0; i < boostsArray.Length; i++)
        {
            GameObject boostButton = boostParent.GetChild(i).gameObject;
            BoostType boost = boostsArray[i].boost;
            int price = boostsArray[i].price;

            boostButton.GetComponent<Button>().onClick.AddListener(() => PurchaseBoost(boost, price));
        }
        for (int i = 0; i < colors.Length; i++)
        {
            GameObject newButton = Instantiate(colorButton, colorsParent);
            Color color = colors[i].color;
            int price = colors[i].price;
            Sprite sprite = colors[i].sprite;
            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = price.ToString();

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(sprite, color, price));
        }
    }

    public void PurchaseBoost(BoostType boost, int price)
    {
        if (EnoughMoney(price))
        {
            AudioManager.instance.PlaySFX(4);
            GameManager.instance.SetBoost(boost);
        }
    }

    public void PurchaseColor(Sprite sprite, Color color, int price)
    {
        if (EnoughMoney(price))
        {
            AudioManager.instance.PlaySFX(4);
            backgroundPanel.sprite = sprite;
            PlayerPrefs.SetString("BG", sprite.name);
            LevelGenerator.instance.PlatformColor = color;
            UIThemeManager.instance.ChangeColor(color);
        }
    }

    private bool EnoughMoney(int price)
    {
        int savedCoins = PlayerPrefs.GetInt("Coins");
        if (savedCoins >= price)
        {
            int newCoins = savedCoins - price;
            PlayerPrefs.SetInt("Coins", newCoins);
            coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");
            Debug.Log("Purchase Succesful");
            return true;
        }

        else
        {
            Debug.Log("Not Enough Money");
            return false;
        }
    }
}
