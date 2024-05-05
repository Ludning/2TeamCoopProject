using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI ammoText;
    //public TextMeshProUGUI mainammoText;
    //public TextMeshProUGUI subammoText;
    //public TextMeshProUGUI grenadeammoText;
    //public TextMeshProUGUI healammoText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public GameObject gameOverUI;
    public GameObject gamevictoryUI;
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

    public void GameReplay()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("���ӽ���");
    }
    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
