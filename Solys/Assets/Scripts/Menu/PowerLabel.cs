using UnityEngine.UI;
using UnityEngine;

public class PowerLabel : MonoBehaviour {

    public GameObject Label;
    public GameObject BuyPanel;

    private bool isNeedToUsePower = true;
    // Update is called once per frame
    void Update () {
        Label.GetComponent<Text>().text = PrefsDriver.GetPower().ToString();
	}
    private void OnEnable()
    {
        GeneralLogic.StartSimulationEvent += StartSimulation;
        GeneralLogic.ResetSimulationEvent += ResetSimulation;
        if (BuyPanel && IsNeedToShowBuyPanel())
            ShowBuyPanel();

    }

    private void OnDisable()
    {
        GeneralLogic.StartSimulationEvent -= StartSimulation;
        GeneralLogic.ResetSimulationEvent -= ResetSimulation;
    }

    private bool IsNeedToShowBuyPanel()
    {
        int power = PrefsDriver.GetPower();
        bool buyShowed = SceneDataTransfer.Instance.BuyShowed;
        return (!buyShowed && power < 20) || power == 0;
    }

    private void ShowBuyPanel()
    {
        SceneDataTransfer.Instance.BuyShowed = true;
        BuyPanel.GetComponent<Animator>().SetTrigger("Show");
    }

    public void StartSimulation()
    {
        if (isNeedToUsePower)
        {
            isNeedToUsePower = false;
            PrefsDriver.SpendPower();
        }
    }

    public void ResetSimulation()
    {
        isNeedToUsePower = true;
    }
}
