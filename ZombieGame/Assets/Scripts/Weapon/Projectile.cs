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

    Vector3 prevPosition;

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 normalize = (transform.position - prevPosition).normalized;

        transform.LookAt(transform.position + normalize);

        float distance = (transform.position - prevPosition).magnitude;//speed * Time.fixedDeltaTime;
        if (Physics.Raycast(prevPosition, transform.forward, out hit, distance))
        {
            // 충돌 처리 로직
            Debug.Log("Hit " + hit.collider.gameObject.name);
            //if (hit.transform.CompareTag("Monster"))
            //    hit.transform.GetComponent<Idamageable>().OnDamage();
            DestroyObject();
        }
        prevPosition = transform.position;
    }

    public void Shot(Transform transform, float speed)
    {
        this.transform.position = transform.position;
        direction = transform.forward;
        this.speed = speed;

        //이전 위치 기록
        prevPosition = transform.position;

        rb.AddForce(direction * speed, ForceMode.Impulse);
        Invoke("DestroyObject", 5.0f);
    }

    public void DestroyObject()
    {
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
