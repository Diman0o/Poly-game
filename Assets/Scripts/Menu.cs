using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelMainMenu;
    [SerializeField]
    private GameObject _panelLevels;
    [SerializeField]
    private GameObject _panelSettings;

    public void OpenLevels()
    {
        PanelsManager.panelMainMenuIsActive = false;
        PanelsManager.panelLevelsIsActive = true;
    }
    public void CloseLevels()
    {
        PanelsManager.panelMainMenuIsActive = true;
        PanelsManager.panelLevelsIsActive = false;
    }

    public void OpenSettings()
    {
        _panelSettings.SetActive(true);
        PanelsManager.panelMainMenuIsActive = false;
    }
    public void CloseSettings()
    {
        PanelsManager.panelMainMenuIsActive = true;
        _panelSettings.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void Update()
    {
        if (PanelsManager.panelMainMenuIsActive)
        {
            _panelMainMenu.SetActive(true);
        }
        else _panelMainMenu.SetActive(false);

        if (PanelsManager.panelLevelsIsActive)
        {
            _panelLevels.SetActive(true);
        }
        else _panelLevels.SetActive(false);
    }
}
