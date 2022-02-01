using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    public static void InitPool(ObjectPool pool)
    {
        // check if pool was already initialized
        if (pool.parentTransform != null) { return; }
        // setup queue
        pool.queue = new Queue<GameObject>();
        // setup pool for scene hierarchy
        GameObject parentTransform = new GameObject();
        parentTransform.name = pool.poolName;
        pool.parentTransform = parentTransform.transform;
        // first time instantiating pool items
        for (int i = 0; i < pool.maxSize; i++)
        {
            CreateNewObject(pool);
        }
    }

    public static GameObject CreateNewObject(ObjectPool _pool)
    {
        GameObject newObj = Instantiate(_pool.prefab);
        newObj.SetActive(false);
        Debug.Log(_pool.queue);
        _pool.queue.Enqueue(newObj);
        newObj.transform.parent = _pool.parentTransform;
        return newObj;
    }

    public static GameObject Spawn(ObjectPool _pool, Vector3 _position, Quaternion _rotation)
    {
        GameObject newObj;
        // check if there are no items left in pool
        if (_pool.queue.Count < 1)
        {
                if (_pool.overflowType == OverflowType.Limit)
                {
                    // don't spawn new item
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,limiting spawn.");
                    return null;
                }
                if (_pool.overflowType == OverflowType.ReuseFirst)
                {
                    // despawn first item in queue
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,reusing first");
                    Despawn(_pool, _pool.queue.Dequeue());
                }
                if (_pool.overflowType == OverflowType.AutoResize)
                {
                    // resize pool
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,resizing pool.");
                    newObj = CreateNewObject(_pool);
                    _pool.maxSize++;
                }
        }
        // take item from queue
        newObj = _pool.queue.Dequeue();
        // activate item and set right
        newObj.SetActive(true);
        newObj.transform.SetPositionAndRotation(_position, _rotation);
        // hit OnSpawn function of item that got spawned
        IPooledObject pooledObj = newObj.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.Spawn(_pool);
        }
        return newObj;
    }

    public static void Despawn(ObjectPool _pool, GameObject _gameObject)
    {
        _pool.queue.Enqueue(_gameObject);
        _gameObject.SetActive(false);
    }
}
