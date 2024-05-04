using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    //무기 거치대
    [SerializeField]
    Transform weaponHanger;

    List<GameObject> weaponList = new List<GameObject>();
    //현재 무기
    IWeapon currentWeapon;

    Coroutine weaponFireCoroutine;

    private void Awake()
    {
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("AssaultRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("CrossBow").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("FlameThrower").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("HuntingRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("MachineGun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Minigun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("RocketLauncher").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("Shotgun").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SniperRifle").WaitForCompletion()));
        weaponList.Add(Instantiate(Addressables.LoadAssetAsync<GameObject>("SubMGun").WaitForCompletion()));

        weaponList.ForEach(weapon => 
        {
            weapon.transform.SetParent(weaponHanger);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.GetComponent<IWeapon>().OnEquip();
        });

        ActiveWeapon(0);
    }

    #region 입력시스템에서 사용하는 함수
    //시야
    public void OnLook(InputAction.CallbackContext context)
    {

    }
    //발사
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            currentWeapon.OnFireStart();
        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {
            currentWeapon.OnFireEnd();
        }
    }
    //조준
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }
    }
    //재장전
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
        else if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }
    }
    //무기 변경
    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        string controlPath = context.control.path;
        Debug.Log(controlPath);
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (controlPath.Contains(i.ToString()))
            {
                ActiveWeapon(i);
                break;
            }
        }
    }
    #endregion
    //무기 활성화
    public void ActiveWeapon(int index)
    {
        weaponList.ForEach(weapon => weapon.SetActive(false));
        weaponList[index].SetActive(true);
        currentWeapon = weaponList[index].GetComponent<IWeapon>();
    }
}
