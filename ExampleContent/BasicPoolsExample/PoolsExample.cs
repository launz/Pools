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

    public void SpawnObject()
    {
        GameObject newObject = Pools.Spawn(testObjectPool, transform.position + Random.insideUnitSphere * 4f, Random.rotation);
        newObject.GetComponent<SpawnEffect>().StartEffect();
    }
}
