using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    protected WeaponData weaponData;
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected Transform rightHandGrip;
    [SerializeField]
    protected Transform leftHandGrip;
    [SerializeField]
    public Transform magazineTransform;
    public WeaponData WeaponData { get { return weaponData; } }

    protected Coroutine fireCoroutine;
    protected Coroutine reloadCoroutine;


    protected int magazineAmmoCount;
    protected int invenAmmoCount;

    public virtual Transform GetMagazineTransform()
    {
        return magazineTransform;
    }
    public virtual Transform GetRightHandGrip()
    {
        return rightHandGrip;
    }
    public virtual Transform GetLeftHandGrip()
    {
        return leftHandGrip;
    }
    public virtual void GetState()
    {

    }
    public virtual void OnAim()
    {

    }
    public virtual void OnEquip()
    {
        magazineAmmoCount = weaponData.MaxAmmo;
        invenAmmoCount = weaponData.InvenAmmo;
    }
    public virtual void OnFireEnd()
    {
        /*if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);*/
    }
    public virtual void OnFireStart()
    {
        //fireCoroutine = StartCoroutine(FireCoroutine());
    }
    public virtual void OnReload()
    {
        //reloadCoroutine = StartCoroutine(ReloadCoroutine());
    }
    /*IEnumerator FireCoroutine()
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
    }*/
}
