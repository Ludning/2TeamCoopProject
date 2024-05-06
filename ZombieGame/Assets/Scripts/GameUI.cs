using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public void GameReplay()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
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
}
