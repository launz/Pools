using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    /// <summary>
    /// Initialize pool, has to be called on pool before using it.
    /// </summary>
    /// <param name="_pool"></param>
    public static void InitPool(ObjectPool _pool)
    {
        // check if pool was already initialized
        if (_pool.parentTransform != null) { return; }
        // setup queues
        _pool.inactiveQueue = new Queue<GameObject>();
        _pool.activeQueue = new Queue<GameObject>();
        // setup pool for scene hierarchy
        GameObject parentTransform = new GameObject();
        parentTransform.name = _pool.poolName;
        _pool.parentTransform = parentTransform.transform;
        // first time instantiating pool items
        for (int i = 0; i < _pool.maxSize; i++)
        {
            CreateNewObject(_pool);
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
        _pool.inactiveQueue.Enqueue(_gameObject);
        _pool.activeQueue.Dequeue();
        _gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroys all objects in object pool and the parent transform
    /// </summary>
    /// <param name="_pool"></param>
    public static void DestroyPool(ObjectPool _pool) {
        foreach (GameObject go in _pool.activeQueue)
        {
            Despawn(_pool, go);
            Destroy(go);
        }

        foreach (GameObject go in _pool.inactiveQueue)
        {
            Destroy(go);
        }
        Destroy(_pool.parentTransform);
    }
}
