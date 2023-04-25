using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class UI_GameEnd : MonoBehaviour
{
    public AdManager adManager;
    public GameObject continueButton;
    public GameObject adPopup;
    public GameObject laterGroup;

    private void OnEnable()
    {
        StartCoroutine(PopupsCo());
        var source = LocalizationSettings.StringDatabase.SmartFormatter.GetSourceExtension<PersistentVariablesSource>();
        var distance = source["global"]["distance"] as StringVariable;
        var coins = source["global"]["coins"] as StringVariable;
        var score = source["global"]["score"] as StringVariable;
        var highScore = source["global"]["highScore"] as StringVariable;
        distance.Value = GameManager.instance.distance.ToString("#,#");
        coins.Value = GameManager.instance.coins.ToString();
        score.Value = GameManager.instance.score.ToString();
        highScore.Value = PlayerPrefs.GetInt("HighScore").ToString("#,#");
    }

    private IEnumerator PopupsCo()
    {
        adPopup.SetActive(true);
        yield return new WaitForSeconds(5f);
        laterGroup.SetActive(true);
    }

    public void ContinueButton()
    {
        adManager.ShowRewardedAd();
        laterGroup.SetActive(false);
    }

}
