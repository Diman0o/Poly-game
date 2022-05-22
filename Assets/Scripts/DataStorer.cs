using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataStorer
{
    public static string login = "";
    public static string accountId = ""; //Do not save it in PlayersPrefs, it is unsave
    public static string levelDataPrefix = "levelData_";
    public static void saveLevelAsPast(string levelId)
    {
        PlayerPrefs.SetInt(levelDataPrefix + levelId, 1);
        if (!string.IsNullOrWhiteSpace(accountId))
        {
            
        }
    }

    public static bool isLevelPast(string levelId)
    {
        if (PlayerPrefs.GetInt(levelDataPrefix + levelId, 0) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void updateLevelButtons()
    {
        Color notPassedColor = new Color(108.0f / 255.0f, 75.0f / 255.0f, 154.0f / 255.0f);
        Color passedColor = new Color(80.0f / 255.0f, 180.0f / 255.0f, 25.0f / 255.0f);

        var levels = GameObject.FindGameObjectsWithTag("Level");
        foreach (var level in levels)
        {
            string levelId = level.GetComponentInChildren<Text>().text;
            var image = level.GetComponent<Image>();
            Debug.Log($"Checking level passed, id: {levelId}, old_color: {image.color}");
            if (isLevelPast(levelId))
            {
                Debug.Log($"Checking level passed, id: {levelId}, result is past, new_color: {level.GetComponent<Image>().color}");

                level.GetComponent<Image>().color = passedColor;
            }
            else
            {
                level.GetComponent<Image>().color = notPassedColor;

                Debug.Log($"Checking level passed, id: {levelId}, result is not past, new_color: {level.GetComponent<Image>().color}");

            }
        }
    }

    public static void processSaveLevelAsPastOnServer(string result)
    {
        //Do nothing
    }
}
