using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackGroupUnlocker : MonoBehaviour {

    private void Start()
    {
        UpdatePackList();
    }

    public void UpdatePackList()
    {
        for (int i = 0; i < transform.childCount; i++)
            AllowPackIfNeeded(transform.GetChild(i).gameObject);

    }

    private void AllowPackIfNeeded(GameObject Pack)
    {
        SelectPackButton element = Pack.GetComponent<SelectPackButton>();
        Pack.SetActive(PrefsDriver.IsPackGroupUnlocked(element.PackGroup) || element.PackGroup == 0);
    }
}
