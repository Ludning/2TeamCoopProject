using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //���� ����
    public WeaponData WeaponData { get; }
    public void PlayMuzzleFlash();
    public void StopMuzzleFlash();
    public Transform GetReloadMagazineTransform();
    public GameObject GetGameObject();
    public Transform GetWeaponTransform();
    public Transform GetRightHandGrip();
    public Transform GetLeftHandGrip();
    public float GetRecoverySpeed();
    //�ѱ� ���� ����
    public void GetState();
    //����
    public void OnAim();
    //������
    public void OnReload(Action<float> OnReloadAnimation, Action ExitReloadAnimation);
    //����
    public void OnEquip();
    //�߻� Ŭ�� ����
    public void OnFireStart(Action<float> aimReaction);
    //�߻� Ŭ�� ����
    public void OnFireEnd();
    public void AddAmmo();
}
