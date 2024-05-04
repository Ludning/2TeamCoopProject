using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : SingleTon<PoolManager>
{
    GameObject sceneRig;
    public GameObject SceneRig
    {
        get
        {
            if (sceneRig == null)
                sceneRig = GameObject.Find("SceneInstaller");
            return sceneRig;
        }
        set { sceneRig = value; }
    }

    Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
    int startPoolSize = 10;

    //Ǯ���� ������
    public GameObject GetGameObject(GameObject go)
    {
        if (!pool.ContainsKey(go.name))
            CreatePool(go);
        if (pool[go.name].Count <= 1)
            ExpansionPool(go);
        GameObject copy = pool[go.name].Dequeue();
        copy.transform.SetParent(SceneRig.transform);
        copy.transform.SetParent(null);
        copy.SetActive(true);
        return copy;
    }
    //Ǯ ����
    public void ReturnToPool(GameObject go)
    {
        if (go == null)
            return;
        if (!pool.ContainsKey(go.name))
            CreatePool(go);
        go.transform.SetParent(transform);
        go.SetActive(false);
        pool[go.name].Enqueue(go);
    }
    //Ǯ ����
    private void CreatePool(GameObject go)
    {
        if (pool.ContainsKey(go.name))
            return;
        pool.Add(go.name, new Queue<GameObject>());

        for (int i = 0; i < startPoolSize; i++)
        {
            GameObject copy = Instantiate(go);
            copy.name = go.name;
            ReturnToPool(copy);
        }
    }
    //Ǯ �ڵ� Ȯ��
    private void ExpansionPool(GameObject go)
    {
        for (int i = 0; i < startPoolSize; i++)
        {
            GameObject copy = Instantiate(go);
            copy.name = go.name;
            ReturnToPool(copy);
        }
    }
}
