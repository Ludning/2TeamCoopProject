using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject ui = GameObject.Find("UIManager");
                if (ui == null)
                {
                    ui = new GameObject("UIManager");
                }
                instance = ui.GetComponent<UIManager>();
                if (instance == null)
                {
                    instance = ui.AddComponent<UIManager>();
                }
                DontDestroyOnLoad(ui);
            }
            return instance;
        }
    }
    private static UIManager m_instance;
    public Text ammoText;
    public Text scoreText;
    public Text waveText;
    public GameObject gameOverUI;
    public GameObject hpBar;

    //탄창 업데이트 
    public void UpdateAmmoText(int magAmmo,int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }
    //점수 업데이트
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }
    //웨이브 텍스트 업데이트
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }
    public void UpdateHpBar(int hpBar)
    {
        
    }
    //게임오버UI 액티브
    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }
    //게임 재시작 
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
