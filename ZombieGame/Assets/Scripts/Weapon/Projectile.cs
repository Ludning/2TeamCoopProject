using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour, IPoolable
{
    float speed;
    Vector3 direction;
    [SerializeField]
    Rigidbody rb;

    // Update is called once per frame
    /*void Update()
    {
        transform.Translate(direction * speed);
        
    }*/

    public void Shot(Transform transform, float speed)
    {
        this.transform.position = transform.localPosition;
        Debug.Log(this.transform.position);
        direction = transform.forward;
        this.speed = speed;

        rb.AddForce(direction * speed, ForceMode.Impulse);
        Invoke("DestroyObject", 5.0f);
    }

    public void DestroyObject()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
