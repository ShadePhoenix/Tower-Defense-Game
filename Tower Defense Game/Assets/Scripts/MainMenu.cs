using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Scene gameScene;

    string fileLocation;
    string fileName;

    // Use this for initialization
    void Start ()
    {
        if (PlayerPrefs.GetInt("StartUp") != 1)
        {
            PlayerPrefs.SetInt("StartUp", 1);
            IniHighScore();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void IniHighScore()
    {
        // Where we want to save and load to and from
        fileLocation = Application.dataPath + "/";
        fileName = "SaveData.xml";
        CreateXML();
    }
    void CreateXML()
    {
        StreamWriter writer;
        FileInfo t = new FileInfo(fileLocation + "/" + fileName);
        writer = t.CreateText();
        Debug.Log("File Created");
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    void Exit()
    {
        Application.Quit();
    }
}
