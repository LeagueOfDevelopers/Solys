using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {

    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(AdsButton.gameId, true);
        }
    }

    public void OnClick(bool isPlayPressed)
    {
        if (isPlayPressed)
            PlayPressed();
        else
            ReplayPressed();

    }

    private void PlayPressed()
    {
        int power = PrefsDriver.GetPower();
        if (power > 0)
            GeneralLogic.StartSimulationEvent();
        else
            ShowAd();
        

    }

    void ShowAd()
    {
        if (Advertisement.isSupported)
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = HandleShowResult;
            //GetComponent<Toggle>().isOn = false;
            Advertisement.Show(AdsButton.placement, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PrefsDriver.AddPower();
            Advertisement.Initialize(AdsButton.gameId, true);
            GeneralLogic.StartSimulationEvent();

        }
        else
            if (result == ShowResult.Failed) SceneManager.LoadScene(1);


    }

    private void ReplayPressed()
    {
        GeneralLogic.ResetSimulationEvent();
    }
}
