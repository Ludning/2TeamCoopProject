using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : MonoBehaviour, IWeapon
{
    [SerializeField]
    private WeaponData weaponData;
    [SerializeField]
    private Transform firePosition;

    [SerializeField]
    private Transform rightHandGrip;
    [SerializeField]
    private Transform leftHandGrip;
    [SerializeField]
    public Transform magazineTransform;
    public WeaponData WeaponData { get { return weaponData; } }

    Coroutine fireCoroutine;
    Coroutine reloadCoroutine;


    int magazineAmmoCount;
    int invenAmmoCount;

    public Transform GetMagazineTransform()
    {
        return magazineTransform;
    }
    public Transform GetRightHandGrip()
    {
        return rightHandGrip;
    }
    public Transform GetLeftHandGrip()
    {
        return leftHandGrip;
    }

    public void GetState()
    {

    }
    public void OnAim()
    {

    }
    public void OnEquip()
    {

    }
    public void OnFireEnd()
    {
        StopCoroutine(fireCoroutine);
    }
    public void OnFireStart()
    {
        fireCoroutine = StartCoroutine(FireCoroutine());
    }
    public void OnReload()
    {
        reloadCoroutine = StartCoroutine(ReloadCoroutine());
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject projectile = PoolManager.Instance.GetGameObject(weaponData.projectile);
            projectile.transform.position = firePosition.position;
            projectile.GetComponent<Projectile>().Shot(firePosition, weaponData.velocity);
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
