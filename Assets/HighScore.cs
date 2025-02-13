using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    //unity singleton pattern
    private static HighScore instance;
    public static HighScore Instance
    { get
    {
        if (instance == null)
        {
            instance = FindObjectOfType<HighScore>();
        }
        return instance;
    } }

    public decimal GetHighScore()
    {
        if (!File.Exists(Application.persistentDataPath + "/highscore.txt"))
        {
            File.WriteAllText(Application.persistentDataPath + "/highscore.txt", "0");
        }

        var highScore = File.ReadLines(Application.persistentDataPath + "/highscore.txt").First();
        return decimal.TryParse(highScore, out var score)
            ? score
            : 0;
    }

    public void SetHighScore(decimal score)
    {
        if (score > GetHighScore())
        {
            File.WriteAllText(Application.persistentDataPath + "/highscore.txt", $"{score}");
        }
    }
}
