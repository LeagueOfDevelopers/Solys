using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class AdsButton : MonoBehaviour {
    string gameId = "1583833";
    string placement = "rewardedVideo";
    Button btn;

    public GameObject Widget;

    // Use this for initialization
    void Start () {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, true);
        }
        btn = GetComponent<Button>();
    }

    void Update()
    {
            btn.interactable = Advertisement.IsReady();
    }

    public void OnClick()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(placement, options);
    }

    private void HandleShowResult(ShowResult result)
    {
        if(result == ShowResult.Finished) {
            PrefsDriver.AddPower();

        }
        Widget.GetComponent<Animator>().SetTrigger("End");
    }
}
