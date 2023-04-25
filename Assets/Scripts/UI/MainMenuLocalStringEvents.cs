using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class MainMenuLocalStringEvents : MonoBehaviour
{

    private void Start()
    {
        var source = LocalizationSettings.StringDatabase.SmartFormatter.GetSourceExtension<PersistentVariablesSource>();
        var lastScore = source["global"]["lastScore"] as StringVariable;
        var bestScore = source["global"]["bestScore"] as StringVariable;
        lastScore.Value = PlayerPrefs.GetInt("LastScore").ToString();
        bestScore.Value = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
