using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    private Text tip;
    private GameObject TextForCloud;
    private int currentTip;
    public string[] tips;
    public Vector2[] tipsPositions;
    private GameObject Cloud;
    private GameObject clickPanel;
    // Use this for initialization
    void Start ()
    {
        Cloud = transform.GetChild(0).gameObject;
        tip = transform.GetChild(1).gameObject.GetComponent<Text>();
        clickPanel = transform.GetChild(2).gameObject;
        Button bt = clickPanel.gameObject.GetComponent<Button>();
        bt.targetGraphic = Cloud.GetComponent<Image>();
        bt.onClick.AddListener(ShowTip);
        clickPanel.SetActive(false);
        bool isShowed =
         PlayerPrefsUtility.GetEncryptedInt("HelperShowedAtLevel" + SceneManager.GetActiveScene().buildIndex) == 1;

        if (tips == null||isShowed)
        {
            Cloud.SetActive(false);
            SetText(" ");
            return;
        }
        ShowTipAgain(); 
    }
    public void ShowTipAgain()
    {

        currentTip = 0;
        clickPanel.SetActive(true);
        Cloud.SetActive(true);
        this.gameObject.SetActive(true);
        ShowTip();
    }

    public void ShowTip()
    {
        tip.resizeTextForBestFit = false;
        if (currentTip >= tips.Length)
        {
            PlayerPrefsUtility.SetEncryptedInt("HelperShowedAtLevel"+ SceneManager.GetActiveScene().buildIndex,1);
            Cloud.SetActive(false);
            SetText(" ");
            this.gameObject.SetActive(false);
            return;
        }

        //GetComponent<RectTransform>().anchoredPosition = tipsPositions[currentTip];
        SetText(tips[currentTip]);
        /*int kp = tips[currentTip].Length / 40;
        kp++;
        tip.transform.position = this.transform.position; 
        RectTransform rt = Cloud.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(tip.preferredWidth+30, tip.preferredHeight+30);
        rt = tip.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((tip.preferredWidth + 30 )/ kp, tip.preferredHeight );*/
        tip.resizeTextForBestFit = true;
        currentTip++;
    }

    public void SetText(string textString)
    {
        tip.text = textString;
    }
    
}
