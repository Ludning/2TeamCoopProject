using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    int hp = 3;
    public void OnDamage()
    {
        hp -= 1;
        if (hp < 0)
            Destroy(gameObject);
    }
}
