using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Assets/Data/Wave")]
public class WaveTypeScriptable : ScriptableObject
{
    public List<WaveDifficulty> listTypeWave;

    [System.Serializable]
    public class WaveDifficulty
    {
        public List<Wave> listWaveDifficulty;
    }

    [System.Serializable]
    public class Wave
    {
        public int[] indexPositionEnemy;
    }
}
