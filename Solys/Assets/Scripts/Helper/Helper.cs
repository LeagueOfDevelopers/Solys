using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    public Text tip;
    private GameObject TextForCloud;
    private int currentTip;
    public string[] tips;
    public Vector2[] tipsPositions;
    public GameObject Cloud;
    public GameObject clickPanel;
    // Use this for initialization
    void Start ()
    {
        /*Cloud = transform.GetChild(0).gameObject;
        tip = transform.GetChild(1).gameObject.GetComponent<Text>();
        clickPanel = transform.GetChild(2).gameObject;*/
        Button bt = clickPanel.gameObject.GetComponent<Button>();
        bt.targetGraphic = Cloud.GetComponent<Image>();
        bt.onClick.AddListener(ShowTip);
        
    }
    public void ShowTipAgain()
    {

        currentTip = 0;
        clickPanel.SetActive(true);
        Cloud.SetActive(true);
        tip.gameObject.SetActive(true);
        ShowTip();
    }

    public void ShowTip()
    {
        tip.resizeTextForBestFit = false;
        if (currentTip >= tips.Length)
        {
            CloseTips();
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
    
    public void CloseTips()
    {
        PlayerPrefsUtility.SetEncryptedInt("HelperShowedAtLevel" + SceneManager.GetActiveScene().buildIndex, 1);
        Cloud.SetActive(false);
        clickPanel.SetActive(false);
        tip.gameObject.SetActive(false);
        SetText(" ");
    }
}
