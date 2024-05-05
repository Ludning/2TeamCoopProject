using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

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



    public TextMeshProUGUI ammoText; // TextMeshPro 사용
    public TMP_Text scoreText; // TextMeshPro 사용
    public TMP_Text waveText; // TextMeshPro 사용
    public GameObject gameOverUI;
    public Slider hpBar;
    public GameObject pauseUI;

    // 탄창 업데이트 
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // 점수 업데이트
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // 웨이브 텍스트 업데이트
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // 체력바 업데이트
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        hpBar.value = currentHp / maxHp;
    }

    // 게임오버UI 액티브
    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }

    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetActivePauseUI(bool isPause)
    {
        pauseUI.SetActive(isPause);
        Debug.Log("isPause true?");
    }

}
