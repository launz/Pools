using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data that can be used to create pools with predefined parameters.
/// </summary>
[CreateAssetMenu(fileName = "ObjectPool", menuName = "OUT/ObjectPool", order = 0)]
public class ObjectPoolData : ScriptableObject
{
    public string poolName;
    [Header("REFERENCES")]
    public GameObject prefab;
    [Header("PARAMETERS")]
    public int maxSize;
    public OverflowType overflowType;
}
