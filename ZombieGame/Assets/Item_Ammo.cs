using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ammo : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log($"OnCollisionEnter! : {collision.gameObject.name}");
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Add Ammo!");
            collision.transform.GetComponent<WeaponController>().AddAmmo();
            PoolManager.Instance.ReturnToPool(gameObject);
        }
    }
}
