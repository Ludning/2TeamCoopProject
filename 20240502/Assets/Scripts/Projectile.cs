using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    GameObject HitPrefab;

    Vector3 direction;

    [SerializeField]
    MeshRenderer meshRenderer;

    [SerializeField]
    float moveSpeed = 2f;
    public void Init(Vector3 dir)
    {
        direction = dir;
        meshRenderer.enabled = true;
        transform.forward = direction;
        Destroy(gameObject, 10.0f);
    }
    private void FixedUpdate()
    {
        transform.Translate(direction.normalized * moveSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnHit();
    }
    private void OnHit()
    {
        GameObject instance = Instantiate(HitPrefab);
        instance.transform.position = transform.position;
        instance.transform.Rotate(Vector3.forward, 180f);
        Destroy(instance, 5f);
        Destroy(gameObject);
    }
}
