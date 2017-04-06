using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    string fileLocation;
    string fileName;
    string filePath;

    // Use this for initialization
    void Start ()
    {
        //fileLocation = Application.dataPath + "/";
        //fileName = "HighScores.txt";
        //filePath = fileLocation + fileName;
        //PlayerPrefs.SetString("FilePath", filePath);

        //if (!File.Exists(filePath))
        //{
        //    IniHighScore();
        //}
        //print(filePath);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    //void IniHighScore()
    //{
    //    File.CreateText(filePath);
    //    //FileInfo t = new FileInfo(fileLocation + "/" + fileName);
    //    Debug.Log("File Created");
    //}

    //void GetHighScore()
    //{
        
    //}

    public void StartGame(string playScene)
    {
        SceneManager.LoadScene(playScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
