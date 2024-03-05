using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class TypeWritingController : MonoBehaviour
{
    [SerializeField]
    private float typingDelay = 0.1f;
    private float originalTypingDelay;
    [SerializeField]
    private TextMeshProUGUI targetUIText;

    private string targetText;

    [SerializeField]
    [TextArea(3,20)]
    private List<string> speechEntries;
    private int speechEntryIndex;

    private int visibleCharacterCount = 0;
    private Coroutine typingCoroutine;
    private bool nextStageDisplayed = false;
    void Start()
    {
        //typingCoroutine = StartCoroutine(Typing());
        HandleNextSpeechEntry();
        GameEvents.Instance.triggerNextSpeechEntry.AddListener(HandleNextSpeechEntry);
    }

    private void HandleNextSpeechEntry()
    {
        if (speechEntryIndex < speechEntries.Count)
        {
            targetText = speechEntries[speechEntryIndex++];
            visibleCharacterCount = 0;
            typingCoroutine = StartCoroutine(Typing());
            nextStageDisplayed = false;
        }
        else
        {
            GameEvents.Instance.loadNextScene.Invoke();
        }
    }

    private void Update()
    {
        if (!nextStageDisplayed && Input.GetMouseButtonDown(0))
        {
            StopCoroutine(typingCoroutine);
            targetUIText.text = targetText;
            GameEvents.Instance.introTextIsOver.Invoke();
            nextStageDisplayed = true;
        }
    }

    private IEnumerator Typing()
    {
        while (visibleCharacterCount <= targetText.Length)
        {
            string currentText = targetText.Substring(0, visibleCharacterCount);
            targetUIText.text = currentText;
            originalTypingDelay = typingDelay;
            if (currentText.EndsWith('.') || currentText.EndsWith('!') || currentText.EndsWith('?'))
            {
                typingDelay = 0.7f;
            }
            yield return new WaitForSeconds(typingDelay);
            typingDelay = originalTypingDelay;
            visibleCharacterCount++;
        }
        GameEvents.Instance.introTextIsOver.Invoke();
    }

}
