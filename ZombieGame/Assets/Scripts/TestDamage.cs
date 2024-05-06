using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    public GameObject player;
    LivingEntity playerHealth;
    DamageMessage damageMessage;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<LivingEntity>();
        damageMessage = new DamageMessage();
        damageMessage.damage = 50;

        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            playerHealth.ApplyDamage(damageMessage);
        }
    }

}