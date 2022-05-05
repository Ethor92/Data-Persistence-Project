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

    public static MenuUIManager Instance;
    public string playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

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
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveNameEntered()
    {
        MenuUIManager.Instance.SaveName(playerName);
    }

    public void LoadNameEntered()
    {
        //MainManager.Instance.LoadColor();
        //ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;

    }

    
    public void SaveName(string s)
    {
        playerName = s;
        Debug.Log(playerName);

        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }
}
