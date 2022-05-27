using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelsButtonManager : MonoBehaviour
{
    public string levelId;

    public void PlayGame()
    {
        string[][] queryParams = new string[][] { new string[] { "levelName", levelId } };
        StartCoroutine(WebRequest.ProcessRequest("get_level_info", queryParams, handleLevelInfo));
    }

    private void handleLevelInfo(string result)
    {
        Debug.Log($"LevelInfo: {result}");
    }
}
