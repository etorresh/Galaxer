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

    public IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady("bannerAd"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show("bannerAd");
    }
}
