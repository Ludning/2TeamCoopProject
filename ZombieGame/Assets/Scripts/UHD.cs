using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UHD : MonoBehaviour
{
    public static UHD instance;
    [Header("#GAME CONTROL")]
    public float gameTime; //실제 게임시간
    public float maxGameTime = 2 * 10f; //최대게임시간

    void Awake()
    {
        //인스턴스 초기화
        instance = this;
    }

}
