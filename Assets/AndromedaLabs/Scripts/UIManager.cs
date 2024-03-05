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
        GameEvents.Instance.hideNextButton.AddListener(HideNextButton);
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
        GameEvents.Instance.loadNextScene.AddListener(LoadNextScene);
    }


    private void HideNextButton()
    {
        nextStagePanel.SetActive(false);
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

    public void LoadNextSpeechEntry()
    {
        GameEvents.Instance.triggerNextSpeechEntry.Invoke();
    }
    #endregion
}
