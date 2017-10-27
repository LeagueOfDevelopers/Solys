using UnityEngine.UI;
using UnityEngine;

public class MusicTogle : MonoBehaviour {

    private GameObject musicPlayer;
    private void OnEnable()
    {
        musicPlayer = GameObject.Find("MusicPlayer");
        if(musicPlayer)
        {
            GetComponent<Toggle>().isOn = musicPlayer.GetComponent<AudioSource>().mute;
            PrefsDriver.SetMusicMuted(GetComponent<Toggle>().isOn);
        }
    }

    public void ValueChange()
    {
        if(musicPlayer)
        {
            musicPlayer.GetComponent<AudioSource>().mute = GetComponent<Toggle>().isOn;
            PrefsDriver.SetMusicMuted(GetComponent<Toggle>().isOn);
        }
    }
}
