using System;
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
    protected Transform magazineTransform;
    [SerializeField]
    protected GameObject magazineObject;
    //ÀçÀåÀü¿ë
    protected GameObject reloadMagazineObject;
    public WeaponData WeaponData { get { return weaponData; } }

    [SerializeField]
    private Transform weaponMesh;

    protected Coroutine fireCoroutine;
    protected Coroutine reloadCoroutine;

    protected int magazineAmmoCount;
    protected int invenAmmoCount;

    protected Action<float> aimReaction;

    ParticleSystem muzzleFlash;

    protected int weaponSlotIndex;
    public ParticleSystem MuzzleFlash
    {
        get
        {
            if (weaponData.MuzzleFlash != null)
            {
                if (muzzleFlash == null)
                {
                    GameObject go = Instantiate(weaponData.MuzzleFlash, firePosition);
                    muzzleFlash = go.GetComponent<ParticleSystem>();
                }
                return muzzleFlash;
            }
            return null;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        Vector3 pos = firePosition.position;
        Debug.DrawLine(pos, pos + firePosition.forward * 10, Color.red);
    }
#endif
    private void Awake()
    {
        if(magazineObject!= null)
        {
            reloadMagazineObject = Instantiate(magazineObject);
            reloadMagazineObject.transform.SetParent(transform);
            reloadMagazineObject.transform.localPosition = Vector3.zero;
            reloadMagazineObject.SetActive(false);
        }
    }
    public void SetIndex(int index)
    {
        weaponSlotIndex = index;
    }
    public void PlayMuzzleFlash()
    {
        if (MuzzleFlash != null)
        {
            MuzzleFlash.Play();
        }
    }
    public void StopMuzzleFlash()
    {
        if(MuzzleFlash != null)
        {
            MuzzleFlash.Stop();
        }
    }
    public float GetRecoverySpeed()
    {
        return weaponData.RecoverySpeed;
    }
    public virtual Transform GetReloadMagazineTransform()
    {
        return reloadMagazineObject.transform;
    }
    public virtual GameObject GetGameObject()
    {
        return gameObject;
    }
    public virtual Transform GetWeaponTransform()
    {
        return weaponMesh;
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
    public void OnFire()
    {
        PlayMuzzleFlash();
        GameObject projectile = PoolManager.Instance.GetGameObject(weaponData.projectile);
        projectile.GetComponent<Projectile>().Shot(firePosition, weaponData.velocity);
        magazineAmmoCount--;
        UIManager.Instance.UpdateAmmoText(magazineAmmoCount, invenAmmoCount, weaponSlotIndex);
    }
    public virtual void OnEquip(int index)
    {
        SetIndex(index);
        StopMuzzleFlash();
        magazineAmmoCount = weaponData.MaxAmmo;
        invenAmmoCount = weaponData.InvenAmmo;
        UIManager.Instance.UpdateAmmoText(magazineAmmoCount, invenAmmoCount, weaponSlotIndex);
    }
    public virtual void OnFireEnd()
    {

    }
    public virtual void OnFireStart(Action<float> aimReaction)
    {

    }
    public virtual void OnReload(Action<float> OnReloadAnimation, Action ExitReloadAnimation)
    {

    }
    public virtual void AddAmmo()
    {
        invenAmmoCount += 30;
    }
}
