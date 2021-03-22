using DG.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private int indexLevel = 0;
    [SerializeField]
    private bool isLastLevel = false;
    [SerializeField]
    private EndLevelHUD endLevel = null;
    [SerializeField]
    private SpawnerEnemy spawnerEnemy = null;

    public int IndexLevel { get => indexLevel; }
    public bool IsLastLevel { get => isLastLevel; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEndLevel()
    {
        endLevel.transform.parent.gameObject.SetActive(true);
        endLevel.ShowEndLevel(false, false);
    }

    public void ShowGameOver()
    {
        spawnerEnemy.StopSpawner(true);
        endLevel.transform.parent.gameObject.SetActive(true);
        endLevel.ShowEndLevel(true, GameManager.Instance.IsNewHighscore());
    }

    public void StartLevel()
    {
        spawnerEnemy.StartSpawner();
    }
}
