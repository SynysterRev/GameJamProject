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
    private int[] highScores = new int[5];
    public float CustomDT { get => customDT; set => customDT = value; }

    private new void Awake()
    {

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
