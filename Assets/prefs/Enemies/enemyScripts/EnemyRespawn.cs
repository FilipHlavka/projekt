using FOVMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyRespawn : MonoBehaviour
{
    public static EnemyRespawn instance;

    [SerializeField]
    public static int maxRespawn;
    public static int pocetRespawnu;
    [SerializeField]
    enemyScriptable enemies;
    [SerializeField]
    public Terrain SpawnPlocha;
    public UnityEvent<GameObject> enemyRespawn;
    Bounds spawnBounds;
    TerrainData data;
    public bool spawnuj = true;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        data = SpawnPlocha.terrainData;
        spawnBounds = data.bounds;

        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(0, enemies.Count));
    }
    private void Spawn()
    {
        Debug.Log("spawn");

        float x = Random.Range(spawnBounds.min.x, spawnBounds.max.x) + SpawnPlocha.transform.position.x;
        float z = Random.Range(spawnBounds.min.z, spawnBounds.max.z) + SpawnPlocha.transform.position.z;
        float y = SpawnPlocha.SampleHeight(new Vector3(x, 0, z)) + SpawnPlocha.transform.position.y;

        Vector3 pozice = new Vector3(x, y, z);

        if (Checkni(pozice, out Vector3 fajnPozice))
        {
            GameObject enemy = Instantiate(
            enemies.enemies[Random.Range(0, enemies.enemies.Count)].enemak.gameObject,
            new Vector3(x, y, z),
            Quaternion.Euler(0, Random.Range(0, 360), 0));
            enemyRespawn.Invoke(enemy);
            FOVManager.instance.FindAllFOVAgents();
        }
        else
        {
           Spawn();
        }
        
    }

    private bool Checkni(Vector3 pozice, out Vector3 nova)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pozice, out hit, 10, NavMesh.AllAreas))
        {
            nova = hit.position;
            return true;
        }
        nova = Vector3.zero;
        return false;
    }

    IEnumerator SpawnCoroutine()
    {
        
        
        while (spawnuj)
        {
            yield return new WaitForSeconds(30f);
            if(spawnuj)
            Spawn();

        }
    }
}
