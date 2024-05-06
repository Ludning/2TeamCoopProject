using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        titles[0].SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        titles[1].SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

}
