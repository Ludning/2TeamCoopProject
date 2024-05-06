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

        // �߽����� �������� ����Ͽ� ������Ʈ���� �����ɴϴ�.
        Collider[] colliders = Physics.OverlapSphere(hit.point, 2f);

        // ������ ������Ʈ���� ��ȸ�ϸ� ó���մϴ�.
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
