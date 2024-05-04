using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    //���� score
    private int score = 0;
    //���ӿ��� üũ
    public bool isGameOver { get; private set; }

    public void AddScore(int newScore)
    {
        score += newScore;
        UIManager.instance.UpdateScoreText(score);
    }
    public void EndGame()
    {
        isGameOver = true;
        UIManager.instance.SetActiveGameOverUI(true);
    }
}
