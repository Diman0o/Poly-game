using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class LevelsButtonsManager : MonoBehaviour
{
    private int _gameSceneBuildIndex = 1;

    public Text textLevelNumber;
    public static string levelNumber;

    // Start is called before the first frame update
    public void PlayGame()
    {
        textLevelNumber = GetComponent<Text>();
        levelNumber = textLevelNumber.text;
        PanelsManager.panelGameIsActive = true;
        PanelsManager.panelEndGameIsActive = false;
        SceneManager.LoadScene(_gameSceneBuildIndex);
    }

    public void PlayNextLevelGame()
    {
        int intLevelNumber = int.Parse(levelNumber, CultureInfo.InvariantCulture.NumberFormat) + 1;
        levelNumber = intLevelNumber.ToString();
        PanelsManager.panelGameIsActive = true;
        PanelsManager.panelEndGameIsActive = false;
        SceneManager.LoadScene(_gameSceneBuildIndex);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
