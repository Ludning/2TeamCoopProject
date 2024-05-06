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
    //public float gameTime; //���� ���ӽð�
    //public float maxGameTime = 2 * 10f; //�ִ���ӽð�
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
            case InfoType.WaveLevel: //Wave ���ѽð� (3��) ��Ƽ�� ���� Wave�� �Ѿ�� �� 5��������

                break;
            case InfoType.Inventory: //

                break;
            case InfoType.Time: //�� Wave�� ���ѽð� 3��, ��Ƽ�� Victory UI 3�ʵ��� ����� ���� Wave�� �Ѿ / ����Ƽ�� Lose UI
                float curTime = Timer.instance.currentTime;

                break;
            case InfoType.Hp: //�� ���Ӵ� 100�� ü��. Wave Ŭ����� ü�� �ڵ� ȸ��
                float curHp = hp;
                float mexHp = maxhp;
                mySlider.value = curHp / mexHp;
                break;
            case InfoType.Kill: //

                break;

            case InfoType.Score: //Test
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("���ھ� ��");
                }
                break;
        }
    }

}
