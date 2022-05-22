using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelMainMenu;
    [SerializeField]
    private GameObject _panelLevels;
    [SerializeField]
    public GameObject _panelSettings;
    [SerializeField]
    public GameObject _panelLogInAndSignUp;

    public InputField loginInputField;
    public InputField passwordInputField;

    public Text signUpAndLogInState;
    public Text login;

    private string requestType = "";

    public void Start()
    {
        DataStorer.updateLevelButtons();
        login.text = DataStorer.login;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        _panelLevels.SetActive(PanelsManager.panelLevelsIsActive);
        _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
        _panelSettings.SetActive(PanelsManager.panelLogInAndSignUpIsActive);
    }

    public void OnLogInPressed()
    {
        requestType = "login";
        OpenLogInAndSignUp();
    }

    public void OnSignUpPressed()
    {
        requestType = "register";
        Debug.Log("Set request to register");
        OpenLogInAndSignUp();
    }

    public void OpenLogInAndSignUp()
    {
        signUpAndLogInState.text = "";
        loginInputField.text = "";
        passwordInputField.text = "";
        PanelsManager.panelSettingsIsActive = false;
        _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
        PanelsManager.panelLogInAndSignUpIsActive = true;
        _panelLogInAndSignUp.SetActive(PanelsManager.panelLogInAndSignUpIsActive);
    }

    public void OnOkPressed()
    {
        string login = loginInputField.text;
        string password = passwordInputField.text;
        Debug.Log($"Login: {login}, password: {password}, request: {requestType}");
        string[][] queryParams = new string[][] { new string[] {"login", login}, new string[] {"password", password} };
        Action<string> requestResultHandler;
        if (requestType == "login")
        {
            requestResultHandler = handleAuthResult;
        }
        else
        {
            requestResultHandler = handleRegisterResult;
        }

        StartCoroutine(WebRequest.ProcessRequest(requestType, queryParams, requestResultHandler));
        signUpAndLogInState.text = "Обрабатывается...";
    }

    public void CloseLogInAndSignUp()
    {
        PanelsManager.panelSettingsIsActive = true;
        _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
        PanelsManager.panelLogInAndSignUpIsActive = false;
        _panelLogInAndSignUp.SetActive(PanelsManager.panelLogInAndSignUpIsActive);
    }

    public void OpenLevels()
    {
        PanelsManager.panelMainMenuIsActive = false;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        PanelsManager.panelLevelsIsActive = true;
        _panelLevels.SetActive(PanelsManager.panelLevelsIsActive);
        DataStorer.updateLevelButtons();
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
        PanelsManager.panelSettingsIsActive = true;
        _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
        PanelsManager.panelMainMenuIsActive = false;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
    }
    public void CloseSettings()
    {
        PanelsManager.panelMainMenuIsActive = true;
        _panelMainMenu.SetActive(PanelsManager.panelMainMenuIsActive);
        PanelsManager.panelSettingsIsActive = false;
        _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
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
        Debug.Log($"login: ${login.text}");
        
    }

    public void handleAuthResult(string authResult)
    {
        if (authResult == "ERROR")
        {
            DataStorer.accountId = "";
            if (signUpAndLogInState.text == "Обрабатывается...")
            {
                signUpAndLogInState.text = "Ошибка, попробуйте снова";
            }
        }
        else
        {
            string[] result = authResult.Split(",");
            DataStorer.login = result[0];
            DataStorer.accountId = result[1];
            login.text = DataStorer.login;
            if (signUpAndLogInState.text == "Обрабатывается...")
            {
                PanelsManager.panelSettingsIsActive = true;
                _panelSettings.SetActive(PanelsManager.panelSettingsIsActive);
                PanelsManager.panelLogInAndSignUpIsActive = false;
                _panelLogInAndSignUp.SetActive(PanelsManager.panelLogInAndSignUpIsActive);
            }

            DataStorer.clear();

            string[][] queryParams = new string[][] { new string[] { "accountId", DataStorer.accountId}};
            StartCoroutine(WebRequest.ProcessRequest("get_passed_levels", queryParams, handleGetPassedLevelsResult));
        }
    }

    public void handleRegisterResult(string registerResult)
    {
        Debug.Log($"Handling register result = {registerResult}, text={signUpAndLogInState.text}");

        if (registerResult == "OK")
        {
            if (signUpAndLogInState.text == "Обрабатывается...")
            {
                signUpAndLogInState.text = "Успешно";
            }
        }
        if (registerResult == "ERROR")
        {
            if (signUpAndLogInState.text == "Обрабатывается...")
            {
                signUpAndLogInState.text = "Ошибка, попробуйте снова";
                Debug.Log("Handling register result");
            }
        }
    }

    public void handleGetPassedLevelsResult(string result)
    {
        Debug.Log($"Handling getPassedLevels result = {result}");
        string[] passedLevels = result.Split(",");
        foreach (string passedLevel in passedLevels)
        {
            DataStorer.saveLevelAsPast(passedLevel.Trim());
        }
    }
}
