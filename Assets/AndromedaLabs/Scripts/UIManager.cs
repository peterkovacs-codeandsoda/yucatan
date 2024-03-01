using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject nextStagePanel;

    [SerializeField]
    private string nextButtonTargetSceneName;

    private void Start()
    {
        GameEvents.Instance.introTextIsOver.AddListener(DisplayNextStageButtonAfterTextDisplayed);
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
    }

    private void HandleStageCleared()
    {
        nextStagePanel.SetActive(true);
    }


    #region Scene Loading
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void LoadFirstStageScene()
    {
        SceneManager.LoadScene("FirstStageScene");
    }

    public void LoadFirstStageStoryScene()
    {
        SceneManager.LoadScene("FirstStageStoryScene");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextButtonTargetSceneName);
    }
    #endregion

    #region Event Handling
    private void DisplayNextStageButtonAfterTextDisplayed()
    {
        nextStagePanel.SetActive(true);
    }
    #endregion
}
