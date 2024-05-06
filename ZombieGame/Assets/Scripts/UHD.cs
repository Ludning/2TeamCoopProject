using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;


public class UHD : MonoBehaviour
{
    public static UHD instance;

    [Header("#GAME CONTROL")]
    //public float gameTime; //���� ���ӽð�
    //public float maxGameTime = 2 * 10f; //�ִ���ӽð�
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
            case InfoType.WaveLevel: //Wave ���ѽð� (3��) ��Ƽ�� ���� Wave�� �Ѿ�� �� 5��������

                break;
            case InfoType.Inventory: //

                break;
            case InfoType.Time: //�� Wave�� ���ѽð� 3��, ��Ƽ�� Victory UI 3�ʵ��� ����� ���� Wave�� �Ѿ / ����Ƽ�� Lose UI

                break;
            case InfoType.Hp: //�� ���Ӵ� 100�� ü��. Wave Ŭ����� ü�� �ڵ� ȸ��
                float curHp = hp;
                float mexHp = maxhp;
                mySlider.value = curHp / mexHp;
                break;
            case InfoType.Kill: //

                break;
        }
    }

}
