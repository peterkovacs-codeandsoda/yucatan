using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : Singleton<EnemySpawnController>
{
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private List<GameObject> enemyPrefabs;

    private readonly float maxDelay = 10f;
    private readonly int maxNumberOfEnemiesInWave = 10;

    private void Start()
    {
        //GameEvents.Instance.triggerEnemySpawning.AddListener(HandleEnemySpawning);
        HandleEnemySpawning();
    }
    public void HandleEnemySpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            int prefabCount = Random.Range(maxNumberOfEnemiesInWave / 2, maxNumberOfEnemiesInWave + 1);
            float delay = Random.Range(maxDelay / 2, maxDelay);
            for (int i = 0; i < prefabCount; i++)
            {
                int enemyPrefabIndex = CalculatePrefabIndex();
                Vector2 position = GameAreaBoundController.Instance.RandomPositionInBound();
                Instantiate(enemyPrefabs[enemyPrefabIndex], position, Quaternion.identity, enemyParent);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private int CalculatePrefabIndex()
    {
        return Random.Range(0, enemyPrefabs.Count);
    }
}
