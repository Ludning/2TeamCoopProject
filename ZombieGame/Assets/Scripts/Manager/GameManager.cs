using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{//score , pause , cashing , spawn(objectpool)

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                if (go == null)
                {
                    go = new GameObject("GameManager");
                }
                instance = go.GetComponent<GameManager>();
                if (instance == null)
                {
                    instance = go.AddComponent<GameManager>();
                }
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private bool isPause;

    private int wave = 1;

    //���� score
    private int score = 0;
    //���ӿ��� üũ
    public bool isGameOver { get; private set; }


    public void UpdateAmmo(int magAmmo, int remainAmmo, int weaponSlotIndex)
    {
        UIManager.Instance.UpdateAmmoText(magAmmo, remainAmmo, weaponSlotIndex);
    }


    //���� �߰�
    public void AddScore(int newScore)
    {
        score += newScore;
        UIManager.Instance.UpdateScoreText(score);
    }
    //���̺� ����
    public void AddWave()
    {
        wave++;
        UIManager.Instance.UpdateWaveText(wave);
    }
    //���ӿ���
    public void EndGame()
    {
        isGameOver = true;
        UIManager.Instance.SetActiveGameOverUI(true);
    }

    public void Pause()
    {
        isPause = !isPause;
        Time.timeScale = isPause ? 0 : 1;
        UIManager.Instance.SetActivePauseUI(isPause);
        Debug.Log("pause?");
    }
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        UIManager.Instance.UpdateHpBar(currentHealth, maxHealth);

    }

}
