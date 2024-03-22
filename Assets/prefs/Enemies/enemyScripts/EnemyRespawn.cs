using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRespawn : MonoBehaviour
{
    [SerializeField]
    public static int maxRespawn;
    public static int pocetRespawnu;
    [SerializeField]
    public List<GameObject> enemies;
    [SerializeField]
    public Collider2D SpawnPlocha;
    public UnityEvent<GameObject> enemyRespawn;
    // Start is called before the first frame update
    
    void Start()
    {

        InvokeRepeating("Spawn", 25, 45);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(0, enemies.Count));
    }
    private void Spawn()
    {
        Debug.Log("spawn");

        Bounds spawnBounds = SpawnPlocha.bounds;

        GameObject enemy = Instantiate(enemies[Random.Range(0,enemies.Count)], new Vector3(Random.Range(spawnBounds.min.x,spawnBounds.max.x),Random.Range(spawnBounds.min.y,spawnBounds.max.y),0), Quaternion.identity);
        enemyRespawn.Invoke(enemy);
    }
}
