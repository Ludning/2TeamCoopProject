using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : Weapon
{
    GameObject muzzleFlash;
    GameObject MuzzleFlash
    {
        get
        {
            if (weaponData.MuzzleFlash != null)
            {
                if(muzzleFlash == null)
                    muzzleFlash = Instantiate(weaponData.MuzzleFlash, firePosition);
                return muzzleFlash;
            }
            return null;
        }
    }
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
        MuzzleFlash.SetActive(false);
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
            MuzzleFlash.SetActive(true);
            
            OnFire();
            Debug.Log(magazineAmmoCount);
            yield return new WaitForSeconds(weaponData.fireRate);
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
