using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> SpawnPointList;

    [SerializeField]
    List<LivingEntity> MonsterList;

    [SerializeField]
    List<GameObject> MonsterPrefabs;

    private void Start()
    {
        StartCoroutine(CheakSpawnPointAndSpawn());
    }

    IEnumerator CheakSpawnPointAndSpawn()
    {
        while (true)
        {
            foreach (var spawnPoint in SpawnPointList)
            {
                Collider[] colliders =  Physics.OverlapSphere(spawnPoint.transform.position, 0.2f);
                if(colliders.Count() == 0)
                {
                    Debug.Log($"스폰 성공 {spawnPoint.name}");
                    GameObject monster = PoolManager.Instance.GetGameObject(MonsterPrefabs[Random.Range(0, MonsterPrefabs.Count)]);
                    Debug.Log(monster.GetComponent<MonsterController>());
                    Debug.Log(spawnPoint.transform.position);
                    MonsterInitialize init = monster.GetComponent<MonsterInitialize>();
                    init.SetupPosition(spawnPoint.transform.position);
                    init.Init();
                    MonsterList.Add(monster.GetComponent<LivingEntity>());
                }
                else
                {
                    Debug.Log($"뭔가 있어 {spawnPoint.name}, {spawnPoint.transform.position}");
                    foreach (var item in colliders)
                    {
                        Debug.Log($"그게 뭐냐면 {item.gameObject.name}");
                    }
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
