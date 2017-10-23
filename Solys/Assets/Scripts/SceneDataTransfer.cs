using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class SceneDataTransfer
{
    #region Singleton
        static SceneDataTransfer(){}
        private static readonly SceneDataTransfer _instance = new SceneDataTransfer();
        public static SceneDataTransfer Instance{ get { return _instance; } }


    //Закомментированный вблок десериализации уровня. Вроде нах не нужен
    /*
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
        */
        public int CurrentSceneID;

    public int FirstLevelInPack;
    public int LastLevelInPack;
    public string PackTitle;
    public int NeedToUpdateStarsForLevel;
    public int LastLevelRating = 0;
    public bool BuyShowed = false;
        //public string LoadingLevelData ="";
    #endregion


        }
