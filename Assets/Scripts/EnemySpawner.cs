using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    // Start is called before the first frame update
    int startingWave = 0;
    void Start()
    {
        WaveConfig currentWave = waveConfigs[startingWave];

        StartCoroutine(SpawnAllEnemeiesInWave(currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAllEnemeiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        { 
            GameObject newEnemyClone = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].position, Quaternion.identity);

                                        newEnemyClone.GetComponent<EnemyPathing>        ().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
