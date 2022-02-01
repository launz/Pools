using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior for pooling more items than max pool size.<br></br>
/// Limit: Don't allow further pooling of objects.<br></br>
/// ReuseFirst: Use despawn behavior for first object in pool and spawn it as new.<br></br>
/// AutoResize: Automatically resize pool max size and create new objects.
/// </summary>
public enum OverflowType
{
    Limit,
    ReuseFirst,
    AutoResize
}

/// <summary>
/// Basic object pool.
/// </summary>
[System.Serializable]
public class ObjectPool
{
    public string poolName;
    [Header("REFERENCES")]
    public GameObject prefab;
    [Header("PARAMETERS")]
    public int maxSize;
    public OverflowType overflowType;
    // Runtime Variables
    [HideInInspector]
    public int currentSize;
    [HideInInspector]
    public Queue <GameObject> queue;
    [HideInInspector]
    public Transform parentTransform;

    /// <summary>
    /// Basic object pool instance.
    /// </summary>
    public ObjectPool(
        string _poolName, GameObject _prefab, int _maxSize, OverflowType _overflowType)
    {
        poolName = _poolName;
        prefab = _prefab;
        maxSize = _maxSize;
        overflowType = _overflowType;
    }

    /// <summary>
    /// Basic object pool instance from ObjectPoolData.
    /// </summary>
    public ObjectPool(ObjectPoolData _objectPoolData)
    {
        poolName = _objectPoolData.poolName;
        prefab = _objectPoolData.prefab;
        maxSize = _objectPoolData.maxSize;
        overflowType = _objectPoolData.overflowType;
    }
}

