using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void SetupPosition(Vector3 position)
    {
        agent.enabled = false;
        transform.position = position;
        agent.enabled = true;
    }
}
