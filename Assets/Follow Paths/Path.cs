using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public Transform[] points;
    public Transform[] enemies;
    
    public float moveSpeed;

    public GameObject enemy;
    private Dictionary<string, ArrayList> rounds = new Dictionary<string, ArrayList>()
    {
        {"round1", new ArrayList() { "normal", "normal", "normal", "fast", "fast", "normal", "normal"}},
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
        transform.position = points[pointIndex].transform.position;
        // GameObject moveSpeed = transform.GetComponent<EnemyLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ArrayList a = Rounds(1);

            foreach (string b in a)
            {
                Instantiate(enemy);
            }
        }
    }

    void FixedUpdate()
    {
        if (pointIndex < points.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[pointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == points[pointIndex].transform.position)
            {
                pointIndex++;
            }
        }
    }
}
