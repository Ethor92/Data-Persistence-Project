using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MenuUIManager : MonoBehaviour
{

    public string playerName;


    public void StartNew()
    {
        
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("please enter name");
        }
    }

    public void Exit()
    {
        
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    
    public void Name(string s)
    {
        playerName = s;
        GameManager.Instance.playerName = playerName;
        Debug.Log("signed in as " + playerName);
        
    }
}
