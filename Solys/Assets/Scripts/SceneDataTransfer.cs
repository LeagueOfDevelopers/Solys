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


   

    public int FirstLevelInPack;
    public int LastLevelInPack;
    public string PackTitle;
    public int NeedToUpdateStarsForLevel;
    public int LastLevelRating = 0;
    public bool BuyShowed = false;
        //public string LoadingLevelData ="";
    #endregion


        }
