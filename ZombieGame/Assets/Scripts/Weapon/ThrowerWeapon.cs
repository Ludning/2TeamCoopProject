using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerWeapon : Weapon
{


    ParticleSystem flameThrower;
    ParticleSystem FlameThrower
    {
        get
        {
            if(flameThrower == null)
            {
                GameObject particleObject = Instantiate(weaponData.projectile);
                particleObject.transform.SetParent(firePosition);
                particleObject.transform.position = firePosition.position;
                particleObject.transform.rotation = firePosition.rotation;
                flameThrower = particleObject.GetComponent<ParticleSystem>();
            }
            return flameThrower;
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
        FlameThrower.Stop();
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
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
                FlameThrower.Stop();
                yield break;
            }

            FlameThrower.Play();
            magazineAmmoCount--;
            Debug.Log(magazineAmmoCount);
            aimReaction?.Invoke(weaponData.Recoil);
            yield return new WaitForSeconds(weaponData.fireRate);
        }
    }
    IEnumerator ReloadCoroutine(Action<float> OnReloadAnimation, Action ExitReloadAnimation)
    {
        //����źȯ�� ���ų� źâ�� �� �������� �������� �ʴ´�
        if (invenAmmoCount == 0 || magazineAmmoCount >= weaponData.MaxAmmo)
            yield break;

        //������ �ִϸ��̼� ���
        OnReloadAnimation?.Invoke(weaponData.ReloadTime);
        if (magazineObject != null)
            magazineObject.SetActive(false);
        //������ �ð����� ���
        yield return new WaitForSeconds(weaponData.ReloadTime);

        ExitReloadAnimation?.Invoke();
        if (magazineObject != null)
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
