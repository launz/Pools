using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPooler", menuName = "boyant/ObjectPooler", order = 0)]
public class ObjectPooler : ScriptableObject 
{
    public List<ObjectPool> objectPools = new List<ObjectPool>();
    
    public virtual void InitPools() 
    {
        foreach (ObjectPool pool in objectPools) 
        {
            InitPool(pool);
        }
    }
    private void InitPool(ObjectPool pool) 
    {
        GameObject parentTransform = new GameObject();
        parentTransform.name = pool.name;
        pool.parentTransform = parentTransform.transform;

        for (int i = 0; i < pool.initSize; i++) 
        {
            CreateNewItem(pool);
        }
    }

    public virtual GameObject CreateNewItem(ObjectPool _pool) 
    {
        GameObject newObj = Instantiate(_pool.prefab);
        newObj.SetActive(false);
        _pool.queue.Enqueue(newObj);
        _pool.currentSize++;
        newObj.transform.parent = _pool.parentTransform;
        return newObj;
    }

    public virtual GameObject Spawn(ObjectPool _pool, Vector3 position_, Quaternion rotation_) 
    {
        GameObject newObj;
        // check if there are items left in pool
        if (_pool.queue.Count < 1) 
        {
            // check if pool exceeds max size
            if (_pool.currentSize >= _pool.maxSize) 
            {
                Debug.Log("#Warning: pool "+_pool.name+ " reached maximum items " + _pool.currentSize + " / " + _pool.maxSize);
                
                // if (_pool.overflowType==ObjectPoolOverflowType.Limit)
                // {
                //     return null;
                // }
                // if (_pool.overflowType==ObjectPoolOverflowType.NoLimit)
                // {
                //     newObj = CreateNewItem(_pool);
                // }
                // if (_pool.overflowType==ObjectPoolOverflowType.UseFirst)
                // {
                    
                // }
            }
            // if there are no items left but pool is not yet at max size:
            newObj = CreateNewItem(_pool);
        } 
        // take item from queue
        newObj = _pool.queue.Dequeue();
        // activate item and set right
        newObj.SetActive(true);
        newObj.transform.position = position_;
        newObj.transform.rotation = rotation_;
        // hit OnSpawn function of item that got spawned
        IPooledObject pooledObj = newObj.GetComponent<IPooledObject>();
        if (pooledObj!=null) 
        {
            pooledObj.Spawn(_pool);
        }
        return newObj;
    }

    public virtual void Despawn(ObjectPool _pool, GameObject _gameObject) {
        _pool.queue.Enqueue(_gameObject);
        _gameObject.SetActive(false);
    }

    // dictionary try/ catch approach:

    // public ObjectPool GetOrCreateObjectPool(string key) {
    //     ObjectPool objectPool;
    //     try {
    //         objectPool = objectPools[key];
    //     } catch (KeyNotFoundException) {
    //         objectPool = InitPool();
    //     }
    //     return objectPool;
    // }
}
