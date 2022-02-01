using UnityEngine;

public class Pools : MonoBehaviour
{
    public static void InitPool(ObjectPool pool)
    {
        // check if pool was already initialized
        if (pool.parentTransform != null) { return; }
        // setup for pool scene hierarchy
        GameObject parentTransform = new GameObject();
        parentTransform.name = pool.poolName;
        pool.parentTransform = parentTransform.transform;

        // first time instantiating pool items
        for (int i = 0; i < pool.initSize; i++)
        {
            CreateNewItem(pool);
        }
    }

    public static GameObject CreateNewItem(ObjectPool _pool)
    {
        GameObject newObj = Instantiate(_pool.prefab);
        newObj.SetActive(false);
        _pool.queue.Enqueue(newObj);
        _pool.currentSize++;
        newObj.transform.parent = _pool.parentTransform;
        return newObj;
    }

    public static GameObject Spawn(ObjectPool _pool, Vector3 _position, Quaternion _rotation)
    {
        GameObject newObj;
        // check if there are items left in pool
        if (_pool.queue.Count < 1)
        {
            // check if pool exceeds max size
            if (_pool.currentSize >= _pool.maxSize)
            {
                Debug.Log("#Warning: pool " + _pool.poolName + " reached maximum items " + _pool.currentSize + " / " + _pool.maxSize);

                //if (_pool.overflowType == OverflowType.Limit)
                //{
                //    // don't spawn new item
                //    return null;
                //}
                //if (_pool.overflowType == OverflowType.ReuseFirst)
                //{
                //    // despawn first item in queue
                //    Despawn(_pool, _pool.queue.Dequeue());
                //}
                //if (_pool.overflowType == OverflowType.AutoResize)
                //{
                //    // resize pool
                //    _pool.maxSize++;
                //    newObj = CreateNewItem(_pool);
                //}
            }
            // if there are no items left but pool is not yet at max size:
            newObj = CreateNewItem(_pool);
        }
        // take item from queue
        newObj = _pool.queue.Dequeue();
        // activate item and set right
        newObj.SetActive(true);
        newObj.transform.position = _position;
        newObj.transform.rotation = _rotation;
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
