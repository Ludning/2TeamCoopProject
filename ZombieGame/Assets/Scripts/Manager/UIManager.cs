using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI weapon1AmmoText; // TextMeshPro 사용
    public TextMeshProUGUI weapon2AmmoText; // TextMeshPro 사용
    public TextMeshProUGUI weapon3AmmoText; // TextMeshPro 사용
    public TextMeshProUGUI weapon4AmmoText; // TextMeshPro 사용
    public TMP_Text scoreText; // TextMeshPro 사용
    public TMP_Text waveText; // TextMeshPro 사용
    public GameObject gameOverUI;
    public Slider hpBar;
    public GameObject pauseUI;
    public GameObject GameReplayUI;

    public void ShowReplayUI()
    {
        GameReplayUI.SetActive(true);
    }
    
    // 탄창 업데이트 
    public void UpdateAmmoText(int magAmmo, int remainAmmo, int weaponSlotIndex)
    {
        switch(weaponSlotIndex)
        {
            case 0:
                weapon1AmmoText.text = magAmmo + "/" + remainAmmo;
                break;
            case 1:
                weapon2AmmoText.text = magAmmo + "/" + remainAmmo;
                break;
            case 2:
                weapon3AmmoText.text = magAmmo + "/" + remainAmmo;
                break;
            case 3:
                weapon4AmmoText.text = magAmmo + "/" + remainAmmo;
                break;
        }
        
    }

    // 점수 업데이트
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // 웨이브 텍스트 업데이트
    public void UpdateWaveText(int waves)
    {
        waveText.text = "Wave : " + waves;
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

////수정해야하나?
    public void GameReplay()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("게임시작");
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    ////
    public void SetActivePauseUI(bool isPause)
    {
        pauseUI.SetActive(isPause);
        Debug.Log("isPause true?");
    }

}
