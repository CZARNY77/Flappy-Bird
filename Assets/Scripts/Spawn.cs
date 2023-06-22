using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float minSpawnRate = -2f;
    [SerializeField] float maxSpawnRate = 2.5f;
    [SerializeField] int spawnfrequency = 1;
    void Start()
    {
        Invoke(nameof(Spawner), spawnfrequency);
    }

    public void Spawner()
    {
        if (GameManager.instance.startGame && !GameManager.instance.dead)
        {
            float spawnPosition = Random.Range(minSpawnRate, maxSpawnRate);

            GameObject obstacle = Instantiate(prefab);
            obstacle.transform.position += new Vector3(transform.position.x, spawnPosition);

        }
        Invoke(nameof(Spawner), spawnfrequency);
    }
}
