using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreSystem : MonoBehaviour
{
    string path;
    BinaryFormatter bf = new BinaryFormatter();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath + "/data.data";
        if (!File.Exists(path))
        {
            Data data = new Data(0);
            FileStream fs = new FileStream(path, FileMode.Create);
            bf.Serialize(fs, data);
            fs.Close();
        }
    }

    public void SaveScore(int score)
    {
        Data data = new Data(score);
        FileStream fs = new FileStream(path, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
    }

    public int LoadScore()
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        Data data = bf.Deserialize(fs) as Data;
        fs.Close();
        return data.highScore;
    }
}

[Serializable]
public class Data
{
    public int highScore;

    public Data(int hs)
    {
        highScore = hs;
    }
}
