using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsConfiguration : Singleton<OptionsConfiguration>
{
    private const string subtitleSpeedKey = "subtitleSpeed";
    private const string subtitleSeparatorSpeedKey = "separatorSpeed";
    private const string difficultyKey = "difficulty";
    [HideInInspector]
    public float subtitleSpeed;
    [HideInInspector]
    public float subtitleSeparatorSpeed;
    [HideInInspector]
    public bool easyDifficulty;

    protected override void Awake()
    {
        base.Awake();
        subtitleSpeed = PlayerPrefs.GetFloat(subtitleSpeedKey, 0.05f);
        subtitleSeparatorSpeed = PlayerPrefs.GetFloat(subtitleSeparatorSpeedKey, 0.35f);
        easyDifficulty = PlayerPrefs.GetInt(difficultyKey, 1) == 1; 
    }

    #region Option Setters
    public void UseSlowSubtitleSpeed()
    {
        SetupSubtitleSpeed(0.1f, 0.7f);
    }

    public void UseFastSubtitleSpeed()
    {
        SetupSubtitleSpeed(0.05f, 0.35f);
    }

    public void UseInstantSubtitles()
    {
        SetupSubtitleSpeed(0f, 0f);
    }

    private void SetupSubtitleSpeed(float subtitleSpeed, float separatorSpeed)
    {
        this.subtitleSpeed = subtitleSpeed;
        this.subtitleSeparatorSpeed = separatorSpeed;
        PlayerPrefs.SetFloat(subtitleSpeedKey, subtitleSpeed);
        PlayerPrefs.SetFloat(subtitleSeparatorSpeedKey, separatorSpeed);
        PlayerPrefs.Save();
    }

    public void UseEasyDifficulty()
    {
        SetupDifficulty(true);
    }

    public void UseHardDifficulty()
    {
        SetupDifficulty(false);
    }

    private void SetupDifficulty(bool easyDifficulty)
    {
        this.easyDifficulty = easyDifficulty;
        PlayerPrefs.SetInt(difficultyKey, easyDifficulty ? 1 : 0);
        PlayerPrefs.Save();
    }
    #endregion

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(difficultyKey);
        PlayerPrefs.Save();
    }
}
