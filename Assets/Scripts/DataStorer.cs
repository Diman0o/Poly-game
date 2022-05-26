using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataStorer
{
    public static string login = "";
    public static string accountId = ""; //Do not save it in PlayersPrefs, it is unsafe
    public static string levelDataPrefix = "levelData_";
    public static IDictionary<string, GameObject> levelPreviewImages;
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
        //var levelsContent = GameObject.FindGameObjectWithTag("LevelsContent");
        Sprite notPassedSprite = Resources.Load<Sprite>("Previews/QuestionMark");

        var levels = GameObject.FindGameObjectsWithTag("Level");
        foreach (var level in levels)
        {
            string levelId = level.GetComponentInChildren<Text>().text;
            var image = level.GetComponent<Image>();
            Image preview = getPreviewImage(level, image);
            Debug.Log($"Laevelname: {level.name}");
            if (isLevelPast(levelId))
            {
                Sprite passedSprite = getPassedSprite(level.name);
                preview.sprite = passedSprite;
            }
            else
            {
                preview.sprite = notPassedSprite;
            }

            //var newLevel = GameObject.Instantiate(level);
            //newLevel.transform.parent = levelsContent.transform;
        }
    }

    private static Image getPreviewImage(GameObject level, Image levelImage)
    {
        Image[] previews = level.GetComponentsInChildren<Image>();
        foreach (var preview in previews)
        {
            if (preview != levelImage)
            {
                return preview;
            }
        }

        throw new System.ArgumentException("Preview not found");
    }

    private static Sprite getPassedSprite(string name)
    {
        Sprite passedSprite = Resources.Load<Sprite>($"Previews/{name}");
        if (passedSprite != null)
        {
            Debug.Log("NOT NULL");
            return passedSprite;
        }
        return Resources.Load<Sprite>("Previews/DonePreview");
    }

    public static void processSaveLevelAsPastOnServer(string result)
    {
        //Do nothing
    }
}
