using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
