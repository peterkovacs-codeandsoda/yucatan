using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject nextStageButton;

    private void Start()
    {
        GameEvents.Instance.introTextIsOver.AddListener(DisplayNextStageButtonAfterTextDisplayed);
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
    #endregion

    #region Event Handling
    private void DisplayNextStageButtonAfterTextDisplayed()
    {
        nextStageButton.SetActive(true);
    }
    #endregion
}
