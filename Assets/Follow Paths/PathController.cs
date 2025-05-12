using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour
{
    public Transform[] points;
    public List<GameObject> enemiesList = new List<GameObject>();
    
    private GameObject enemy;
    private Dictionary<string, ArrayList> rounds = new Dictionary<string, ArrayList>()
    {
        {"round1", new ArrayList() { "enemy", "enemy", "enemy", "enemy", "enemy", "enemy", "enemy"}},
        {"round2", new ArrayList() {  }},
        {"round3", new ArrayList() {  }},
        {"round4", new ArrayList() {  }},
        {"round5", new ArrayList() {  }},
        {"round6", new ArrayList() {  }},
        {"round7", new ArrayList() {  }},
        {"round8", new ArrayList() {  }},
    };
    
    ArrayList Rounds(int round)
    {
        string roundName = "round"+round;
        
        return rounds[roundName];
    }
    
    
    private int pointIndex; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // transform.position = points[pointIndex].transform.position;
        // GameObject moveSpeed = transform.GetComponent<EnemyLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ArrayList a = enemiesList(1);

            foreach (GameObject b in enemiesList)
            {
                GameObject newEnemy = Instantiate(b, points[0].position, Quaternion.identity);
                
                 newEnemy.GetComponent<Path>().points = points; 
            }
        }
    }
}