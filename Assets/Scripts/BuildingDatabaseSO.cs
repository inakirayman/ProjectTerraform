using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDatabaseSO : ScriptableObject
{
    public List<BuildingData> buildingData;
}

[Serializable]
public class BuildingData
{

    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public int Cost { get; private set; } = 0;
}