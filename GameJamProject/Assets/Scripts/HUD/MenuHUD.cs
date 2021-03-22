using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour
{
    [SerializeField]
    private Button play = null;
    [SerializeField]
    private Button survival = null;
    [SerializeField]
    private Button credit = null;
    [SerializeField]
    private Button quit = null;
    [SerializeField]
    private Text[] highScore = null;
    [SerializeField]
    private Text[] highScoreName = null;
    private GameManager gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        play.onClick.AddListener(Play);
        survival.onClick.AddListener(Survival);
        credit.onClick.AddListener(Credits);
        quit.onClick.AddListener(Quit);
        for (int i = 0; i < highScore.Length; ++i)
        {
            highScore[i].text = gameManager.HighScores[i].ToString();
            highScoreName[i].text = gameManager.HighScoreName[i];
        }
    }

    private void Play()
    {
        TransitionLevel.Instance.StartTransition("Level0");
    }
    private void Survival()
    {
        TransitionLevel.Instance.StartTransition("Survival");
    }
    private void Credits()
    {
        TransitionLevel.Instance.StartTransition("Credits");
    }
    private void Quit()
    {
        Application.Quit();
    }
}
