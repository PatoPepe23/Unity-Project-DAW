using UnityEngine;

public class JoseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        
        transform.Translate(moveDirection * Time.deltaTime);
    }
}
