using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
public class GameManager : MonoBehaviour
{//score , pause , cashing , spawn(objectpool)

    public static GameManager instance;

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

    //���� score
    private int score = 0;
    //���ӿ��� üũ
    public bool isGameOver { get; private set; }


    public void UpdateAmmo(int magAmmo, int remainAmmo)
    {
        UIManager.Instance.UpdateAmmoText(magAmmo, remainAmmo);
    }

    //���� �߰�

    public void AddScore(int newScore)
    {
        score += newScore;
        UIManager.Instance.UpdateScoreText(score);
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

    //..�׽�Ʈ��

    void Update()
    {
        //Timer.instance.currentTime -= Time.deltaTime;
        if (Timer.instance.currentTime <= 0)
        {
            GameVictroy();
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        //isLive = false; �׾�����

        yield return new WaitForSeconds(0.5f);

        UHD.instance.uiResult.gameObject.SetActive(true);
        UHD.instance.uiResult.Lose();
        Stop();
    }

    public void GameVictroy()
    {
        StartCoroutine(GameVictroyRoutine());
    }

    IEnumerator GameVictroyRoutine()
    {
        //isLive = false;

        yield return new WaitForSeconds(0.5f);

        UHD.instance.uiResult.gameObject.SetActive(true);
        UHD.instance.uiResult.Win();
        Stop();
    }

    public void Stop()
    {
        //isLive = false;
        Time.timeScale = 0;
    }
}
