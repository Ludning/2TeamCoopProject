using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour, IPoolable
{
    Vector3 direction;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    TrailRenderer trailRenderer;

    Vector3 prevPosition;

    float damage;

    [SerializeField]
    GameObject hitEnvirPaticle;
    [SerializeField]
    GameObject hitEnemyPaticle;

    // Update is called once per frame
    public virtual void Update()
    {
        RaycastHit hit;
        Vector3 normalize = (transform.position - prevPosition).normalized;

        transform.LookAt(transform.position + normalize);

        Vector3 direction = transform.position - prevPosition;
        //float distance = (transform.position - prevPosition).magnitude;
        if (Physics.Raycast(prevPosition, direction.normalized, out hit, direction.magnitude))
        {
            // �浹 ó�� ����
            Debug.Log("Hit " + hit.collider.gameObject.name);
            HitObject(hit);
            DestroyObject();
        }
        prevPosition = transform.position;
    }
    protected virtual void HitObject(RaycastHit hit)
    {
        GameObject hitPaticle;
        if (hit.transform.CompareTag("Monster"))
        {
            hitPaticle = PoolManager.Instance.GetGameObject(hitEnemyPaticle);
            DamageMessage damageMessage = new DamageMessage();
            damageMessage.damage = damage;
            hit.transform.GetComponent<IDamageable>().ApplyDamage(damageMessage);
        }
        else
        {
            hitPaticle = PoolManager.Instance.GetGameObject(hitEnvirPaticle);
        }
        
        Vector3 hitNormal = hit.normal;
        Debug.Log(hitNormal);

        hitPaticle.transform.position = hit.point + hitNormal * 0.01f;
        hitPaticle.transform.rotation = Quaternion.LookRotation(hitNormal);
    }

    public void Shot(Transform firePosition, float damage, float speed)
    {
        this.damage = damage;
        rb.isKinematic = true;
        rb.isKinematic = false;
        this.transform.position = firePosition.position;
        this.transform.rotation = firePosition.rotation;
        direction = firePosition.forward;

        //���� ��ġ ���
        prevPosition = this.transform.position;

        if (trailRenderer != null)
            trailRenderer.enabled = true;

        rb.AddForce(direction * speed, ForceMode.Impulse);
        Invoke("DestroyObject", 5.0f);
    }

    public void DestroyObject()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();
        rb.ResetCenterOfMass();

        if(trailRenderer != null)
            trailRenderer.enabled = false;

        PoolManager.Instance.ReturnToPool(gameObject);
    }

}
