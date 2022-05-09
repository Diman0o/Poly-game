using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountData : MonoBehaviour
{

    public static string accountId;

    public static void setAccountId(string newAccountId)
    {
        accountId = newAccountId;
        Debug.Log($"Set accountId to {accountId}");
    }
}
