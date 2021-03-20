using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelHUD : MonoBehaviour
{
    [SerializeField]
    private Button menuButton = null;
    [SerializeField]
    private Button nextTryagainButton = null;
    [SerializeField]
    private Text nextTryagainText = null;
    private Animator anim = null;
    private void Awake()
    {
        menuButton.interactable = false;
        nextTryagainButton.interactable = false;
        anim = GetComponent<Animator>();
    }

    public void ShowEndLevel(bool isGameOver)
    {
        anim.SetTrigger("Start");
        menuButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        if (isGameOver)
        {
            nextTryagainText.text = "Retry";
            nextTryagainButton.onClick.AddListener(GameManager.Instance.Retry);
        }
        else
        {
            nextTryagainText.text = "Next";
            nextTryagainButton.onClick.AddListener(GameManager.Instance.NextLevel);
        }
    }

    private void EndAnimation()
    {
        menuButton.interactable = true;
        nextTryagainButton.interactable = true;
    }
}
