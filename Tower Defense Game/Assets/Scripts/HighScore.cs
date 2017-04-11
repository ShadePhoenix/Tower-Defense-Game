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

    //    highScore = new string[5];

    //    if (!File.Exists(filePath))
    //    {
    //        IniHighScore();
    //    }
    //    GetHighScore();
    //}

    //void IniHighScore()
    //{
    //    File.CreateText(filePath);
    //    Debug.Log("File Created");
    //}

    //public string[] temp = new string[5];
    //public string[] splitUp = new string[5];
    //void GetHighScore()
    //{
    //    int tempNum = 0;
    //    StreamReader reader = new StreamReader(filePath);
    //    foreach (string line in reader.ReadToEnd().Split('\n'))
    //    {
    //        if (tempNum != 6)
    //        {
    //            temp[tempNum] = line;
    //            tempNum++;
    //        }            
    //    }
    //    for (int i = 0; i < temp.Length; i++)
    //    {
    //        splitUp = temp[i].Split(',');
    //        int rank = int.Parse(splitUp[0]);
    //        string playerName = splitUp[1];
    //        int score = int.Parse(splitUp[2]);
    //        highScore[rank] = rank + " : " + playerName + " : " + score;
    //    }
    //    foreach (string HS in highScore)
    //    {
    //        highScoreTB.text += HS + "\n";
    //    }
    //}

    //void SaveHighScore()
    //{
    //    StreamWriter writer = new StreamWriter(filePath);
    //    writer.WriteLine();
    //}
}
