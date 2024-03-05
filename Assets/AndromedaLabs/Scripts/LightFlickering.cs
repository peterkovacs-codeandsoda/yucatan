using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField]
    private float maxFlicker;
    private Light2D light;

    void Start()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(DoFlickering());
    }

    private IEnumerator DoFlickering()
    {
        while (true)
        {
            float flicker = Random.Range(0.3f, maxFlicker);
            light.intensity = flicker;
            float delay = Random.Range(0.2f, 0.5f);
            yield return new WaitForSeconds(delay);
        }
    }

}
