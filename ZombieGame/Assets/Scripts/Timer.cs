using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] float currentTime;

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if( currentTime < 0)
        {
            currentTime = 0;
            //�������̺� ȣ�� �Լ�
        }

        int min = Mathf.FloorToInt(currentTime / 60);
        int sec = Mathf.FloorToInt(currentTime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
