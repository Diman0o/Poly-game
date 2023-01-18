using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class LevelsButtonsManager : MonoBehaviour
{
    private int _gameSceneBuildIndex = 1;
    private int _menuSceneBuildIndex = 0;
    public Text textLevelNumber;
    public static string levelNumber;

    public static string levelMode;
    public string levelId; //Used only for custom levels
    public static string levelData; //Used only for custom levels
    public static string lastCustomLevelId; //Used only for custom levels

    // Start is called before the first frame update
    public void PlayGame()
    {
        levelMode = "main";
        textLevelNumber = GetComponent<Text>();
        levelNumber = textLevelNumber.text;
        PanelsManager.panelGameIsActive = true;
        PanelsManager.panelEndGameIsActive = false;
        SceneManager.LoadScene(_gameSceneBuildIndex);
    }

    public void PlayCustomGame()
    {
        levelMode = "custom";
        lastCustomLevelId = levelId;
        string[][] queryParams = new string[][] { new string[] { "levelName", levelId } };
        StartCoroutine(WebRequest.ProcessRequest("get_level_info", queryParams, handleLevelInfo));
    }

    public void PlayNextLevelGame()
    {
        Debug.Log($"PrevLevel: {levelNumber}");
        if (levelNumber == "2")
        {
            PanelsManager.panelMainMenuIsActive = false;

            if (LevelsButtonsManager.levelMode == "main")
            {
                PanelsManager.panelLevelsIsActive = true;
            }
            else
            {
                PanelsManager.panelCustomLevelsIsActive = true;
            }
            SceneManager.LoadScene(_menuSceneBuildIndex);
        }
        else
        {
            int prevLevelNumber = int.Parse(levelNumber, CultureInfo.InvariantCulture.NumberFormat);
            int intLevelNumber = prevLevelNumber + 1;
            levelNumber = intLevelNumber.ToString();
            PanelsManager.panelGameIsActive = true;
            PanelsManager.panelEndGameIsActive = false;
            SceneManager.LoadScene(_gameSceneBuildIndex);

        }

    }

    private void handleLevelInfo(string result)
    {
        Debug.Log($"LevelInfo: {result}");
        string fixedResult = result.Replace("\\r\\n", "\n");
        Debug.Log($"FixedLevelInfo: {fixedResult}");
        levelData = fixedResult;
        PanelsManager.panelGameIsActive = true;
        PanelsManager.panelEndGameIsActive = false;
        SceneManager.LoadScene(_gameSceneBuildIndex);
    }
}
