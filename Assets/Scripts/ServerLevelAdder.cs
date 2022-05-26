using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;

public class ServerLevelAdder : MonoBehaviour
{
    public static string authRequiredText = "Требуется авторизация!";
    public Text status;
    public Text filepathInfo;
    public void AddLevelToServer()
    {
        if (!isAuthed())
        {
            return;
        }

        string filepath = EditorUtility.OpenFilePanel("Choose file to load on server", "", "txt");
        filepathInfo.text = $"{filepath}";
        Debug.Log(filepath);
        if (string.IsNullOrWhiteSpace(filepath))
        {
            Debug.Log("Invalid filepath");
            return;
        }

        StreamReader reader = new StreamReader(filepath);
        string data = reader.ReadToEnd();
        reader.Close();

        string[][] queryParams = new string[][] { new string[] { "accountId", DataStorer.accountId }, new string[] { "levelData", data} };
        StartCoroutine(WebRequest.ProcessRequest("add_level", queryParams, handleResult));
        status.text = "Обрабатывается...";
    }

    private void Start()
    {
        filepathInfo.text = "";
        isAuthed();
    }

    private void Update()
    {
        if (status.text == authRequiredText && !string.IsNullOrWhiteSpace(DataStorer.accountId))
        {
            status.text = "";
        }
        
    }

    public bool isAuthed()
    {
        if (string.IsNullOrEmpty(DataStorer.accountId))
        {
            status.text = authRequiredText;
            return false;
        }
        status.text = "";
        return true;
    }

    void handleResult(string result)
    {
        Debug.Log(result);
        if (result == "OK")
        {
            status.text = "Уровень успешно добавлен!";
        }
        else if (result == "ERROR:NOT_AUTHED")
        {
            status.text = "Для загрузки уровня требуется авторизация!";
        }
        else if (result == "ERROR:LEVEL_INVALID")
        {
            status.text = "Неверные данные в файле!";
        }
        else
        {
            status.text = "Неизвестная ошибка, попробуйте снова!";
        }
    }
}
