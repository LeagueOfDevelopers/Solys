using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    private Text tip;
    private GameObject TextForCloud;
    private int currentTip;
    public string[] tips;
    public Vector2[] tipsPositions;
    private GameObject Cloud;
	// Use this for initialization
	void Start ()
    {
        Cloud = transform.GetChild(0).gameObject;
        tip = transform.GetChild(1).gameObject.GetComponent<Text>();
        Button bt = tip.gameObject.AddComponent<Button>();
        bt.targetGraphic = Cloud.GetComponent<Image>();
        bt.onClick.AddListener(ShowTip);
        if (tips == null)
        {
            Cloud.SetActive(false);
            SetText(" ");
            return;
        }
        currentTip = 0;
        ShowTip();
    }
    

    public void ShowTip()
    {
        tip.resizeTextForBestFit = false;
        if (currentTip >= tips.Length)
        {
            Cloud.SetActive(false);
            SetText(" ");
            Destroy(this.gameObject);
            return;
        }

        GetComponent<RectTransform>().anchoredPosition = tipsPositions[currentTip];
        SetText(tips[currentTip]);
        int kp = tips[currentTip].Length / 40;
        kp++;
        tip.transform.position = this.transform.position; 
        RectTransform rt = Cloud.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(tip.preferredWidth+30, tip.preferredHeight+30);
        rt = tip.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((tip.preferredWidth + 30 )/ kp, tip.preferredHeight );
        tip.resizeTextForBestFit = true;
        currentTip++;
    }

    public void SetText(string textString)
    {
        tip.text = textString;

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        tip.font = ArialFont;
        tip.material = ArialFont.material;
    }
    
}
