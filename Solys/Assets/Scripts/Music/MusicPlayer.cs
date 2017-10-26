using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	void Start () {
        GetComponent<AudioSource>().mute = PrefsDriver.IsMusicMuted();
		DontDestroyOnLoad(this.gameObject);
		if(GameObject.Find("MusicPlayer") != this.gameObject)
		{
			Destroy(this.gameObject);
		}
	}

}
