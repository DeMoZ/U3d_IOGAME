using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private SpawnSpotManager _spawnSpotManager;
    /// <summary>
    /// Returns spawn spot manager
    /// </summary>
    public SpawnSpotManager GetSpawnSpotManager => _spawnSpotManager;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _spawnSpotManager = GetComponentInChildren<SpawnSpotManager>();

        if (_spawnSpotManager != null)
            _spawnSpotManager.Initialize();
        else
            throw new System.Exception($"There are no SpawnSpostManager among childs of {name}");

    }



}
