using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour{
    [Serializable]
    public class Wave{
        public Enemy[] enemies;
        public int count;

        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool spawningFinished;

    public GameObject boss;
    public Transform bossSpawnPoint;
    public GameObject healthBar;

    private void Start(){
        player = FindObjectOfType<Player>().transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private IEnumerator StartNextWave(int index){
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    private IEnumerator SpawnWave(int index){
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++){
            if (player == null){
                yield break;
            }

            var randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            var randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            if (i == currentWave.count - 1){
                spawningFinished = true;
            }
            else{
                spawningFinished = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update(){
        if (spawningFinished && FindObjectsOfType<Enemy>().Length == 0){
            spawningFinished = false;
            if (currentWaveIndex + 1 < waves.Length){
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else{
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                healthBar.SetActive(true);
                Debug.Log("Finished waves");
            }
        }
    }
}