using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour {

    public void OnClick()
    {
        GameObject.Find("Helper").GetComponent<Helper>().ShowTipAgain();
    }
}
