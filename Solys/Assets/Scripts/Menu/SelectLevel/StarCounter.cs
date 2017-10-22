using UnityEngine.UI;
using UnityEngine;

public class StarCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<Text>().text = PrefsDriver.GetAvailableStars().ToString();
	}
	
}
