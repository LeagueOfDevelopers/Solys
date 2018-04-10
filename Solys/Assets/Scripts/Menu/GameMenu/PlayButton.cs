using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PlayButton : MonoBehaviour {

    public Sprite PlaySprite;
    public Sprite ReplaySprite;

    private Image image;
    private bool isPlayMode = false;

    public bool IsPlaying
    {
        get
        {
            return isPlayMode;
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
        isPlayMode = false; 
        if (!Advertisement.isInitialized && PrefsDriver.GetPower() < 5)
            Advertisement.Initialize(AdsButton.gameId, true);

    }

    private void Update()
    {
        image.sprite = isPlayMode ? ReplaySprite : PlaySprite;
    }

    public void OnClick()
    {
        int power = PrefsDriver.GetPower();

        if (!isPlayMode)
            PlayPressed(power);
        else
            ReplayPressed();

        if (!Advertisement.isInitialized && power < 5)
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
        isPlayMode = true;
        if (power > 0)
            GeneralLogic.StartSimulationEvent();
        else
            if (PrefsDriver.IsPackGroupUnlocked(3))
            ShowAd();
        else
        {
            PrefsDriver.AddPower(3);
            GeneralLogic.StartSimulationEvent();
        }
        
    }


    void ShowAd()
    {
        if (Advertisement.isSupported)
        {
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
            GeneralLogic.StartSimulationEvent();

        }
        else
            if (result == ShowResult.Failed) SceneManager.LoadScene(1);


    }

    private void ReplayPressed()
    {
        isPlayMode = false;
        GeneralLogic.StopSimulationEvent();
    }
}
