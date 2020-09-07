#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using UnityEngine;
using System;

public class AdsManager : MonoBehaviour//Реклама (пока не работает)
{
    Menu menu;
    [SerializeField] string gameID = "3126730";
    [SerializeField] string videoPlacementID = "video";
    [SerializeField] string rewardedVideoPlacementID = "rewardedVideo";

    public void Start()
    {
        menu = GetComponent<Menu>();
#if UNITY_ADS
        Advertisement.Initialize(gameID, true);
#endif
    }

    public bool CheckInternet()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public bool CheckMonetizationReward()
    {
        return Advertisement.IsReady(videoPlacementID);
    }

    [Obsolete]
    public void ResultRewardPlay()
    {
#if UNITY_ADS
        if (CheckMonetizationReward())
        {
            ShowOptions so = new ShowOptions()
            { resultCallback = ResultReward };
            Advertisement.Show(videoPlacementID);
        }
        return;
#endif
    }

    public bool CheckMonetizationRestart()
    {
        return Advertisement.IsReady(rewardedVideoPlacementID);
    }

    [Obsolete]
    public void ResultRestartPlay()
    {
        if (CheckMonetizationRestart())
        {
            ShowOptions so = new ShowOptions
            { resultCallback = ResultRestart };
            Advertisement.Show(rewardedVideoPlacementID);
        }
        return;
    }
    
    public void ResultReward(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                menu.ResultRewardFinished();
                break;
            case ShowResult.Skipped:
                menu.ResultRewardSkipped();
                break;
            case ShowResult.Failed:
                break;
        }
    }
    
    public void ResultRestart(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                menu.ResultRestartFinished();
                break;
            case ShowResult.Skipped:
            case ShowResult.Failed:
                break;
        }
    }
}