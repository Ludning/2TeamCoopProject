using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //무기 정보
    public WeaponData WeaponData { get; }
    public void PlayMuzzleFlash();
    public void StopMuzzleFlash();
    public Transform GetReloadMagazineTransform();
    public GameObject GetGameObject();
    public Transform GetWeaponTransform();
    public Transform GetRightHandGrip();
    public Transform GetLeftHandGrip();
    public float GetRecoverySpeed();
    //총기 현재 상태
    public void GetState();
    //조준
    public void OnAim();
    //재장전
    public void OnReload(Action<float> OnReloadAnimation, Action ExitReloadAnimation);
    //장착
    public void OnEquip();
    //발사 클릭 시작
    public void OnFireStart(Action<float> aimReaction);
    //발사 클릭 종료
    public void OnFireEnd();
    public void AddAmmo();
}
