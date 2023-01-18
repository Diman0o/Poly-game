using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelGame;
    [SerializeField]
    private GameObject _panelEndGame;

    private int _menuSceneBuildIndex = 0;

    public void ExitLevel()
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
}

