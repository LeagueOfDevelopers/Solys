using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Localization;


public class HelperTextHandler : MonoBehaviour {
    private void Start()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        GetComponent<Helper>().tips = LeanLocalization.CurrentLanguage == "Russian" ? HelperTextData.GetRussian(activeScene) : HelperTextData.GetEnglish(activeScene);
        bool isShowed =
         PlayerPrefsUtility.GetEncryptedInt("HelperShowedAtLevel" + activeScene) == 1;
        if (!isShowed || HelperTextData.GetRussian(activeScene).Length == 0)
            GetComponent<Helper>().ShowTipAgain();
        else
            GetComponent<Helper>().CloseTips();
    }

}
