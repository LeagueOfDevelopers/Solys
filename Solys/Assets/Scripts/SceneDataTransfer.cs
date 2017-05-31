using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SceneDataTransfer
{
    #region Singleton
        static SceneDataTransfer(){}
        private static readonly SceneDataTransfer _instance = new SceneDataTransfer();
        public static SceneDataTransfer Instance{ get { return _instance; } }
        private static Dictionary<string, int> RatingPassages;

    public bool LoadLevelData(int id)
        {
        TextAsset asset = Resources.Load("Level/"+id, typeof(TextAsset)) as TextAsset;
        if(asset == null) return false;
        string levelData = asset.text;
        Resources.UnloadAsset(asset);

        CurrentSceneID = id;
        LoadingLevelData = levelData;
        Debug.Log(levelData);
        return true;
        }

        public int CurrentSceneID;
        public string LoadingLevelData ="";
        public int currentRate
    {
        get
        {
            int rate=0;
            if (RatingPassages == null) RatingPassages = new Dictionary<string, int>();
            RatingPassages.TryGetValue("Level / " + CurrentSceneID.ToString(), out rate);
            return rate;
        }
        set
        {
            int rate=0;
            if (RatingPassages == null) RatingPassages = new Dictionary<string, int>();
            RatingPassages.TryGetValue("Level / " + CurrentSceneID.ToString(), out rate);
            if (value > rate) { RatingPassages.Remove("Level / " + CurrentSceneID.ToString());
                RatingPassages.Add("Level / " + CurrentSceneID.ToString(), value);
            }
        }
    }
    #endregion


        }
