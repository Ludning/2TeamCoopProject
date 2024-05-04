using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //무기 정보
    public WeaponData WeaponData { get; }
    public Transform GetMagazineTransform();
    public Transform GetRightHandGrip();
    public Transform GetLeftHandGrip();
    //총기 현재 상태
    public void GetState();
    //조준
    public void OnAim();
    //재장전
    public void OnReload();
    //장착
    public void OnEquip();
    //발사 클릭 시작
    public void OnFireStart();
    //발사 클릭 종료
    public void OnFireEnd();
}
