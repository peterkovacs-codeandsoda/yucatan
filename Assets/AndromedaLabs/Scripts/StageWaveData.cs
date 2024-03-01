using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stageData", menuName = "yucatan/stageData", order = 2)]
public class StageWaveData : ScriptableObject
{
    public List<WaveData> waves;

    [CreateAssetMenu(fileName = "waveData", menuName = "yucatan/waveData", order = 1)]
    public class WaveData : ScriptableObject
    {
        public int waveId;
        public int entityCount;
        public bool burstMode;
        public float internalDelay;
        public float internalDelayRandomMin;
        public float internalDelayRandomMax;
    }
}
