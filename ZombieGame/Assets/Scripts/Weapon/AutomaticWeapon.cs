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
        //예비탄환이 없거나 탄창이 꽉 차있으면 진행하지 않는다
        if (invenAmmoCount == 0 || magazineAmmoCount >= weaponData.MaxAmmo)
            yield break;

        //재장전 애니메이션 재생
        //재장전 시간동안 대기
        yield return new WaitForSeconds(weaponData.ReloadTime);

        //탄약 충전
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
