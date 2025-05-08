using UnityEngine;
using TMPro;

public class JoseMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int lives = 100;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        
        rb.linearVelocity = moveDirection;
        // transform.Translate(moveDirection * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            lives -= 1;
            GameObject.Find("LivesDisplay").GetComponent<TextMeshProUGUI>().text = lives.ToString();
        }
    }
}
