using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : Singleton<EnemySpawnController>
{
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    [SerializeField]
    private StageWaveData stageWaveSetup;

    private float maxDelay = 10f;
    private int maxNumberOfEnemiesInWave = 10;

    private bool spawnInProgress;
    private int waveCount = 1;


    private void Start()
    {
        //GameEvents.Instance.triggerEnemySpawning.AddListener(HandleEnemySpawning);
        HandleEnemySpawning();
    }
    public void HandleEnemySpawning()
    {
        spawnInProgress = true;
        StartCoroutine(SpawnEnemies());
        GameEvents.Instance.triggerEnemyEliminated.AddListener(HandleEnemyEliminated);
    }

    public void HandleEnemyEliminated()
    {
        if (enemyParent.childCount == 0 && CheckStageDeployed())
        {
            //print("all enemies eliminated");
            GameEvents.Instance.triggerStageCleared.Invoke();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnInProgress)
        {
            InitializeByWaveData(waveCount);
            float delay = SpawnBasicMode();
            waveCount++;
            yield return new WaitForSeconds(delay);
            if (CheckStageDeployed())
            {
                //print("stage deployed");
                spawnInProgress = false;
            }
        }
       
    }

    private bool CheckStageDeployed()
    {
        return waveCount == stageWaveSetup.waves.Count;
    }

    private void InitializeByWaveData(int waveId)
    {
        StageWaveData.WaveData waveData = stageWaveSetup.waves[waveId - 1];
        maxNumberOfEnemiesInWave = waveData.entityCount;
        maxDelay = CalculateMaxDelay(waveData);
    }

    private float CalculateMaxDelay(StageWaveData.WaveData waveData)
    {
        float result;
        if (waveData.internalDelay > 0f)
        {
            result = waveData.internalDelay;
        }
        else
        {
            result = Random.Range(waveData.internalDelayRandomMin, waveData.internalDelayRandomMax);
        }
        return result;
    }

    private float SpawnBasicMode()
    {
        int prefabCount = Random.Range(maxNumberOfEnemiesInWave / 2, maxNumberOfEnemiesInWave + 1);
        float delay = Random.Range(maxDelay / 2, maxDelay);
        for (int i = 0; i < prefabCount; i++)
        {
            int enemyPrefabIndex = CalculatePrefabIndex();
            Vector2 position = GameAreaBoundController.Instance.RandomPositionInBound();
            Instantiate(enemyPrefabs[enemyPrefabIndex], position, Quaternion.identity, enemyParent);
        }

        return delay;
    }

    private int CalculatePrefabIndex()
    {
        return Random.Range(0, enemyPrefabs.Count);
    }
}
