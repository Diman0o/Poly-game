using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;


// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class WebRequest
{
    public static string host = "http://localhost:8000/";
    public static IEnumerator ProcessRequest(string path, string[][] queryParams, Action<string> handleResult)
    {
        List<string> queryParamsPairs = new List<string>();
        foreach (string[] queryParamPair in queryParams)
        {
            Debug.Log($"queryParams: {queryParamPair}");

            string paramName = queryParamPair[0];
            string paramValue = queryParamPair[1];
            queryParamsPairs.Add(paramName + "=" + paramValue);
        }
        string queryParamsString = string.Join("&", queryParamsPairs);
        string delimiter = string.IsNullOrWhiteSpace(queryParamsString) ? "" : "?";
        string uri = host + path + delimiter + queryParamsString;
        Debug.Log(uri);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    handleResult(webRequest.downloadHandler.text.Trim('"'));
                    break;
            }
        }
    }
}