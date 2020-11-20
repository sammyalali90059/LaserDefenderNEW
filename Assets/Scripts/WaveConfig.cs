using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject pathPrefab;

    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;

    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float enemyMoveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    /*public GameObject GetPathPrefab()
    {
        return pathPrefab;
    }*/

    public List<Transform> GetWaypoints()
    {
        //creating an empty list to place each waypoint from the pathprefab inside this list
        List<Transform> waypoints = new List<Transform>();

        //Syntax for a foreach is foreach item(indicate type of item and give it a temp name) in collection
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float GetRandomSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }
}
