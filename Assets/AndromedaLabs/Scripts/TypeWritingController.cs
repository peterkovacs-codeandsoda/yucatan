using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWritingController : MonoBehaviour
{
    [SerializeField]
    private float typingDelay = 0.1f;
    private float originalTypingDelay;
    [SerializeField]
    private TextMeshProUGUI targetUIText;
    [TextArea(3,20)]
    [SerializeField]
    private string targetText;

    private int visibleCharacterCount = 0;
    private Coroutine typingCoroutine;
    void Start()
    {
        typingCoroutine = StartCoroutine(Typing());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(typingCoroutine);
            targetUIText.text = targetText;
            GameEvents.Instance.introTextIsOver.Invoke();
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
