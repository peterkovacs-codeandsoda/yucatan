using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject nextStagePanel;

    [SerializeField]
    private string nextButtonTargetSceneName;

    [SerializeField]
    private GameObject restartGamePanel;

    [SerializeField]
    private Slider might;

    [SerializeField]
    private Slider bossHp;

    [SerializeField]
    private GameObject acerolaPanel;

    [SerializeField]
    private GameObject acerolaIcon;

    private void Start()
    {
        GameEvents.Instance.introTextIsOver.AddListener(DisplayNextStageButtonAfterTextDisplayed);
        GameEvents.Instance.hideNextButton.AddListener(HideNextButton);
        GameEvents.Instance.triggerStageCleared.AddListener(HandleStageCleared);
        GameEvents.Instance.loadNextScene.AddListener(LoadNextScene);
        GameEvents.Instance.triggerRestartGame.AddListener(HandleRestartGame);
        GameEvents.Instance.triggerMightChanged.AddListener(HandleMightChange);
        GameEvents.Instance.bossHpChanged.AddListener(HandleBossHpChange);
        GameEvents.Instance.openAcerolaPanel.AddListener(HandleAcerolaPanelOpen);
        GameEvents.Instance.activateAcerola.AddListener(HandleAcerolaActivation);

        if (PlayerPrefs.GetInt("acerola") == 1 && acerolaIcon != null)
        {
            acerolaIcon.SetActive(true);
        }
    }

    private void HandleAcerolaActivation()
    {
        acerolaIcon.SetActive(false);
    }

    private void HandleAcerolaPanelOpen()
    {
        acerolaPanel.SetActive(true);
        acerolaIcon.SetActive(true);
        StartCoroutine(CloseAcerolaPanel());
    }

    private IEnumerator CloseAcerolaPanel()
    {
        yield return new WaitForSeconds(2f);
        acerolaPanel.SetActive(false);
    }

    private void HandleBossHpChange(float hp)
    {
        bossHp.value = hp;
    }

    private void HandleMightChange(int mightValue)
    {
        might.value = mightValue;
    }

    private void HandleRestartGame()
    {
        restartGamePanel.SetActive(true);
        Time.timeScale = 0f;
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

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void LoadStartGameScene()
    {
        SceneManager.LoadScene("StartGameScene");
    }

    public void RestartGame()
    {
        LoadStartGameScene();
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
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
