using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VegetableSpawner : MonoBehaviour
{
    public List<GameObject> vegetablesPrefabs = new List<GameObject>();

    float timer = 0f;
    float tempTimer = 0f;
    float randomSpawnTime;
    void Start()
    {
        randomSpawnTime = 2.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        tempTimer += Time.deltaTime;

        if(tempTimer >= randomSpawnTime)
        {
            SpawnVegetable();
            tempTimer = 0f;
        }
    }

    void SpawnVegetable()
    {
        randomSpawnTime = Random.Range(0.4f, 2.0f);
        int randomPrefab = Random.Range(0, vegetablesPrefabs.Count);
        GameObject vegetableSpawn = Instantiate(vegetablesPrefabs[randomPrefab], transform);
        float randomAddforce = Random.Range(2600000.0f, 6000000.0f);
        vegetableSpawn.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-0.1f, 0.1f)*randomAddforce,randomAddforce,0));
    }
}
