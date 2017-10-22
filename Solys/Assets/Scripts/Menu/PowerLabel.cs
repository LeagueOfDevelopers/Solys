using UnityEngine.UI;
using UnityEngine;

public class PowerLabel : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.GetChild(0).GetComponent<Text>().text = PrefsDriver.GetPower().ToString();
	}
}
