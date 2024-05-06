using System.Collections;
using System.Collections.Generic;
using TMPro;
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
<<<<<<< Updated upstream
    public int Wavelevel;
    public int kill;
    [Header("#GAME OBJECT")]
    public Result uiResult;

=======
    public int Score;
    public int Wave;
    public int[] NextWave = { 20, 20, 20, 20, 20 };
    [Header("#GAME OBJECT")]
    public Result uiResult;
>>>>>>> Stashed changes


    public enum InfoType { WaveLevel, Inventory, Time, Hp, Kill, Score }
    public InfoType type;

    TextMeshProUGUI myText;
    Slider mySlider;


    void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
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
                float curTime = Timer.instance.currentTime;

                break;
            case InfoType.Hp: //한 게임당 100의 체력. Wave 클리어시 체력 자동 회복
                float curHp = hp;
                float mexHp = maxhp;
                mySlider.value = curHp / mexHp;
                break;
            case InfoType.Kill: //

                break;

            case InfoType.Score: //Test
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("스코어 업");
                }
                break;
        }
    }

}
