using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic object pool.
/// </summary>
[System.Serializable]
public class ObjectPool {
    public string name = "name";
    [Header("REFERENCES")]
    public GameObject prefab;
    [Header("PARAMETERS")]
    public int initSize;
    public int maxSize;
    [HideInInspector]
    public int currentSize;
    [HideInInspector]
    public Queue <GameObject> queue = new Queue<GameObject>();
    [HideInInspector]
    public Transform parentTransform;
    public OverflowType overflowType;
}

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