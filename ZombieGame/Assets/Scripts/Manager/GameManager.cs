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
                //DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    [Header("#GAME CONTROL")]
    public float gameTime; //실제 게임시간
    public float maxGameTime = 2 * 10f; //최대게임시간
    public bool isLive;
    [Header("#PLAYER INFO")]
    public float hp;
    public float maxhp = 100;
    public int kill;
    public int Score;
    public int Wave;
    public int[] nextWave = { 20,20,20,20,20 }; //20초 나중에 수정 가능
    [Header("#GAME OBJECT")]
    public Result uiResult;

    private bool isPause;

    private int wave = 1;

    //게임 score
    private int score = 0;
    //게임오버 체크
    public bool isGameOver { get; private set; }


    public void UpdateAmmo(int magAmmo, int remainAmmo, int weaponSlotIndex)
    {
        UIManager.Instance.UpdateAmmoText(magAmmo, remainAmmo, weaponSlotIndex);
    }

    //점수 추가
    public void AddScore(int newScore)
    {
        score += newScore;
        UIManager.Instance.UpdateScoreText(score);

    }
    //웨이브 변경
    public void AddWave()
    {
        wave++;
        UIManager.Instance.UpdateWaveText(wave);
    }
    //게임오버
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

    //..테스트용

    void Update()
    {
        //Timer.instance.currentTime -= Time.deltaTime;
        if (Timer.instance.currentTime <= 0)
        {
            GameVictroy();
        }
    }
    public void GameStart()
    {
        UHD.instance.hp = UHD.instance.maxhp;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {

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
