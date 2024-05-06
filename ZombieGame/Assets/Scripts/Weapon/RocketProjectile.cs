using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    [SerializeField]
    GameObject hitPaticle;

    protected override void HitObject(RaycastHit hit)
    {
        GameObject hitPaticle = PoolManager.Instance.GetGameObject(this.hitPaticle);

        Vector3 hitNormal = hit.normal;
        Debug.Log(hitNormal);

        hitPaticle.transform.position = hit.point + hitNormal * 0.01f;
        hitPaticle.transform.rotation = Quaternion.LookRotation(hitNormal);

        // 중심점과 반지름을 사용하여 오브젝트들을 가져옵니다.
        Collider[] colliders = Physics.OverlapSphere(hit.point, 2f);

        // 가져온 오브젝트들을 순회하며 처리합니다.
        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Monster"))
            {
                //collider.GetComponent<IDamageable>().OnDamage();
                Debug.Log($"HitMonster : {collider.gameObject.name}");
            }
        }
    }
}
