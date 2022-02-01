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
    public int initSize;
    public int maxSize;
    public OverflowType overflowType;
    // Runtime Variables
    [HideInInspector]
    public int currentSize;
    [HideInInspector]
    public Queue <GameObject> queue = new Queue<GameObject>();
    [HideInInspector]
    public Transform parentTransform;

    /// <summary>
    /// Basic object pool instance.
    /// </summary>
    public ObjectPool(
        string _poolName, GameObject _prefab, int _initSize, int _maxSize, OverflowType _overflowType)
    {
        poolName = _poolName;
        prefab = _prefab;
        initSize = _initSize;
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
        initSize = _objectPoolData.initSize;
        maxSize = _objectPoolData.maxSize;
        overflowType = _objectPoolData.overflowType;
    }
}

