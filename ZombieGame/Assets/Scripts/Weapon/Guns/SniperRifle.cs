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
