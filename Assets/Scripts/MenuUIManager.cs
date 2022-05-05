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
    

    private MainManager mainManager;

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

    private void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
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
        //MenuUIManager.Instance.SavedData(playerName, endScore);
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
        public int endScore;
    }

    
    public void SavedData()
    {
        
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.endScore = mainManager.endScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            mainManager.endScore = data.endScore;
            Debug.Log("data score is: " + data.endScore);

        }
    }

    public void Name(string s)
    {
        playerName = s;
        Debug.Log(playerName);
    }
}
