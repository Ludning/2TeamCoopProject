using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;

    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // 게임 일시정지
        PauseUI.SetActive(true);
        Debug.Log("Paused");
    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // 게임 재개
        PauseUI.SetActive(false);
        Debug.Log("Resumed");
    }
}
