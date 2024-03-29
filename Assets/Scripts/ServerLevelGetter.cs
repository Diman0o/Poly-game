using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerLevelGetter : MonoBehaviour
{
    public static List<string> levelIds = new List<string>();
    public static IDictionary<string, string> customLevelAuthors  = new Dictionary<string, string>();

    public static GameObject levelTemplate;
    public static void updateServerLevels()
    {
        GameObject levelTemplate = (GameObject)Resources.Load("Prefabs/LevelTemplate");
        GameObject customLevelsContent = GameObject.FindGameObjectWithTag("CustomLevelsContent");

        foreach (string levelId in levelIds)
        {
            var newLevel = Instantiate(levelTemplate);
            newLevel.GetComponent<LevelsButtonsManager>().levelId = levelId;
            string authorName = new string("undefined");
            customLevelAuthors.TryGetValue(levelId, out authorName);
            newLevel.GetComponentInChildren<Text>().text = authorName;
            newLevel.transform.SetParent(customLevelsContent.transform);
            DataStorer.updateLevelButton(newLevel, "custom");
        }
    }

    public void updateLevels()
    {
        string[][] queryParams = new string[][] {};
        StartCoroutine(WebRequest.ProcessRequest("get_levels", queryParams, handleUpdateLevelsResult));
    }

    public void handleUpdateLevelsResult(string result)
    {
        Debug.Log($"Updating custom levels: {result}");
        levelIds.Clear();
        customLevelAuthors.Clear();
        foreach (string levelInfo in result.Split(","))
        {
            string[] components = levelInfo.Split(";");
            string levelId = components[0];
            string authorName = components[1];
            levelIds.Add(levelId);
            customLevelAuthors.Add(levelId, authorName);
        }
    }

    public void Start()
    {
        InvokeRepeating("updateLevels", 0f, 60f);
    }
}
