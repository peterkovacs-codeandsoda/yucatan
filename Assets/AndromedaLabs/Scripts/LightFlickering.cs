using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{

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
            float flicker = Random.Range(0.3f, 1f);
            light.intensity = flicker;
            float delay = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(delay);
        }
    }

}
