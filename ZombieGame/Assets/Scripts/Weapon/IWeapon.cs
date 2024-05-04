using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //���� ����
    public WeaponData WeaponData { get; }
    public Transform GetMagazineTransform();
    public Transform GetRightHandGrip();
    public Transform GetLeftHandGrip();
    //�ѱ� ���� ����
    public void GetState();
    //����
    public void OnAim();
    //������
    public void OnReload();
    //����
    public void OnEquip();
    //�߻� Ŭ�� ����
    public void OnFireStart();
    //�߻� Ŭ�� ����
    public void OnFireEnd();
}
