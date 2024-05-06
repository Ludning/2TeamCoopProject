using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerParticleCollision : MonoBehaviour
{
    public ParticleSystem flameParticleSystem;
    void OnTriggerEnter(Collider other)
    {
        // 트리거에 진입한 객체를 감지하여 처리
        Debug.Log("Particle triggered with: " + other.name);
    }
}
