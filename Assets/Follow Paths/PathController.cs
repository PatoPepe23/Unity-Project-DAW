using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour
{
    public Transform[] points;
    public List<GameObject> enemiesList = new List<GameObject>();

    public GameObject DefauldEnemy;
    public GameObject FastEnemy;
    public GameObject TankEnemy;
    public GameObject BossEnemy;

    public float spawnInterval = 1f; // Tiempo en segundos entre cada spawn
    private float nextSpawnTime = 0f;
    public int round = 1;
    private int enemyIndex = 0;
    private bool spawningRound = false;

    public Dictionary<string, List<GameObject>> rounds = new Dictionary<string, List<GameObject>>()
    {
        {"round1", new List<GameObject>() { null, null, null, null, null, null, null, null }},
        {"round2", new List<GameObject>() {  }},
        {"round3", new List<GameObject>() {  }},
        {"round4", new List<GameObject>() {  }},
        {"round5", new List<GameObject>() {  }},
        {"round6", new List<GameObject>() {  }},
        {"round7", new List<GameObject>() {  }},
        {"round8", new List<GameObject>() {  }},
    };

    List<GameObject> GetCurrentRound()
    {
        string roundName = "round" + round;
        if (rounds.ContainsKey(roundName))
        {
            return rounds[roundName];
        }
        return null;
    }

    void Start()
    {
        if (rounds.ContainsKey("round1"))
        {
            rounds["round1"][0] = DefauldEnemy;
            rounds["round1"][1] = DefauldEnemy;
            rounds["round1"][2] = DefauldEnemy;
            rounds["round1"][3] = DefauldEnemy;
            rounds["round1"][4] = DefauldEnemy;
            rounds["round1"][5] = TankEnemy;
            rounds["round1"][6] = TankEnemy;
            rounds["round1"][7] = TankEnemy;
        }
    }

    void Update()
    {
        if (!spawningRound && Input.GetMouseButtonDown(0))
        {
            StartSpawningRound();
        }

        if (spawningRound)
        {
            SpawnEnemiesOverTime();
        }
    }

    void StartSpawningRound()
    {
        List<GameObject> currentRoundEnemies = GetCurrentRound();
        if (currentRoundEnemies != null && currentRoundEnemies.Count > 0)
        {
            spawningRound = true;
            nextSpawnTime = Time.time + spawnInterval;
            enemyIndex = 0;
        }
        else
        {
            Debug.Log("Ronda " + round + " está vacía o no existe.");
            round++; // Avanzar a la siguiente ronda si la actual está vacía
        }
    }

    void SpawnEnemiesOverTime()
    {
        List<GameObject> currentRoundEnemies = GetCurrentRound();
        if (currentRoundEnemies != null && enemyIndex < currentRoundEnemies.Count)
        {
            if (Time.time >= nextSpawnTime)
            {
                GameObject enemyToSpawn = currentRoundEnemies[enemyIndex];
                if (enemyToSpawn != null)
                {
                    GameObject newEnemy = Instantiate(enemyToSpawn, points[0].position, Quaternion.identity);
                    Path enemyPath = newEnemy.GetComponent<Path>();
                    if (enemyPath != null)
                    {
                        enemyPath.points = points;
                    }
                    else
                    {
                        Debug.LogWarning("El enemigo instanciado no tiene un script 'Path'.");
                    }
                }

                nextSpawnTime = Time.time + spawnInterval;
                enemyIndex++;
            }
        }
        else
        {
            spawningRound = false;
            round++; // Pasar a la siguiente ronda después de spawnear todos los enemigos
            enemyIndex = 0; // Resetear el índice para la siguiente ronda
        }
    }
}