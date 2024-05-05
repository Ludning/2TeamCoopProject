using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//데미지 주는 오브젝트의 스크립트에서 LivingEntity 컴포넌트 가져와서 ApplyDamage()실행.
//실행전 DamageMessage 새로 만들어서 .damage 편집
public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }

    public event Action OnDeath;

    public float minTimeBetDamaged = 0.1f;   //데미지 받고 무적시간
    private float lastDamagedTime;

    protected bool IsInvulnerable
    {
        get
        {
            if (Time.time > lastDamagedTime + minTimeBetDamaged) return false;
            return true;
        }
    }

    protected virtual void OnEnable()
    {
        dead = false;
        health = maxHealth;
    }

    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        if(IsInvulnerable || damageMessage.damager == gameObject || dead) return false;

        lastDamagedTime = Time.time;
        health -= damageMessage.damage;

        if (health <= 0) Die();

        return true;
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if(dead) return;
        health += newHealth;
    }


    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
        dead = true;
    }

}
