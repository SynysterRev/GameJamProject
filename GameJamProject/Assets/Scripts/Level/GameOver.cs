using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Button okButton = null;
    [SerializeField]
    private InputField pseudoInput = null;
    [SerializeField]
    private EndLevelHUD endLevelHUD = null;

    private void Start()
    {
        pseudoInput.interactable = false;
        okButton.interactable = false;
        pseudoInput.onValueChanged.AddListener(CheckInput);
        okButton.onClick.AddListener(Close);
    }
    private void Close()
    {
        GetComponent<Animator>().SetTrigger("Close");
    }
    private void EndAnimation()
    {
        pseudoInput.interactable = true;
    }
    private void EndAnim()
    {
        endLevelHUD.EnableButtons();
    }
    private void CheckInput(string text)
    {
        if(text.Length > 0)
        {
            okButton.interactable = true;
        }
        else if(text.Length == 0)
            okButton.interactable = false;
    }
}
