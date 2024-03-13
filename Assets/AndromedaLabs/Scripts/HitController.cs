using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : Singleton<HitController>
{
    [SerializeField]
    private GameObject hitPrefab;

    private void Start()
    {
        GameEvents.Instance.triggerHitAnimation.AddListener(HandleHit);
    }

    private void HandleHit(Vector2 position)
    {
        GameObject hitIndicator = Instantiate(hitPrefab, position, Quaternion.identity);
        Destroy(hitIndicator, 0.3f);
    }

}
