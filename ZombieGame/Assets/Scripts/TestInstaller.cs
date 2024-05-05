using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameManager.Instance.gameObject;
        GameObject ui = UIManager.Instance.gameObject;
    }

}
