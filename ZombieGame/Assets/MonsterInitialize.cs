using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterInitialize : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    public void SetupPosition(Vector3 position)
    {
        gameObject.SetActive(false);
        agent.enabled = false;
        transform.position = position;
        agent.enabled = true;
        gameObject.SetActive(true);
    }
    public void Init()
    {
        gameObject.GetComponent<IInitable>().Init();
    }
}
