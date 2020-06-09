using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains list of spawn spots on the scene.
/// Currently does nothing for game logic
/// </summary>
public class SpawnSpotManager : MonoBehaviour
{
    private List<SpawnSpot> _spawnPoints = new List<SpawnSpot>();

    private void Start()
    {
    }

    public void Initialize()
    {
        SpawnSpot[] spawnSpots = FindObjectsOfType<SpawnSpot>();        
        _spawnPoints.AddRange(spawnSpots);

       
    }

    /// <summary>
    /// Retruns random spawn points position, if presensts on the scene
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomSpawnPosition()
    {
        if (_spawnPoints.Count > 0)
        {
            int spot = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[spot].GetPosition;
        }

        throw new System.Exception("There are now spawn spots on the scene");
    }
}
