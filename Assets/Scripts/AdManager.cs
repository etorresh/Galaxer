using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    string gameId = "3287998";
    bool testMode = false;

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }


    public IEnumerator ShowVideoWhenReady()
    {
        yield return new WaitForSeconds(1f);
        while (!Advertisement.IsReady("video"))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Show("video");
    }

    public void ShowBannerWhenReady()
    {
        if (Advertisement.Banner.isLoaded || Advertisement.IsReady("bannerAd"))
        {
            Advertisement.Banner.Show("bannerAd");
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
}
