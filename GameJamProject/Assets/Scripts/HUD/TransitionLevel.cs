using UnityEngine;
using System.Collections;
using DG.Util;
using UnityEngine.SceneManagement;

public class TransitionLevel : Singleton<TransitionLevel>
{

    #region Public Fields
    #endregion


    #region Private Fields
    private int sceneBuildIndex = -1;
    private string sceneName = "";

    #endregion


    #region Accessors
    public int SceneBuildIndex { get => sceneBuildIndex; set => sceneBuildIndex = value; }
    public string SceneName { get => sceneName; set => sceneName = value; }
    #endregion


    #region MonoBehaviour Methods

    private new void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion


    #region Private Methods
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }
    #endregion

    #region Public Methods

    public void StartTransition(int _sceneBuildIndex)
    {
        sceneBuildIndex = _sceneBuildIndex;
        ChangeLevel();
    }

    public void StartTransition(string _sceneName)
    {
        sceneName = _sceneName;
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        if (sceneBuildIndex != -1)
        {
            SceneManager.LoadSceneAsync(sceneBuildIndex);
        }
        if (SceneName != "")
        {
            SceneManager.LoadSceneAsync(SceneName);
        }

        sceneBuildIndex = -1;
        SceneName = "";
    }
    #endregion


}
