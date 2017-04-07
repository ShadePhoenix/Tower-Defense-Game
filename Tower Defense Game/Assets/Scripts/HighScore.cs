using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    //string fileLocation, fileName, filePath;

    //static public string[] highScore;

    //public Text highScoreTB;

    //void Start()
    //{
    //    fileLocation = Application.dataPath + "/";
    //    fileName = "HighScores.txt";
    //    filePath = fileLocation + fileName;
    //    PlayerPrefs.SetString("FilePath", filePath);

    //    if (!File.Exists(filePath))
    //    {
    //        IniHighScore();
    //    }
    //    print(filePath);
    //}

    //void IniHighScore()
    //{
    //    File.CreateText(filePath);
    //    Debug.Log("File Created");
    //}

    //void GetHighScore()
    //{
    //    StreamReader reader = new StreamReader(filePath);
    //    highScore = reader.ReadToEnd().Split(',');
    //    for (int i = 0; i < highScore.Length; i++)
    //    {
    //        int rank = int.Parse(highScore[i]);
    //        string playerName = highScore[i + 1];
    //        int score = int.Parse(highScore[i + 2]);
    //        highScoreTB.text += rank + playerName + score + "\n";
    //        i = i + 3;
    //    }
    //}

    //void SaveHighScore()
    //{
    //    StreamWriter writer = new StreamWriter(filePath);
    //    writer.WriteLine();
    //}
}
