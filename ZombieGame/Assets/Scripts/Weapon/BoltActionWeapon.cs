using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltActionWeapon : Weapon
{
    public override void GetState()
    {

    }
    public override void OnAim()
    {

    }
    public override void OnFireEnd()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
    }
    public override void OnFireStart()
    {
        fireCoroutine = StartCoroutine(FireCoroutine());
    }
    public override void OnReload()
    {
        reloadCoroutine = StartCoroutine(ReloadCoroutine());
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if (magazineAmmoCount <= 0)
                yield break;
            GameObject projectile = PoolManager.Instance.GetGameObject(weaponData.projectile);
            projectile.GetComponent<Projectile>().Shot(firePosition, weaponData.velocity);
            magazineAmmoCount--;
            Debug.Log(magazineAmmoCount);
            yield return new WaitForSeconds(weaponData.fireRate);
            yield break;
        }
    }
    IEnumerator ReloadCoroutine()
    {
        //����źȯ�� ���ų� źâ�� �� �������� �������� �ʴ´�
        if (invenAmmoCount == 0 || magazineAmmoCount >= weaponData.MaxAmmo)
            yield break;

        //������ �ִϸ��̼� ���
        //������ �ð����� ���
        yield return new WaitForSeconds(weaponData.ReloadTime);

        //ź�� ����
        if (invenAmmoCount >= weaponData.MaxAmmo)
        {
            invenAmmoCount -= weaponData.MaxAmmo;
            magazineAmmoCount = weaponData.MaxAmmo;
        }
        else
        {
            magazineAmmoCount += invenAmmoCount;
            invenAmmoCount = 0;
        }
    }
}
