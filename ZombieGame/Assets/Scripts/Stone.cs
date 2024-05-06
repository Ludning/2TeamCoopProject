using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private float damage = 30f;

    private void Start()
    {
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DamageMessage damageMessage = new DamageMessage();
            damageMessage.damage = damage;
            other.gameObject.GetComponent<LivingEntity>().ApplyDamage(damageMessage);
            gameObject.SetActive(false);
        }
    }
}
