using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : PanelSignleton<ObjectPool>
{
    public List<GameObject> PrefabsForPool;
    private List<GameObject> _pooledObjects = new List<GameObject>();
    public GameObject GetObjectFromPool(string objectName)
    {
        var instance = _pooledObjects.FirstOrDefault(obj => obj.name == objectName); // try to get pooled isntance

        // if pooled instance already exists
        if (instance != null)
        {
            _pooledObjects.Remove(instance);
            instance.SetActive(true);
            return instance;
        }
        // if pooled instance does not exist
        var prefab = PrefabsForPool.FirstOrDefault(obj => obj.name == objectName);
        if (prefab != null)
        {
            // create new instance
            var newInstance = Instaniate(prefab, Vector3.zero, Quaternion.identity, transform);
            newInstance.name = objectName;
            return newInstance;
        }

        Debug.LogWarning("Object pool doesn't have a prefab for the object with name " + objectName);
        return null;
    }

    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        _pooledObjects.Add(obj);
    }
}
