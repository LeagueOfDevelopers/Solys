using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {

    void Start()
    {
        
        if (!Advertisement.isInitialized && PrefsDriver.GetPower() < 5)
            Advertisement.Initialize(AdsButton.gameId, true);

    }

    public void OnClick(bool isPlayPressed)
    {
        int power = PrefsDriver.GetPower();

        if (isPlayPressed)
            PlayPressed(power);
        else
            ReplayPressed();

        if(!Advertisement.isInitialized && power<5)
            Advertisement.Initialize(AdsButton.gameId, true);

    }

    private void PlayPressed(int power)
    {      
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
            GeneralLogic.StartSimulationEvent();

        }
        else
            if (result == ShowResult.Failed) SceneManager.LoadScene(1);


    }

    private void ReplayPressed()
    {
        GeneralLogic.StopSimulationEvent();
    }
}
