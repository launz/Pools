using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsExample : MonoBehaviour
{
    [SerializeField] ObjectPool testObjectPool;

    private void Awake()
    {
        Pools.InitPool(testObjectPool);
    }

    public void SpawnItem()
    {
        Pools.Spawn(testObjectPool, transform.position + Random.insideUnitSphere * 4f, Random.rotation);
    }
}
