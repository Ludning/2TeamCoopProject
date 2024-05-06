using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour, IPoolable
{
    float speed;
    Vector3 direction;
    [SerializeField]
    Rigidbody rb;

    Vector3 prevPosition;

    // Update is called once per frame
    public virtual void FixedUpdate()
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

    public void Shot(Transform firePosition, float speed)
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        this.transform.position = firePosition.position;
        this.transform.rotation = firePosition.rotation;
        direction = firePosition.forward;// firePosition.localRotation * firePosition.forward;//firePosition.localRotation * Vector3.forward; ;//firePosition.forward * firePosition.localRotation;
        this.speed = speed;

        //이전 위치 기록
        prevPosition = this.transform.position;

        rb.AddForce(direction * speed, ForceMode.Impulse);
        Invoke("DestroyObject", 5.0f);
    }

    public void DestroyObject()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();
        rb.ResetCenterOfMass();

        PoolManager.Instance.ReturnToPool(gameObject);
    }

}
