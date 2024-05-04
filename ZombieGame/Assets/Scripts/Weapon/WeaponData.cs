using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    //������
    [Header("������")]
    public float Damage;

    //�ѱ� ���� �ӵ�
    [Header("�߻� ����")]
    public float fireRate;

    //ź��
    [Header("ź��")]
    public float velocity;

    //�ѱ� �߻� ���
    [Header("�߻� ���")]
    public FireMode fireMode;

    //�ѱ� �ִ� ź���
    [Header("�ִ� ź��")]
    public int MaxAmmo;

    //ù ȹ��� ���� ź���
    [Header("���� ź��")]
    public int InvenAmmo;

    //�ݵ�
    [Header("�ݵ�")]
    public float Recoil;

    //������ �ð�
    [Header("������ �ð�")]
    public float ReloadTime;

    //�ݹ���
    [Header("�ݹ���")]
    public AudioClip Sound;

    //����ü
    [Header("����ü")]
    public GameObject projectile;

    //�߻� ����Ʈ
    [Header("�ѱ� ȭ��")]
    public GameObject MuzzleFlash;
}
