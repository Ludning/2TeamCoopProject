using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WaveSystem : MonoBehaviour
{
    private int currentWaveIndex = -1;

    public Waves[] waves;

    public void StartWave()
    {
        if ( Timer.instance.currentTime == 0 && currentWaveIndex < waves.Length -1)
        {
            currentWaveIndex++;
            Debug.Log("�������̺�");
        }
    }
}

[System.Serializable]
public class Waves
{
    //���� Ÿ��
    public float spawnTime;
    public int spritType;
    public int hp;
    //public int maxhp;
    public float speed;
}

