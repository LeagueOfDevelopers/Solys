using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFileDeserialize {

    public static void GetHelpArrayForLevel(int buildindex)
    {
        
        
    }

    private static HelpersForLevel LoadFile()
    {
        TextAsset helper = Resources.Load<TextAsset>("Helpers.txt");
        return JsonUtility.FromJson<HelpersForLevel>(helper.text);
    }
}
