using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UHD : MonoBehaviour
{
    public static UHD instance;
    [Header("#GAME CONTROL")]
    public float gameTime; //���� ���ӽð�
    public float maxGameTime = 2 * 10f; //�ִ���ӽð�

    void Awake()
    {
        //�ν��Ͻ� �ʱ�ȭ
        instance = this;
    }

}
