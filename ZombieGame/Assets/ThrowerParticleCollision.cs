using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerParticleCollision : MonoBehaviour
{
    public ParticleSystem flameParticleSystem;
    void OnTriggerEnter(Collider other)
    {
        // Ʈ���ſ� ������ ��ü�� �����Ͽ� ó��
        Debug.Log("Particle triggered with: " + other.name);
    }
}
