using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    /// <summary>
    /// Initialize pool, has to be called on pool before using it.
    /// </summary>
    /// <param name="pool"></param>
    public static void InitPool(ObjectPool pool)
    {
        // check if pool was already initialized
        if (pool.parentTransform != null) { return; }
        // setup queues
        pool.inactiveQueue = new Queue<GameObject>();
        pool.activeQueue = new Queue<GameObject>();
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

    /// <summary>
    /// Internal function to instantiate actual objects.
    /// </summary>
    /// <param name="_pool"></param>
    /// <returns></returns>
    private static GameObject CreateNewObject(ObjectPool _pool)
    {
        GameObject newObj = Instantiate(_pool.prefab);
        newObj.SetActive(false);
        Debug.Log(_pool.inactiveQueue);
        _pool.inactiveQueue.Enqueue(newObj);
        newObj.transform.parent = _pool.parentTransform;
        return newObj;
    }

    /// <summary>
    /// Spawn new object from pool into scene.
    /// </summary>
    /// <param name="_pool"></param>
    /// <param name="_position"></param>
    /// <param name="_rotation"></param>
    /// <returns></returns>
    public static GameObject Spawn(ObjectPool _pool, Vector3 _position, Quaternion _rotation)
    {
        GameObject newObj;
        // check if there are no items left in pool
        if (_pool.inactiveQueue.Count < 1)
        {
            switch (_pool.overflowType)
            {
                case OverflowType.ReuseFirst:
                    // despawn first item in queue
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,reusing first");
                    Despawn(_pool, _pool.activeQueue.Peek());
                    break;

                case OverflowType.AutoResize:
                    // resize pool
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,resizing pool.");
                    newObj = CreateNewObject(_pool);
                    _pool.maxSize++;
                    break;

                default:
                    // don't spawn new item
                    Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.maxSize + " ,limiting spawn.");
                    return null;
            }
        }
        // take item from queue
        newObj = _pool.inactiveQueue.Dequeue();
        // activate item and set right
        newObj.SetActive(true);
        newObj.transform.SetPositionAndRotation(_position, _rotation);
        // call OnSpawn function of item that got spawned
        IPooledObject pooledObj = newObj.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnSpawn();
        }
        // add object to active queue to track for order of active objects
        _pool.activeQueue.Enqueue(newObj);
        return newObj;
    }

    /// <summary>
    /// Despawn object from scene into pool.
    /// </summary>
    /// <param name="_pool"></param>
    /// <param name="_gameObject"></param>
    public static void Despawn(ObjectPool _pool, GameObject _gameObject)
    {
        // call OnDeSpawn function of item that got despawned
        IPooledObject pooledObj = _gameObject.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnDespawn();
        }
        _pool.inactiveQueue.Enqueue(_gameObject);
        _pool.activeQueue.Dequeue();
        _gameObject.SetActive(false);
    }
}
