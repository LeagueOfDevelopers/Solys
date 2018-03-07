using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class GooglePlayAuth : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayGamesConfiguration();
    }
	

    private void PlayGamesConfiguration()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    private void Auth()
    {
        Social.localUser.Authenticate((bool success) => {
            
        });
    }
	
}
