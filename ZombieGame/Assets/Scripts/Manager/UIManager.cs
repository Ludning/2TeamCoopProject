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



    public TextMeshProUGUI ammoText; // TextMeshPro ���
    public TMP_Text scoreText; // TextMeshPro ���
    public TMP_Text waveText; // TextMeshPro ���
    public GameObject gameOverUI;
    public Slider hpBar;
    public GameObject pauseUI;

    // źâ ������Ʈ 
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    // ���� ������Ʈ
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    // ���̺� �ؽ�Ʈ ������Ʈ
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    // ü�¹� ������Ʈ
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        hpBar.value = currentHp / maxHp;
    }

    // ���ӿ���UI ��Ƽ��
    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }

    // ���� �����
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
