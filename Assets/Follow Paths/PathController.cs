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
    private List<GameObject> aliveEnemies = new List<GameObject>();

    public Dictionary<string, List<GameObject>> rounds = new Dictionary<string, List<GameObject>>()
    {
        {"round1", new List<GameObject>() { }},
        {"round2", new List<GameObject>() { }},
        {"round3", new List<GameObject>() { }},
        {"round4", new List<GameObject>() { }},
        {"round5", new List<GameObject>() { }},
        {"round6", new List<GameObject>() { }},
        {"round7", new List<GameObject>() { }},
        {"round8", new List<GameObject>() { }},
        {"round9", new List<GameObject>() { }},
        {"round10", new List<GameObject>() { }},
        {"round11", new List<GameObject>() { }},
        {"round12", new List<GameObject>() { }},
        {"round13", new List<GameObject>() { }},
        {"round14", new List<GameObject>() { }},
        {"round15", new List<GameObject>() { }},
        {"round16", new List<GameObject>() { }},
        {"round17", new List<GameObject>() { }},
        {"round18", new List<GameObject>() { }},
        {"round19", new List<GameObject>() { }},
        {"round20", new List<GameObject>() { }},
        {"round21", new List<GameObject>() { }},
        {"round22", new List<GameObject>() { }},
        {"round23", new List<GameObject>() { }},
        {"round24", new List<GameObject>() { }},
        {"round25", new List<GameObject>() { }},
        {"round26", new List<GameObject>() { }},
        {"round27", new List<GameObject>() { }},
        {"round28", new List<GameObject>() { }},
        {"round29", new List<GameObject>() { }},
        {"round30", new List<GameObject>() { }}
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
        // --- Round 1: Gentle Introduction ---
        rounds["round1"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            DefauldEnemy
        }; // 5 Default

        // --- Round 2: A bit more, maybe one Tank ---
        rounds["round2"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            DefauldEnemy, TankEnemy
        }; // 5 Default, 1 Tank

        // --- Round 3: Introducing Fast enemies ---
        rounds["round3"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            FastEnemy, FastEnemy
        }; // 4 Default, 2 Fast

        // --- Round 4: More variety ---
        rounds["round4"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy,
            FastEnemy
        }; // 3 Default, 2 Tank, 1 Fast

        // --- Round 5: First small wave ---
        rounds["round5"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            FastEnemy, FastEnemy, FastEnemy
        }; // 5 Default, 3 Fast

        // --- Round 6: Tank push ---
        rounds["round6"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            DefauldEnemy, DefauldEnemy
        }; // 4 Tank, 2 Default

        // --- Round 7: Fast swarm starts ---
        rounds["round7"] = new List<GameObject>()
        {
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy,
            DefauldEnemy, DefauldEnemy, DefauldEnemy
        }; // 5 Fast, 3 Default

        // --- Round 8: Balanced mix ---
        rounds["round8"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy,
            FastEnemy, FastEnemy
        }; // 4 Default, 2 Tank, 2 Fast

        // --- Round 9: Tank-heavy, with fast support ---
        rounds["round9"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy
        }; // 5 Tank, 2 Fast

        // --- Round 10: Mini-Boss Introduction ---
        rounds["round10"] = new List<GameObject>()
        {
            BossEnemy, // A single boss
            DefauldEnemy, DefauldEnemy, DefauldEnemy,
            FastEnemy, FastEnemy
        }; // 1 Boss, 3 Default, 2 Fast (Boss as centerpiece)

        // --- Round 11: Increased Fast numbers ---
        rounds["round11"] = new List<GameObject>()
        {
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy,
            DefauldEnemy, DefauldEnemy, DefauldEnemy
        }; // 6 Fast, 3 Default

        // --- Round 12: Heavy Tank presence ---
        rounds["round12"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            DefauldEnemy
        }; // 6 Tank, 1 Default

        // --- Round 13: Mixed assault ---
        rounds["round13"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy
        }; // 4 Default, 3 Tank, 3 Fast

        // --- Round 14: Fast + Default wave ---
        rounds["round14"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 6 Default, 5 Fast

        // --- Round 15: Significant Tank push ---
        rounds["round15"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy
        }; // 7 Tank, 2 Fast

        // --- Round 16: Speed challenge ---
        rounds["round16"] = new List<GameObject>()
        {
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy,
            DefauldEnemy, DefauldEnemy
        }; // 8 Fast, 2 Default

        // --- Round 17: More balanced but larger ---
        rounds["round17"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy
        }; // 5 Default, 4 Tank, 3 Fast

        // --- Round 18: Heavy tank and fast combined ---
        rounds["round18"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 6 Tank, 5 Fast

        // --- Round 19: All enemy types in force ---
        rounds["round19"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 6 Default, 3 Tank, 4 Fast

        // --- Round 20: Second Boss Wave ---
        rounds["round20"] = new List<GameObject>()
        {
            BossEnemy, // A boss again
            TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 1 Boss, 4 Tank, 4 Fast (Boss with tougher support)

        // --- Round 21: Recovery wave / Smaller, but still tough ---
        rounds["round21"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 7 Default, 4 Fast

        // --- Round 22: Relentless fast enemies ---
        rounds["round22"] = new List<GameObject>()
        {
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy,
            DefauldEnemy
        }; // 10 Fast, 1 Default

        // --- Round 23: Tank blockade ---
        rounds["round23"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy
        }; // 9 Tank

        // --- Round 24: All-out mixed assault ---
        rounds["round24"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 5 Default, 5 Tank, 6 Fast

        // --- Round 25: First major wave with high density ---
        rounds["round25"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 8 Default, 5 Tank, 4 Fast

        // --- Round 26: Sustained Fast and Tank pressure ---
        rounds["round26"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 7 Tank, 7 Fast

        // --- Round 27: Maximum Fast enemies ---
        rounds["round27"] = new List<GameObject>()
        {
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy,
            FastEnemy, FastEnemy, FastEnemy // 13 Fast
        };

        // --- Round 28: Extreme Tank defense ---
        rounds["round28"] = new List<GameObject>()
        {
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy
        }; // 10 Tank

        // --- Round 29: Pre-final boss mix (warm-up) ---
        rounds["round29"] = new List<GameObject>()
        {
            DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy, DefauldEnemy,
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy,
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy
        }; // 7 Default, 6 Tank, 7 Fast

        // --- Round 30: The FINAL Boss Encounter! ---
        rounds["round30"] = new List<GameObject>()
        {
            BossEnemy, // The ultimate boss!
            TankEnemy, TankEnemy, TankEnemy, TankEnemy, TankEnemy, // Heavy tank escort
            FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy, FastEnemy // Fast flanking units
        }; // 1 Boss, 5 Tank, 7 Fast (This should be a significant challenge!)
    }

    void Update()
    {
        if (spawningRound)
        {
            SpawnEnemiesOverTime();
        }
    }

    public void StartSpawningRound()
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
                        aliveEnemies.Add(newEnemy);
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
            round++;
            enemyIndex = 0;
        }
    }
}