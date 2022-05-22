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
        DataStorer.updateLevelButtons();
        PanelsManager.panelMainMenuIsActive = false;
        PanelsManager.panelLevelsIsActive = true;
        SceneManager.LoadScene(_menuSceneBuildIndex);
    }
}

