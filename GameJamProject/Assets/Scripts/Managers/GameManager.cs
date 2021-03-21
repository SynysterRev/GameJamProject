using DG.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public delegate void DelegateScore(int score);
    public event DelegateScore OnUpdateScore;


    private float customDT = 1.0f;
    private static int score = 0;
    private static string[] highScoresName = new string[5];
    private static int[] highScores = new int[5];

    public float CustomDT { get => customDT; set => customDT = value; }
    public string[] HighScoreName { get => highScoresName; }
    public int[] HighScores { get => highScores; }

    private new void Awake()
    {
        for (int i = 0; i < highScores.Length; ++i)
        {
            highScores[i] = 0;
            highScoresName[i] = "-";
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                highScores[i] = PlayerPrefs.GetInt(i.ToString());
            }
            if (PlayerPrefs.HasKey("name"+ i.ToString()))
            {
                highScoresName[i] = PlayerPrefs.GetString("name" + i.ToString());
            }
        }
    }
    private void OnApplicationQuit()
    {
        for (int i = 0; i < highScores.Length; ++i)
        {
            if (highScores[i] > 0)
            {
                PlayerPrefs.SetInt(i.ToString(), highScores[i]);
                PlayerPrefs.GetString("name" + i.ToString(), highScoresName[i]);
            }
        }
    }
    private void Start()
    {
        if (OnUpdateScore != null)
            OnUpdateScore(score);
    }

    public void UpdateScore(int points)
    {
        score += points;
        if (OnUpdateScore != null)
            OnUpdateScore(score);
    }

    public void NextLevel()
    {
        if (LevelManager.Instance.IsLastLevel)
        {
            //go menu
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("Level" + (LevelManager.Instance.IndexLevel + 1));
        }
    }

    public void Retry()
    {
        score = 0;
        SceneManager.LoadScene("Level0");
    }
}
