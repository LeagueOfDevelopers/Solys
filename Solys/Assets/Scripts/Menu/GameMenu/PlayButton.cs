using UnityEngine;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {


	public void OnClick()
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
}
