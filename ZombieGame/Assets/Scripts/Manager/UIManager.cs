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

    //źâ ������Ʈ 
    public void UpdateAmmoText(int magAmmo,int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }
    //���� ������Ʈ
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }
    //���̺� �ؽ�Ʈ ������Ʈ
    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }
    public void UpdateHpBar(int hpBar)
    {
        
    }
    //���ӿ���UI ��Ƽ��
    public void SetActiveGameOverUI(bool active)
    {
        gameOverUI.SetActive(active);
    }
    //���� ����� 
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
