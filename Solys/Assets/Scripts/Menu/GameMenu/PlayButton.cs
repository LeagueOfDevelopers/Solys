using UnityEngine;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {


    public void OnClick(bool isPlayPressed)
    {
        if (isPlayPressed)
            PlayPressed();
        else
            ReplayPressed();

    }

    private void PlayPressed()
    {
        if (PrefsDriver.GetPower() > 0)
            GeneralLogic.StartSimulationEvent();
        else
            ShowAd();
    }

    void ShowAd()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(AdsButton.gameId, true);
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;

            Advertisement.Show(AdsButton.placement, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PrefsDriver.AddPower();

        }
    }

    private void ReplayPressed()
    {
        GeneralLogic.ResetSimulationEvent();
    }
}
