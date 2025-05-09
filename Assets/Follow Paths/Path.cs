using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] Transform[] points;
    
    [SerializeField] private float moveSpeed;
    
    private int pointIndex; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = points[pointIndex].transform.position;
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
