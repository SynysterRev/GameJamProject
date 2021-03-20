using DG.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void DelegateScore(int score);
    public event DelegateScore OnUpdateScore;


    private float customDT = 1.0f;
    private int score = 0;

    public float CustomDT { get => customDT; set => customDT = value; }

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
}
