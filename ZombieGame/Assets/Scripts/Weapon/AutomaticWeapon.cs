using System;
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
    public override void OnFireStart(Action<float> aimReaction)
    {
        this.aimReaction = aimReaction;
        fireCoroutine = StartCoroutine(FireCoroutine());
    }
    public override void OnReload(Action OnReloadAnimation, Action ExitReloadAnimation)
    {
        reloadCoroutine = StartCoroutine(ReloadCoroutine(OnReloadAnimation, ExitReloadAnimation));
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if (magazineAmmoCount <= 0)
            {
                MuzzleFlash.SetActive(false);
                yield break; 
            }
            MuzzleFlash.SetActive(true);
            GameObject projectile = PoolManager.Instance.GetGameObject(weaponData.projectile);
            projectile.GetComponent<Projectile>().Shot(firePosition, weaponData.velocity);
            magazineAmmoCount--;
            Debug.Log(magazineAmmoCount);
            aimReaction?.Invoke(weaponData.Recoil);
            yield return new WaitForSeconds(weaponData.fireRate);
        }
    }
    IEnumerator ReloadCoroutine(Action OnReloadAnimation, Action ExitReloadAnimation)
    {
        //����źȯ�� ���ų� źâ�� �� �������� �������� �ʴ´�
        if (invenAmmoCount == 0 || magazineAmmoCount >= weaponData.MaxAmmo)
            yield break;

        //������ �ִϸ��̼� ���
        OnReloadAnimation?.Invoke();
        magazineObject?.SetActive(false);
        //������ �ð����� ���
        yield return new WaitForSeconds(weaponData.ReloadTime);

        ExitReloadAnimation?.Invoke();
        magazineObject.SetActive(true);
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
