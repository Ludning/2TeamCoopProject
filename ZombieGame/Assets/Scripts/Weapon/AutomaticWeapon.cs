using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : Weapon
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
        StopMuzzleFlash();
    }
    public override void OnFireStart(Action<float> aimReaction)
    {
        this.aimReaction = aimReaction;
        fireCoroutine = StartCoroutine(FireCoroutine());
    }
    public override void OnReload(Action<float> OnReloadAnimation, Action ExitReloadAnimation)
    {
        reloadCoroutine = StartCoroutine(ReloadCoroutine(OnReloadAnimation, ExitReloadAnimation));
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            if (magazineAmmoCount <= 0)
            {
                StopMuzzleFlash();
                yield break; 
            }
            OnFire();
            Debug.Log(magazineAmmoCount);
            aimReaction?.Invoke(weaponData.Recoil);
            yield return new WaitForSeconds(weaponData.fireRate);
        }
    }
    IEnumerator ReloadCoroutine(Action<float> OnReloadAnimation, Action ExitReloadAnimation)
    {
        //예비탄환이 없거나 탄창이 꽉 차있으면 진행하지 않는다
        if (invenAmmoCount == 0 || magazineAmmoCount >= weaponData.MaxAmmo)
            yield break;

        //재장전 애니메이션 재생
        OnReloadAnimation?.Invoke(weaponData.ReloadTime);
        if (magazineObject != null)
            magazineObject.SetActive(false);
        //재장전 시간동안 대기
        yield return new WaitForSeconds(weaponData.ReloadTime);

        ExitReloadAnimation?.Invoke();
        if (magazineObject != null)
            magazineObject.SetActive(true);
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
        UIManager.Instance.UpdateAmmoText(magazineAmmoCount, invenAmmoCount, weaponSlotIndex);
    }
}
