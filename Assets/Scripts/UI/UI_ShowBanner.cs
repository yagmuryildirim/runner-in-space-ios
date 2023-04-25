using UnityEngine;

public class UI_ShowBanner : MonoBehaviour
{
    public AdManager adManager;

    private void OnEnable()
    {
        adManager.LoadBannerAd();
    }

    private void OnDisable()
    {
        adManager.DestroyBannerAd();
    }
}
