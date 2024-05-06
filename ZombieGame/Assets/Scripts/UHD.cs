using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;


public class UHD : MonoBehaviour
{
    public static UHD instance;

    [Header("#GAME CONTROL")]
    //public float gameTime; //실제 게임시간
    //public float maxGameTime = 2 * 10f; //최대게임시간
    public bool isLive;
    [Header("#PLAYER INFO")]
    public float hp;
    public float maxhp = 100;
    public int Wavelevel;
    public int kill;
    [Header("#GAME OBJECT")]
    public Result uiResult;


    public enum InfoType { WaveLevel, Inventory, Time, Hp, Kill }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        instance = this;
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.WaveLevel: //Wave 제한시간 (3분) 버티면 다음 Wave로 넘어가기 총 5스테이지

                break;
            case InfoType.Inventory: //

                break;
            case InfoType.Time: //한 Wave당 제한시간 3분, 버티면 Victory UI 3초동안 띄운후 다음 Wave로 넘어감 / 못버티면 Lose UI

                break;
            case InfoType.Hp: //한 게임당 100의 체력. Wave 클리어시 체력 자동 회복
                float curHp = hp;
                float mexHp = maxhp;
                mySlider.value = curHp / mexHp;
                break;
            case InfoType.Kill: //

                break;
        }
    }

}
