using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stageData", menuName = "yucatan/stageData", order = 2)]
public class StageWaveData : ScriptableObject
{
    public List<WaveData> waves;

}
