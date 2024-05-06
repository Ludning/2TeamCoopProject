using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugExplosionRange : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position - new Vector3(0, 0, 3f), transform.position + new Vector3(0, 0, 3f));
        Debug.DrawLine(transform.position - new Vector3(0, 3f, 0), transform.position + new Vector3(0, 3f, 0));
        Debug.DrawLine(transform.position - new Vector3(3f, 0, 0), transform.position + new Vector3(3f, 0, 0));
    }
}
