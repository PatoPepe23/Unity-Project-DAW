using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public Transform[] points;
    public int moveSpeed;
    
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
