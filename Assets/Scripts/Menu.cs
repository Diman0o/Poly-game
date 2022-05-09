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
    [SerializeField]
    private GameObject _panelLogInAndSignUp;
 
    private string requestType = "";

    public void Start()
    {
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        _panelLevels.SetActive(PanelsManager.panelLevelsIsActive);
        _panelSettings.SetActive(false);
    }

    public void OnLogInPressed()
    {
        requestType = "auth";
        OpenLogInAndSignUp();
    }

    public void OnSignUpPressed()
    {
        requestType = "register";
        OpenLogInAndSignUp();

    }

    public void OpenLogInAndSignUp()
    {
        _panelSettings.SetActive(false);
        _panelLogInAndSignUp.SetActive(true);
    }

    public void OnOkPressed()
    {
        //TODO: fetch login and password from input fields
        string[][] queryParams = new string[][] { new string[] {"login", }, new string[] {"password", } };
        StartCoroutine(WebRequest.ProcessRequest(requestType, queryParams, AccountData.setAccountId));
        CloseLogInAndSignUp();
    }

    public void CloseLogInAndSignUp()
    {
        _panelSettings.SetActive(true);
        _panelLogInAndSignUp.SetActive(false);
    }

    public void OpenLevels()
    {
        PanelsManager.panelMainMenuIsActive = false;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        PanelsManager.panelLevelsIsActive = true;
        _panelLevels.SetActive(PanelsManager.panelLevelsIsActive);
    }
    public void CloseLevels()
    {
        PanelsManager.panelMainMenuIsActive = true;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        PanelsManager.panelLevelsIsActive = false;
        _panelLevels.SetActive(PanelsManager.panelLevelsIsActive);
    }

    public void OpenSettings()
    {
        _panelSettings.SetActive(true);
        PanelsManager.panelMainMenuIsActive = false;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
    }
    public void CloseSettings()
    {
        PanelsManager.panelMainMenuIsActive = true;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
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
        
    }
}
