using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SpawnSystem : MonoBehaviour
{
    [Header("Wave Text")]
    [SerializeField] private int WaveNumber = 1;
    [SerializeField] private TextMeshProUGUI text_WaveNumber;

    [Header("Zombies Killed To Check")]
    [SerializeField] private int ZombiesKilled = 0;

    [Header("Spawning Settings")]
    [SerializeField] private int SpawnedZombies = 0;
    [SerializeField] private int MaxZombiesOfThisWave = 3;
    [SerializeField] private float WaitTimeToSpawnAZombie = 3f;

    [Header("Zombies Prefab")]
    [SerializeField] private GameObject[] Zombies;

    [Header("Spawn Places")]
    [SerializeField] private GameObject[] RespawnPoints;


    void Start()
    {
        StartCoroutine(StartSpawningZombies());
    }


    public IEnumerator StartSpawningZombies()
    {
        while (SpawnedZombies < MaxZombiesOfThisWave)
        {
            yield return new WaitForSeconds(WaitTimeToSpawnAZombie);
            CreateZombie();
        }
        

    }

    public void CreateZombie()
    {
        int SpawnPoint = Random.Range(0, 4);

        if (WaveNumber < 3)
        {
            Instantiate(Zombies[0], RespawnPoints[SpawnPoint].transform.position, RespawnPoints[SpawnPoint].transform.rotation);
        }
        if (WaveNumber >= 3 && WaveNumber < 5)
        {
            Instantiate(Zombies[1], RespawnPoints[SpawnPoint].transform.position, RespawnPoints[SpawnPoint].transform.rotation);
        }
        if (WaveNumber >= 5)
        {
            Instantiate(Zombies[2], RespawnPoints[SpawnPoint].transform.position, RespawnPoints[SpawnPoint].transform.rotation);
        }
        SpawnedZombies++;

    }


    public void ZombieDied(int Number)
    {
        ZombiesKilled = ZombiesKilled + Number;

        if (ZombiesKilled >= MaxZombiesOfThisWave)
        {
            WaveNumber++;
            text_WaveNumber.text = "Onda " + WaveNumber;
            ZombiesKilled = 0;
            SpawnedZombies = 0;
            MaxZombiesOfThisWave = MaxZombiesOfThisWave + 1;
            StartCoroutine(StartSpawningZombies());
        }

    }
}
