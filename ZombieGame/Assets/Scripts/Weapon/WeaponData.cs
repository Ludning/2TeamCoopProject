using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    //데미지
    [Header("데미지")]
    public float Damage;

    //총기 연사 속도
    [Header("발사 간격")]
    public float fireRate;

    //탄속
    [Header("탄속")]
    public float velocity;

    //총기 발사 방식
    [Header("발사 모드")]
    public FireMode fireMode;

    //총기 최대 탄약수
    [Header("최대 탄약")]
    public int MaxAmmo;

    //첫 획득시 소지 탄약수
    [Header("소지 탄약")]
    public int InvenAmmo;

    //반동
    [Header("반동")]
    public float Recoil;

    //재장전 시간
    [Header("재장전 시간")]
    public float ReloadTime;

    //격발음
    [Header("격발음")]
    public AudioClip Sound;

    //투사체
    [Header("투사체")]
    public GameObject projectile;

    //발사 이펙트
    [Header("총구 화염")]
    public GameObject MuzzleFlash;
}
