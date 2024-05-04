using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    //���� ��ġ��
    [SerializeField]
    Transform weaponHanger;

    List<GameObject> weaponList = new List<GameObject>();
    //���� ����
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

    #region �Է½ý��ۿ��� ����ϴ� �Լ�
    //�þ�
    public void OnLook(InputAction.CallbackContext context)
    {

    }
    //�߻�
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
    //����
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
    //������
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
    //���� ����
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
    //���� Ȱ��ȭ
    public void ActiveWeapon(int index)
    {
        weaponList.ForEach(weapon => weapon.SetActive(false));
        weaponList[index].SetActive(true);
        currentWeapon = weaponList[index].GetComponent<IWeapon>();
    }
}
