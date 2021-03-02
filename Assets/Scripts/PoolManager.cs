using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public  class ObjectPoolItem
{

    public int poolLenght;
    public bool shouldExpand = true;

    public GameObject objectToPool;

}

public  class PoolManager : Singleton<PoolManager>
{
    public List<ObjectPoolItem> itemsToPool;
    private List<GameObject> PooledObjects;

    void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        {
            PooledObjects = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.poolLenght; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, new Vector3 (Random.Range(-20, 20),0, Random.Range(30, 70)),Quaternion.identity);
                    obj.SetActive(false);
                    PooledObjects.Add(obj);
                }
            }
        }
    }
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].activeInHierarchy && PooledObjects[i].tag == tag)
            {
                return PooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool, new Vector3(Random.Range(-20, 20), 0, Random.Range(30, 70)), Quaternion.identity);
                    PooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
