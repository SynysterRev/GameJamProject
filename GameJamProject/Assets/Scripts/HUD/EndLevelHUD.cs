using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelHUD : MonoBehaviour
{
    [SerializeField]
    private Text score = null;
    [SerializeField]
    private Button menuButton = null;
    [SerializeField]
    private Button nextTryagainButton = null;
    [SerializeField]
    private Text nextTryagainText = null;
    [SerializeField]
    private Animator animNewScore = null;
    [SerializeField]
    private InputField pseudo = null;
    private Animator anim = null;
    private bool isNewHighscore = false;
    private bool isGameOver = false;
    private void Awake()
    {
        menuButton.interactable = false;
        nextTryagainButton.interactable = false;
        anim = GetComponent<Animator>();
    }

    public void ShowEndLevel(bool isGameOver, bool isNewHighscore)
    {
        this.isNewHighscore = isNewHighscore;
        this.isGameOver = isGameOver;
        anim.SetTrigger("Start");
        score.text = "Score : " + GameManager.Instance.Score.ToString();
        menuButton.onClick.AddListener(() => GameManager.Instance.GoMenu(pseudo.text));
        if (isGameOver)
        {
            nextTryagainText.text = "Retry";
            nextTryagainButton.onClick.AddListener(() => GameManager.Instance.Retry(pseudo.text));
        }
        else
        {
            nextTryagainText.text = "Next";
            nextTryagainButton.onClick.AddListener(GameManager.Instance.NextLevel);
        }
    }

    private void EndAnimation()
    {
        if (!isNewHighscore)
        {
            menuButton.interactable = true;
            nextTryagainButton.interactable = true;
        }
        else if(isGameOver && isNewHighscore)
        {
            animNewScore.SetTrigger("Start");
        }
    }

    public void EnableButtons()
    {
        menuButton.interactable = true;
        nextTryagainButton.interactable = true;
    }
}
