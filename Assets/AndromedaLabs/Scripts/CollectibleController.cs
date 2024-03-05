using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{

    [SerializeField]
    private GameObject goodCollectiblePrefab;

    [SerializeField]
    private GameObject badCollectiblePrefab;

    [SerializeField]
    private Transform collectibleParent;

    void Start()
    {
        GameEvents.Instance.spawnCollectible.AddListener(HandleCollectibleSpawn);    
    }

    private void HandleCollectibleSpawn(Vector2 position)
    {
        GameObject collectible = badCollectiblePrefab;
        Instantiate(collectible, position, Quaternion.identity, collectibleParent);
    }

}
