using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] public float currentTime;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime < 0)
        {
            currentTime = 0;
            //다음웨이브 호출 함수
        }

        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}